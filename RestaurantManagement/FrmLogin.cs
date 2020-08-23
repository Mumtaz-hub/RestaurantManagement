using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBLibrary;
namespace RestaurantManagement
{
    public partial class FrmLogin : Form
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public FrmLogin()
        {
            InitializeComponent();
        }
        public static string Username = "";
        private void button2_Click(object sender, EventArgs e)
        {
            //frmChangePwd obj = new frmChangePwd();
            //obj.ShowDialog();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool CheckUser = false;

            
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please Enter User Name..", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }

            if (txtpassword.Text == "")
            {
                MessageBox.Show("Please Enter New Password..", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpassword.Focus();
                return;
            }

            DBClass.SetConnectionString();

            if (txtSecretPwd.Text == "2713")
            {
                DBClass.AddUser(txtUserName.Text, txtpassword.Text);
            }

            dt = DBClass.GetTableRecords("User_Master");
            ds = new DataSet();
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                        DBClass.UserId = DBClass.GetUserIdByUsernameAndPassword(txtUserName.Text,txtpassword.Text);
                        DBClass.UserName = txtUserName.Text;
                        DBClass.UserType = DBClass.GetColValueByQuery("Select User_Type from User_Master where User_Id=" + DBClass.UserId);
                        if(DBClass.UserId>0)
                            CheckUser = true;
                
            }
            
            if (CheckUser)
            {
                Username=txtUserName.Text;
                this.Hide();
                Base obj = new Base();
                obj.ShowDialog();
                

            }
            else
            {
                
                MessageBox.Show("Invalid Username or Password", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Text = "";
                txtpassword.Text = "";
                txtUserName.Focus();
                //txtpassword.Focus();
                return;
            }


            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

            DateTime date1 = System.DateTime.Now;
            DateTime date2 = new DateTime(2019, 7, 10,10,0,0);
            int result = DateTime.Compare(date1, date2);

            if (result == 1)
            {
                MessageBox.Show("Software copy Of Reastaurant is Expired,Contact MNS SoftTech");
                this.Close();
            }

        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
