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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }

        SqlDataAdapter _MainAdapter;
        DataTable _dt = new DataTable();
        DataSet dsMain = new DataSet();
        
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dgvBill.Rows.Count; row++)
            {
                
                //DBClass.ExecuteNonQuery("Update Bill_Mast Set Bill_T=" + int.Parse(dgvBill.Rows[row].Cells["Bill_T"].Value.ToString()) + "  where Bill_No=" + int.Parse(dgvBill.Rows[row].Cells["Bill_No"].Value.ToString()));
                DBClass.connection.Open();
                _MainAdapter.UpdateCommand = new SqlCommand(@"update Bill_Master set Bill_T=@Bill_T 
                                                                where Bill_No= @Bill_No ", DBClass.connection);

                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_No", int.Parse(dgvBill.Rows[row].Cells["Bill_No"].Value.ToString()));
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_T", int.Parse(dgvBill.Rows[row].Cells["Bill_T"].Value.ToString()));

                _MainAdapter.UpdateCommand.ExecuteNonQuery();
                DBClass.connection.Close();
            }

            MessageBox.Show("Transfer Successfully");

            FillGridBill();
            CalculateToatl();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            FillGridBill();
            CalculateToatl();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            Set_Grid();
        }

        public void FillGridBill()
        {
            //(select sum(Qty*UnitPrice) from Bill_Trans where Bill_No =M.Bill_No) as 'Amount',
                            //(select sum(Qty*UnitPrice) from Bill_Trans where Bill_No =M.Bill_No) -M.Discount+M.Delivery_Charge as 'NetAmount'

            string Sql = @"select M.Bill_No,M.bill_date,M.Bill_T,M.Amount,M.Amount-M.Discount+M.Delivery_Charge as 'NetAmount'
                            from Bill_Master M where 1=1";

                        if (txtFromBillDate.Text != "")
                        {
                            Sql = Sql + " and M.Bill_Date>='" + txtFromBillDate.Text + "'";
                        }
                        if (txtToBillDate.Text != "")
                        {
                            Sql = Sql + " and M.Bill_Date<='" + txtToBillDate.Text + "'";
                        }

            Sql = Sql + " Order By M.Bill_Date,M.Bill_No";


            if (dsMain.Tables.Contains("Bill"))
                dsMain.Tables["Bill"].Clear();

            
            _MainAdapter = DBClass.GetAdapterByQuery(Sql);
            if (dsMain.Tables.Contains("Bill"))
                 _MainAdapter.Fill(dsMain.Tables["Bill"]);
            else
            {
                _MainAdapter.Fill(_dt);
                dsMain.Tables.Add(_dt);
            }

            dsMain.Tables[0].TableName = "Bill";

            if (dgvBill.DataSource == null)
                dgvBill.DataSource = dsMain.Tables["Bill"];

            dgvBill.Columns["Bill_T"].Width = 80;
            dgvBill.Columns["Bill_No"].Width = 80;
            dgvBill.Columns["Bill_Date"].Width = 120;
            dgvBill.Columns["NetAmount"].Width = 120;
        }

        private void CalculateToatl()
        {
            decimal TAmt = 0,SelTAmt=0;

            
            for(int row=0;row<dgvBill.Rows.Count;row++)       
            {
                if (dgvBill.Rows[row].Cells["NetAmount"].Value.ToString() != "")
                {
                    TAmt += decimal.Parse(dgvBill.Rows[row].Cells["NetAmount"].Value.ToString());
                    if (dgvBill.Rows[row].Cells["Bill_T"].Value.ToString() == "1")
                    {
                        SelTAmt += decimal.Parse(dgvBill.Rows[row].Cells["NetAmount"].Value.ToString());
                    }
                }
            }

            lblTotal.Text = TAmt.ToString();
            lblSelTAmt.Text = SelTAmt.ToString();
        }

        private void Set_Grid()
        {
            dgvBill.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn Bill_T = new DataGridViewCheckBoxColumn();
            Bill_T.Name = "Bill_T";
            Bill_T.DataPropertyName = "Bill_T";
            Bill_T.HeaderText = "";
            Bill_T.ReadOnly = false;
            Bill_T.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBill.Columns.Add(Bill_T);

            DataGridViewTextBoxColumn Bill_No = new DataGridViewTextBoxColumn();
            Bill_No.Name = "Bill_No";
            Bill_No.DataPropertyName = "Bill_No";
            Bill_No.HeaderText = "Bill No";
            Bill_No.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Bill_No.ReadOnly = true;
            dgvBill.Columns.Add(Bill_No);

            DataGridViewTextBoxColumn BillDate = new DataGridViewTextBoxColumn();
            BillDate.Name = "Bill_Date";
            BillDate.DataPropertyName = "Bill_Date";
            BillDate.HeaderText = "Date";
            BillDate.ReadOnly = true;
            BillDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBill.Columns.Add(BillDate);

            DataGridViewTextBoxColumn NetAmount = new DataGridViewTextBoxColumn();
            NetAmount.Name = "NetAmount";
            NetAmount.DataPropertyName = "NetAmount";
            NetAmount.HeaderText = "Net Amount";
            NetAmount.ReadOnly = true;
            NetAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBill.Columns.Add(NetAmount);

            DataGridViewTextBoxColumn Amount = new DataGridViewTextBoxColumn();
            Amount.Name = "Amount";
            Amount.DataPropertyName = "Amount";
            Amount.HeaderText = "Amount";
            Amount.ReadOnly = true;
            Amount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBill.Columns.Add(Amount);
            dgvBill.Columns["Amount"].Visible = false;
            
        }

        private void Transfer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvBill_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.RowIndex)
            if (dsMain.HasChanges())
            {
                CalculateToatl();
            }
            else
                return;
        }

        private void btnSelectBill_Click(object sender, EventArgs e)
        {
            SelectedBill();
        }

        private void SelectedBill()
        {
            decimal  SelTAmt = 0;

            for (int row = 0; row < dgvBill.Rows.Count; row++)
            {
                SelTAmt += decimal.Parse(dgvBill.Rows[row].Cells["NetAmount"].Value.ToString());
                dgvBill.Rows[row].Cells["Bill_T"].Value = 1;
            }

            //lblTotal.Text = TAmt.ToString();
            lblSelTAmt.Text = SelTAmt.ToString();
        }

    }
}
