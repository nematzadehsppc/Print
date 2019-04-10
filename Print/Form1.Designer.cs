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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.acro_pdf_viewer = new AxAcroPDFLib.AxAcroPDF();
            this.btn_download_pdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.acro_pdf_viewer)).BeginInit();
            this.SuspendLayout();
            // 
            // acro_pdf_viewer
            // 
            this.acro_pdf_viewer.Enabled = true;
            this.acro_pdf_viewer.Location = new System.Drawing.Point(12, 12);
            this.acro_pdf_viewer.Name = "acro_pdf_viewer";
            this.acro_pdf_viewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("acro_pdf_viewer.OcxState")));
            this.acro_pdf_viewer.Size = new System.Drawing.Size(781, 446);
            this.acro_pdf_viewer.TabIndex = 0;
            // 
            // btn_download_pdf
            // 
            this.btn_download_pdf.Location = new System.Drawing.Point(12, 464);
            this.btn_download_pdf.Name = "btn_download_pdf";
            this.btn_download_pdf.Size = new System.Drawing.Size(137, 23);
            this.btn_download_pdf.TabIndex = 1;
            this.btn_download_pdf.Text = "Download PDF";
            this.btn_download_pdf.UseVisualStyleBackColor = true;
            this.btn_download_pdf.Click += new System.EventHandler(this.btn_download_pdf_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 499);
            this.Controls.Add(this.btn_download_pdf);
            this.Controls.Add(this.acro_pdf_viewer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.acro_pdf_viewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF acro_pdf_viewer;
        private System.Windows.Forms.Button btn_download_pdf;
    }
}

