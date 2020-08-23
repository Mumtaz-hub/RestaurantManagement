namespace RestaurantManagement
{
    partial class frmBillPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillPrint));
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.brnPrint = new System.Windows.Forms.Button();
            this.printdoc1 = new System.Drawing.Printing.PrintDocument();
            this.previewdlg = new System.Windows.Forms.PrintPreviewDialog();
            this.lblAddress = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(-2, 1);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(112, 24);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Laziz Pizza";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblAddress);
            this.panel1.Controls.Add(this.lblCompanyName);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 184);
            this.panel1.TabIndex = 1;
            // 
            // brnPrint
            // 
            this.brnPrint.Location = new System.Drawing.Point(273, 393);
            this.brnPrint.Name = "brnPrint";
            this.brnPrint.Size = new System.Drawing.Size(75, 23);
            this.brnPrint.TabIndex = 2;
            this.brnPrint.Text = "Print";
            this.brnPrint.UseVisualStyleBackColor = true;
            this.brnPrint.Click += new System.EventHandler(this.brnPrint_Click);
            // 
            // printdoc1
            // 
            this.printdoc1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printdoc1_PrintPage);
            // 
            // previewdlg
            // 
            this.previewdlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.previewdlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.previewdlg.ClientSize = new System.Drawing.Size(400, 300);
            this.previewdlg.Enabled = true;
            this.previewdlg.Icon = ((System.Drawing.Icon)(resources.GetObject("previewdlg.Icon")));
            this.previewdlg.Name = "previewdlg";
            this.previewdlg.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(-2, 25);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(207, 15);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "Lunsi Kui,Opp.Parsi Hospital.Navsari";
            // 
            // frmBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 472);
            this.ControlBox = false;
            this.Controls.Add(this.brnPrint);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmBillPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBillPrint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBillPrint_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button brnPrint;
        private System.Drawing.Printing.PrintDocument printdoc1;
        private System.Windows.Forms.PrintPreviewDialog previewdlg;
        private System.Windows.Forms.Label lblAddress;
    }
}