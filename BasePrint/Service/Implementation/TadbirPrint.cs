using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;

namespace BasePrint
{
    public class TadbirPrint : System.Activities.CodeActivity<String> , ITadbirPrint
    {
        CodepageConvertor _codepageService = new CodepageConvertor();

        public InArgument<int> UserId { get; set; }

        public InArgument<int> WorkspaceId { get; set; }

        public InArgument<int> FPId { get; set; }

        public InArgument<int> SubsystemId { get; set; }

        public InArgument<string> ReportName { get; set; }

        public InArgument<string[]> ParamTypes { get; set; }
    
        public InArgument<string[]> ParamValues { get; set; }

        protected override String Execute(CodeActivityContext context)
        {
            try
            {
                int userId = UserId.Get<int>(context);
                int workspaceId = WorkspaceId.Get<int>(context);
                int fpid = FPId.Get<int>(context);
                int subsystemId = SubsystemId.Get<int>(context);
                string reportName = ReportName.Get<string>(context);
                string[] paramTypes = ParamTypes.Get<string[]>(context);
                string[] paramValues = ParamValues.Get<string[]>(context);
                string exceptionStr;

                PrintReport(userId, workspaceId, fpid, subsystemId, reportName, paramTypes, paramValues, out exceptionStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public string PrintReport(int userId, int workspaceId, int fpId, int subsystemId, string reportName, string[] paramTypes, string[] paramValues, out string exceptionStr)
        {
            exceptionStr = "";
            if (string.IsNullOrEmpty(QueuePath))
            {
                exceptionStr = "مسیر چاپ تعیین نشده است.";
                return null;
            }

            /*if (paramTypes.Length != paramValues.Length)
            {
                exceptionStr = $"paramValues count ({paramValues.Length}) <> paramTypes count ({paramTypes.Length})";
                return null;
            }*/

            try
            {
                List<string> lines = new List<string>();
                /*
                 ****************************************************** FILE FORMAT STARTING FROM LINE 1:
                 * LINE 1: UserId
                 * LINE 2: WsId
                 * LINE 3: FpId
                 * LINE 4:Output File Path (c:\test\1.pdf) //unused - you can put anything here
                 * LINE 5:Tadbir Report Name //If this string contains $ it means it is a QuickReport and it's format is: QueryName$PrintTitle$HiddenColumnsCount$ColumnWidth1$ColumnWidth2$...$ColumnWidthN
                 * LINE 6:Parameters Count:
                 * LINE 7: Param1... (For Quick Print => Param1 = ParamVal$ParamText$ParamIsHidden$ParamWidth)
                 * LINE 7 + (Parameters Count) : ParamN
                 * LINE 1 + 7 + (Parameters Count): SubSystem
                 * LINE 2 + 7 + (Parameters Count): Width(1200 - 4800) //unused - you can put anything here
                 * LINE 3 + 7 + (Parameters Count): Height(1600 - 6400) //unused - you can put anything here
                 * LINE 4 + 7 + (Parameters Count): C:\Program Files(x86)\PDFCreator\Images2PDF\Images2PDFC.exe //unused - you can put anything here
                 ****************************************************** THE ABOVE WAS THE LAST LINE
                 */


                lines.Add(userId.ToString()); //LINE 1
                lines.Add(workspaceId.ToString()); //LINE 2
                lines.Add(fpId.ToString()); //LINE 3
                lines.Add("NOT EMPTY LINE"); //LINE 4
                lines.Add(_codepageService.toTadbir(reportName)); //LINE 5



                lines.Add(paramValues.Length.ToString()); //LINE 6


                for (int i = 0; i < paramValues.Length; i++)
                {
                    if (paramTypes[i] == "TEXT")
                    {
                        lines.Add(_codepageService.toTadbir(paramValues[i]));
                    }
                    else
                    if (paramTypes[i] == "DATE")
                    {
                        string paramValue = paramValues[i];
                        string[] parts = paramValue.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 3)
                        {
                            int year = int.Parse(parts[0]);
                            int month = int.Parse(parts[1]);
                            int day = int.Parse(parts[2]);

                            paramValue = String.Format("{0:D2}/{1:D2}/{2:D4}", month, day, year);
                        }
                        lines.Add(paramValue);

                    }
                    else
                    {
                        lines.Add(paramValues[i]);
                    }
                }

                lines.Add(subsystemId.ToString()); //LINE 1 + 7 + (Parameters Count): SubSystem

                //last three lines:
                lines.Add("NOT EMPTY LINE");
                lines.Add("NOT EMPTY LINE");
                lines.Add("NOT EMPTY LINE");

                string queueTextFileName = GenerateJobFileName() + ".txt";


                JobWatcher jobWatcher =
                    (
                    new JobWatcher()
                    {
                        PdfFileName = Path.GetFileNameWithoutExtension(queueTextFileName) + ".pdf",
                        Processed = new FileSystemWatcher(ProcessedPath, "*.pdf"),
                        Errors = new FileSystemWatcher(ErrorsPath, "*.txt"),
                        AutoResetEvent = new AutoResetEvent(false),
                        Error = false,
                        Timeout = false
                    }
                    );

                jobWatcher.Timer = new Timer(TimerCallBack, jobWatcher, 30000, 1000);

                jobWatcher.Processed.Created += WatcherProcessed_Created;
                jobWatcher.Processed.Renamed += Processed_Renamed;
                jobWatcher.Errors.Created += WatcherErrors_Created;

                jobWatcher.Processed.EnableRaisingEvents = true;
                jobWatcher.Errors.EnableRaisingEvents = true;
                _watchers.Add(jobWatcher);

                var enc1256 = System.Text.Encoding.GetEncoding(1256);// CodePagesEncodingProvider.Instance.GetEncoding("windows-1256");
                using (FileStream fs = new FileStream(queueTextFileName, FileMode.Create, FileAccess.Write, FileShare.None, 32768, FileOptions.WriteThrough))
                {
                    using (StreamWriter sw = new StreamWriter(fs, enc1256))
                    {
                        foreach (string line in lines)
                            sw.WriteLine(line);
                    }
                }
                jobWatcher.AutoResetEvent.WaitOne();

                jobWatcher.Processed.Dispose();
                jobWatcher.Errors.Dispose();
                jobWatcher.Timer.Dispose();
                _watchers.Remove(jobWatcher);

                if (jobWatcher.Timeout)
                {
                    exceptionStr = "لطفا مطمئن شوید برنامه TadRepPdf روی سرور در حال اجراست.";
                    return null;
                }

                if (jobWatcher.Error)
                {
                    exceptionStr = "برنامه چاپ سرور روی چاپ این فرم خطا داده است.";
                    return null;
                }

                Task.Delay(TadbirPrintAccessFileDelay).Wait(); //برای جلوگیری از تداخل با TadRepPdf

                return Path.Combine(ProcessedPath, Path.GetFileNameWithoutExtension(queueTextFileName) + ".pdf");

            }
            catch (Exception exp)
            {
                exceptionStr = exp.ToString();
                return null;
            }
        }

        private class JobWatcher
        {
            public FileSystemWatcher Processed { get; set; }
            public FileSystemWatcher Errors { get; set; }
            public Timer Timer { get; set; }
            public AutoResetEvent AutoResetEvent { get; set; }
            public bool Error { get; set; }
            public bool Timeout { get; set; }
            public string PdfFileName { get; set; }
        }

        private List<JobWatcher> _watchers = new List<JobWatcher>();

        // This method is called by the timer delegate.
        private void TimerCallBack(Object stateInfo)
        {
            JobWatcher jobWatcher = (JobWatcher)stateInfo;
            jobWatcher.Timeout = true;
            jobWatcher.AutoResetEvent.Set();
        }

        private void Processed_Renamed(object sender, RenamedEventArgs e)
        {
            FileSystemWatcher watcher = (FileSystemWatcher)sender;
            JobWatcher jobWatcher = _watchers.Where(w => w.Processed == watcher).FirstOrDefault();
            if (jobWatcher != null)
            {
                if (jobWatcher.PdfFileName == e.Name)
                {
                    jobWatcher.AutoResetEvent.Set();
                }
            }

        }


        private void WatcherProcessed_Created(object sender, FileSystemEventArgs e)
        {
            FileSystemWatcher watcher = (FileSystemWatcher)sender;
            JobWatcher jobWatcher = _watchers.Where(w => w.Processed == watcher).FirstOrDefault();
            if (jobWatcher != null)
            {
                if (jobWatcher.PdfFileName == e.Name)
                {
                    jobWatcher.AutoResetEvent.Set();
                }
            }
        }


        private void WatcherErrors_Created(object sender, FileSystemEventArgs e)
        {
            FileSystemWatcher watcher = (FileSystemWatcher)sender;
            JobWatcher jobWatcher = _watchers.Where(w => w.Errors == watcher).FirstOrDefault();
            if (jobWatcher != null)
            {
                jobWatcher.Error = true;
                jobWatcher.AutoResetEvent.Set();
            }
        }


        /// <summary>
        /// فایل پیشنهادی برای قرار گرفتن در صف چاپ
        /// </summary>
        private string GenerateJobFileName()
        {
            string queuePath = QueuePath;
            string suggestedFileName = Path.Combine(queuePath, Guid.NewGuid().ToString());
            while (File.Exists(suggestedFileName))
            {
                suggestedFileName = Path.Combine(queuePath, Guid.NewGuid().ToString());
            }
            return suggestedFileName;
        }

        /// <summary>
        /// مسیر خطا
        /// </summary>
        public string ErrorsPath
        {
            get
            {
                if (string.IsNullOrEmpty(TadbirPrintPath))
                    return "";
                return Path.Combine(TadbirPrintPath, "Errors");
            }
        }


        /// <summary>
        /// مسیر خروجی
        /// </summary>
        public string ProcessedPath
        {
            get
            {
                if (string.IsNullOrEmpty(TadbirPrintPath))
                    return "";
                return Path.Combine(TadbirPrintPath, "Processed");
            }
        }

        /// <summary>
        /// مسیر صف کارهای چاپ
        /// </summary>
        public string QueuePath
        {
            get
            {
                if (string.IsNullOrEmpty(TadbirPrintPath))
                    return "";
                return Path.Combine(TadbirPrintPath, "Queue");
            }
        }

        /// <summary>
        /// مسیر تعیین شده برای فایلهای چاپ
        /// </summary>
        private string TadbirPrintPath
        {
            get
            {
                return BasePrint.Properties.Settings.Default.TadbirPrintPath;
            }
        }

        private int TadbirPrintAccessFileDelay
        {
            get
            {
                return 500;
            }
        }
    }
}
