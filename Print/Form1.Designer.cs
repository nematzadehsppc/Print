namespace Print
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_download_pdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_download_pdf
            // 
            this.btn_download_pdf.Location = new System.Drawing.Point(101, 12);
            this.btn_download_pdf.Name = "btn_download_pdf";
            this.btn_download_pdf.Size = new System.Drawing.Size(101, 23);
            this.btn_download_pdf.TabIndex = 0;
            this.btn_download_pdf.Text = "download PDF";
            this.btn_download_pdf.UseVisualStyleBackColor = true;
            this.btn_download_pdf.Click += new System.EventHandler(this.btn_download_pdf_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_download_pdf);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_download_pdf;
    }
}

