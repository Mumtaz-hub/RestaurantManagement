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
    public partial class ViewReport : Form
    {
        string strFile="";
        public ViewReport(string Filename)
        {
            InitializeComponent();
            strFile = Filename;
        }

        private void ViewReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void ViewReport_Load(object sender, EventArgs e)
        {
            try
            {
                //webBrowser1.Navigate(new Uri(strFile));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
    }
}
