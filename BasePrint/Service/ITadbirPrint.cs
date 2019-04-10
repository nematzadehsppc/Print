using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasePrint
{
    /// <summary>
    /// پردازش درخواستهای چاپ
    /// </summary>
    public interface ITadbirPrint
    {
        /// <summary>
        /// درخواست چاپ گزارش چاپی را پردازش می‌کند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workspaceId"></param>
        /// <param name="fpId"></param>
        /// <param name="subsystemId"></param>
        /// <param name="reportName"></param>
        /// <param name="paramTypes"></param>
        /// <param name="paramValues"></param>
        /// <param name="exceptionStr"></param>
        /// <returns>Output File Path</returns>
        string PrintReport(int userId, int workspaceId, int fpId, int subsystemId, string reportName, string[] paramTypes, string[] paramValues, out string exceptionStr);


        /*
        string QuickPrint(int userId, int workspaceId, int fpId, int subsystemId, string procName, string reportTitle, string[] paramValues, string[] paramTexts, bool[] paramHiddenStatuses, int[] paramTextWidths, int hiddenFieldCount, int[] columnWidthes, out string exceptionStr);


        /// <summary>
        /// لیست گزارشات هم‌ارز یک گزارش را برمی‌گرداند
        /// </summary>
        /// <param name="reportName"></param>
        /// <param name="exceptionStr"></param>
        /// <returns></returns>
        string[] GetReportVaraints(string reportName, out string exceptionStr);
        */


    }
}
