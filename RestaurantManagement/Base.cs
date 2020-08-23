using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBLibrary;
using System.Data.SqlClient;

namespace RestaurantManagement
{
    public partial class Base : Form
    {
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        SqlDataAdapter _MainAdapter;
        DataTable _dt = new DataTable();
        DataSet dsMain = new DataSet();

        public enum Btn
        {
            Billing = 1,
            Configuration = 2,
            Report = 3,
            Logout=4
        }
        public Base(string formName = "frmBilling")
        {
            InitializeComponent();
            myTimer.Tick += new System.EventHandler(myTimer_Tick);
            
        }
        private void SelectedButtonNew(int selected)
        {
            btnBilling.BackColor = System.Drawing.Color.White;
            btnConfiguration.BackColor = System.Drawing.Color.White;
            btnReport.BackColor = System.Drawing.Color.White;
            btnBackupRestore.BackColor = System.Drawing.Color.White;
            
            switch (selected)
            {
                case (int)Btn.Billing:
                    btnBilling.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Configuration:
                    btnConfiguration.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Report:
                    btnReport.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Logout:
                    btnBackupRestore.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                
            }
        }
        string msg = @"@ Powered By MNS SoftTech,Contact No->91 9724035505                                                                                                                                     ";
        int pos = 0;
        //Timer tick event handler
        private void myTimer_Tick(object sender, System.EventArgs e)
        {
            if (pos < msg.Length)
            {
                txtMessage.AppendText(msg.Substring(pos, 1));
                ++pos;
            }
            else
            {
                //myTimer.Stop();
                pos = 0;
                txtMessage.Text = "";
            }

            //            txtMessage.Text = msg;

        }
        private void DispayButtonOnUserRight()
        {
            if (DBClass.UserType == "USER")
            {
                DataTable dtMenu = new DataTable();
                DataTable dtUright = new DataTable();
                dtMenu = DBClass.GetTableRecords("Menu_Master");

                dtUright = DBClass.GetTableByQuery("Select * from User_Rights where User_Id=" + DBClass.UserId);

                if (dtUright.Rows.Count > 0)
                {
                    for (int row = 0; row < dtUright.Rows.Count; row++)
                    {
                        int Uright = int.Parse(dtUright.Rows[row]["U_Right"].ToString());
                        int MCode = int.Parse(dtUright.Rows[row]["Menu_Code"].ToString());

                        if (dtMenu.Rows.Count > 0)
                        {
                            switch (MCode)
                            {
                                case 1:
                                    if (Uright == 1)
                                        btnBilling.Visible = true;
                                    else
                                        btnBilling.Visible = false;
                                    break;
                                case 2:
                                    if (Uright == 1)
                                        btnCategory.Visible = true;
                                    else
                                        btnCategory.Visible = false;
                                    break;
                                case 3:
                                    if (Uright == 1)
                                        btnItems.Visible = true;
                                    else
                                        btnItems.Visible = false;
                                    break;
                                case 4:
                                    if (Uright == 1)
                                        btnCustomer.Visible = true;
                                    else
                                        btnCustomer.Visible = false;
                                    break;
                                case 5:
                                    if (Uright == 1)
                                        btnMenuSettings.Visible = true;
                                    else
                                        btnMenuSettings.Visible = false;
                                    break;
                                case 6:
                                    if (Uright == 1)
                                        btnUserSettings.Visible = true;
                                    else
                                        btnUserSettings.Visible = false;
                                    break;
                                case 7:
                                    if (Uright == 1)
                                        btnTransfer.Visible = true;
                                    else
                                        btnTransfer.Visible = false;
                                    break;
                                case 8:
                                    if (Uright == 1)
                                        btnReport.Visible = true;
                                    else
                                        btnReport.Visible = false;
                                    break;
                                case 9:
                                    if (Uright == 1)
                                        btnBackupRestore.Visible = true;
                                    else
                                        btnBackupRestore.Visible = false;
                                    break;
                            }
                        }
                    }
                }
            }
        }
        private void Base_Load(object sender, EventArgs e)
        {
            DispayButtonOnUserRight();

            myTimer.Interval = 300;
            //Start timer
            myTimer.Start();
            lblDate.Text = System.DateTime.Now.ToShortDateString();

            FillGridBill();

            SelectedButtonNew((int)Btn.Billing);
            this.pnlMiddle.Controls.Clear();
            frmBilling obj = new frmBilling(this,BillNo);
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Generate Bill";
            //FrmLogin obj=new FrmLogin;
            //if (FrmLogin.Username=="A")
            //    btnTransfer.Visible=true;
            //else
            //    btnTransfer.Visible=false;

            pnlLeft2.Visible = false;
            
        }
        private void btnBilling_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            SelectedButtonNew((int)Btn.Billing);
            this.pnlMiddle.Controls.Clear();
            frmBilling obj = new frmBilling(this,BillNo);
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Generate Bill";
        }
        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            if (pnlLeft2.Visible == true)
                pnlLeft2.Visible = false;
            else
                pnlLeft2.Visible = true;

            SelectedButtonNew((int)Btn.Configuration);
            this.pnlMiddle.Controls.Clear();
            
        }
        private void btnItems_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            SelectedButtonNew((int)Btn.Configuration);
            this.pnlMiddle.Controls.Clear();
            ItemMaster obj = new ItemMaster();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Item Detail";
        }
        private void btnCategory_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            SelectedButtonNew((int)Btn.Configuration);
            this.pnlMiddle.Controls.Clear();
            ItemCategory obj = new ItemCategory();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Add/Edit/Delete Category";
        }

        public void RefreshGridBill()
        {
            string Sql = @"Select M.Bill_No as 'BillNo',M.TableNo,SUM(d.UnitPrice*d.Qty) as 'Amount',
                            SUM(d.UnitPrice*d.Qty)-Discount+Delivery_Charge as 'NetAmount',M.Bill_Print
                            from Bill_Master M
                            inner join Bill_Trans d on M.Bill_No=d.Bill_No
                            where Bill_Date='" + System.DateTime.Now.ToShortDateString() + @"'
                            group By M.Bill_No,M.TableNo,M.Bill_Print,M.Delivery_Charge,M.Discount
                            Order By M.Bill_No Desc";

            if (dsMain.Tables.Contains("Bill"))
                dsMain.Tables["Bill"].Clear();

            _MainAdapter = DBClass.GetAdapterByQuery(Sql);
            _MainAdapter.Fill(dsMain.Tables["Bill"]);

        }
        public void FillGridBill()
        {

            string Sql = @"Select M.Bill_No as 'BillNo',M.TableNo,SUM(d.UnitPrice*d.Qty) as 'Amount',
                            SUM(d.UnitPrice*d.Qty)-Discount+Delivery_Charge as 'NetAmount',M.Bill_Print
                            from Bill_Master M
                            inner join Bill_Trans d on M.Bill_No=d.Bill_No
                            where Bill_Date='" + System.DateTime.Now.ToShortDateString() + @"'
                            group By M.Bill_No,M.TableNo,M.Bill_Print,M.Delivery_Charge,M.Discount
                            Order By M.Bill_No Desc";

            if (dsMain.Tables.Contains("Bill"))
                dsMain.Tables["Bill"].Clear();

            _MainAdapter = DBClass.GetAdapterByQuery(Sql);
            _MainAdapter.Fill(_dt);
            dsMain.Tables.Add(_dt);
            dsMain.Tables[0].TableName = "Bill";
            
            if(dgvBill.DataSource==null)
                dgvBill.DataSource = dsMain.Tables["Bill"];

            dgvBill.Columns["BillNo"].HeaderText = "No";
            dgvBill.Columns["BillNo"].Width = 50;
            dgvBill.Columns["BillNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBill.Columns["TableNo"].HeaderText = "Table";
            dgvBill.Columns["TableNo"].Width = 50;
            dgvBill.Columns["TableNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBill.Columns["NetAmount"].Width = 130;
            dgvBill.Columns["NetAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBill.Columns["NetAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvBill.Columns["NetAmount"].HeaderText = "Amount";

            dgvBill.Columns["Bill_Print"].Visible = false;
            dgvBill.Columns["Amount"].Visible = false;

            dgvBill.ReadOnly = true;
            //dgvBill.Enabled = false;
        }

        private void Base_Validated(object sender, EventArgs e)
        {
            //FillGridBill();
           // MessageBox.Show("Validated");
        }

        int BillNo;
        private void dgvBill_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvBill_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dgvBill_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((dgvBill.SelectedRows.Count != 0) && (dgvBill.SelectedRows[0].Cells[0].Value != null) && (Convert.ToString(dgvBill.SelectedRows[0].Cells[0].Value) != ""))
            {
                BillNo = int.Parse(dgvBill.SelectedRows[0].Cells["BillNo"].Value.ToString());

                //if (dgvBill.Rows[e.RowIndex].Cells["BillNo"].Value != null)
                //{
                //  BillNo = dgvBill.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                SelectedButtonNew((int)Btn.Billing);
                this.pnlMiddle.Controls.Clear();
                frmBilling obj = new frmBilling(this, BillNo);
                obj.TopLevel = false;
                obj.AutoScroll = true;
                this.pnlMiddle.Controls.Add(obj);
                obj.Show();
                lblPageHead.Text = "Generate Bill-" + BillNo;
            }
        }

        private void dgvBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in dgvBill.Rows)
            {
                if ((Myrow.Cells["Bill_Print"].Value.ToString() !="") && Convert.ToInt32(Myrow.Cells["Bill_Print"].Value) == 1)// Or your condition 
                {
                    Myrow.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            SelectedButtonNew((int)Btn.Configuration);
            this.pnlMiddle.Controls.Clear();
            Transfer obj = new Transfer();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Transfer Data";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            this.pnlMiddle.Controls.Clear();
            Report obj = new Report();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Report Summary";
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            SelectedButtonNew((int)Btn.Configuration);
            this.pnlMiddle.Controls.Clear();
            Customer obj = new Customer();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Customer Details";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackupRestore_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            this.pnlMiddle.Controls.Clear();
            BackupRestore obj = new BackupRestore();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Backup / Restore Database";
        }

        private void btnMenuSettings_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            this.pnlMiddle.Controls.Clear();
            MenuSettings obj = new MenuSettings();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "Menu Settings";
        }

        private void btnUserSettings_Click(object sender, EventArgs e)
        {
            pnlLeft2.Visible = false;
            this.pnlMiddle.Controls.Clear();
            frmUserSettings obj = new frmUserSettings();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
            lblPageHead.Text = "User Settings";
        }
    }
}
