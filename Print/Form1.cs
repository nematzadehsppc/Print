using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Activities;

namespace Print
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_download_pdf_Click(object sender, EventArgs e)
        {
            BasePrint.TadbirPrint tadbirPrint = new BasePrint.TadbirPrint();

            tadbirPrint.UserId = new InArgument<int>(1);
            tadbirPrint.WorkspaceId = new InArgument<int>(3);
            tadbirPrint.FPId = new InArgument<int>(1);
            tadbirPrint.SubsystemId = new InArgument<int>(4);
            tadbirPrint.ReportName = new InArgument<string>("");
            tadbirPrint.ParamTypes = new InArgument<string[]>();
            tadbirPrint.ParamValues = new InArgument<string[]>();

            System.Activities.WorkflowInvoker.Invoke<String>(tadbirPrint);
        }
    }
}
