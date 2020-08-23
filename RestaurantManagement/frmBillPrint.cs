using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestaurantManagement
{
    public partial class frmBillPrint : Form
    {
        public frmBillPrint()
        {
            InitializeComponent();
        }

        private void frmBillPrint_Load(object sender, EventArgs e)
        {
            GetPrintArea(panel1);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
            this.Close();
            //this.reportViewer1.RefreshReport();
        }

        private void frmBillPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        //Rest of the code
        Bitmap MemoryImage;
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }

        private void printdoc1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }

        //public void Print(Panel pnl)
        //{
        //    Panel pannel = pnl;
        //    GetPrintArea(pnl);
        //    previewdlg.Document = printdoc1;
        //    previewdlg.ShowDialog();
        //}

        private void brnPrint_Click(object sender, EventArgs e)
        {
            //Print(this.panel1);
        }
    }
}
