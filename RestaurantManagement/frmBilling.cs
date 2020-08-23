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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

namespace RestaurantManagement
{
    public partial class frmBilling : Form
    {
        private readonly Base objBase;
        int MBillNo;
        ReportClass ReportClassObj = new ReportClass();
        static DataTable dt = new DataTable();
        public frmBilling(Base Parent,int BillNo)
        {
            InitializeComponent();
            objBase = Parent;
            MBillNo = BillNo;
        }

        #region Common Variables
        // Common Variables///////////////////////
        DataSet dsForCombo = new DataSet();
        DataTable _DataTable;
        DataSet dsMain = new DataSet();
        SqlDataAdapter _CustomerAdapter;
        SqlDataAdapter _MainAdapter;
        SqlDataAdapter _MainDTAdapter;
        //int IntRowIndex = 0;
        int maxDetailid = 0;
        #endregion

        #region Form Events
        private void frmBilling_Load(object sender, EventArgs e)
        {
            AutoComplete_textbox();
            DisplayRecord();
            
           // NewRecord();
            Enable_Disable_Controls(true);

        }
        private void frmBilling_KeyDown(object sender, KeyEventArgs e)
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
        #endregion

        #region Fill All Records
        private void AutoComplete_textbox()
        {
            //Customer Name
            _DataTable = new DataTable();

            _CustomerAdapter = DBClass.GetAdapterByQuery("Select * from Customer_Master");
            _CustomerAdapter.Fill(_DataTable);
            dsForCombo.Tables.Add(_DataTable);
            dsForCombo.Tables[0].TableName = "Customer_Master";

            AutoCompleteStringCollection strCustomerName = new AutoCompleteStringCollection();
            for (int i = 0; i < dsForCombo.Tables["Customer_Master"].Rows.Count; i++)
            {
                strCustomerName.Add(dsForCombo.Tables["Customer_Master"].Rows[i]["Customer_Name"].ToString());
            }

            txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtName.AutoCompleteCustomSource = strCustomerName;
            txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            ////Item Short Code
            _DataTable = DBClass.GetTableByQuery("Select * from Item_Master");
            dsForCombo.Tables.Add(_DataTable);
            dsForCombo.Tables[1].TableName = "Item_Master";

            AutoCompleteStringCollection strItemShortCode = new AutoCompleteStringCollection();
            AutoCompleteStringCollection strItemName = new AutoCompleteStringCollection();

            for (int i = 0; i < dsForCombo.Tables["Item_Master"].Rows.Count; i++)
            {
                strItemShortCode.Add(dsForCombo.Tables["Item_Master"].Rows[i]["Short_Code"].ToString());
                strItemName.Add(dsForCombo.Tables["Item_Master"].Rows[i]["Item_Name"].ToString());
            }

            txtSearchShortCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchShortCode.AutoCompleteCustomSource = strItemShortCode;
            txtSearchShortCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = strItemName;
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }
        private void DisplayRecord()
        {
            _MainAdapter = DBClass.GetAdaptor("Bill_Master");
            _DataTable = new DataTable();
            _MainAdapter.Fill(_DataTable);

            dsMain.Tables.Add(_DataTable);
            dsMain.Tables[0].TableName = "Bill_Master";

            _MainDTAdapter = DBClass.GetAdaptor("Bill_Trans");
            _DataTable = new DataTable();
            _MainDTAdapter.Fill(_DataTable);

            dsMain.Tables.Add(_DataTable);
            dsMain.Tables[1].TableName = "Bill_Trans";

            DataColumn TColumn = new DataColumn();
            TColumn.DataType = System.Type.GetType("System.Decimal");
            TColumn.ColumnName = "Total";
            TColumn.Expression = "UnitPrice * Qty";
            dsMain.Tables["Bill_Trans"].Columns.Add(TColumn);

            
            Set_Grid();
            FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
            
        }
        private void DisplayDetails(string BillNo)
        {
            dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = "";
            dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = " Bill_No=" + int.Parse(BillNo);

            dgvDetails.Rows.Clear();
            DataTable dt = new DataTable();
            dt = dsMain.Tables["Bill_Trans"].DefaultView.ToTable();
            if (dt.Rows.Count > 0)
            {
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    string ItemName = "";
                    string Item = dt.Rows[row]["Item_Code"].ToString();
                    dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "";
                    dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "Item_Code="+int.Parse(Item);

                    DataTable dtItem = new DataTable();
                    dtItem = dsForCombo.Tables["Item_Master"].DefaultView.ToTable();
                    if (dtItem.Rows.Count > 0)
                    {
                        ItemName = dtItem.Rows[0]["Short_Code"].ToString()+" "+dtItem.Rows[0]["Item_Name"].ToString();
                    }

                    decimal Amt = int.Parse(dt.Rows[row]["Qty"].ToString()) * decimal.Parse(dt.Rows[row]["UnitPrice"].ToString());
                    dgvDetails.Rows.Add(dt.Rows[row]["DetailId"].ToString(), dt.Rows[row]["Bill_No"].ToString(), ItemName, dt.Rows[row]["UnitPrice"].ToString(), dt.Rows[row]["Qty"].ToString(), dt.Rows[row]["Total"].ToString());    
                }
                
            }

            //dsMain.Tables["OrderDetail"].DefaultView.RowFilter = string.Concat("CONVERT(", "OrderID", "System.String) LIKE '%", OrderID, "%'");
            //dgvDetails.DataSource = dsMain.Tables["Bill_Trans"].DefaultView;

            CalculateTotal();
        }
        private void CalculateTotal()
        {
            //dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = "";
            //dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = " Bill_No=" + int.Parse(txtBillNo.Text);

            if (dgvDetails.Rows.Count >= 1)
            {
                DataTable DT = new DataTable();
                DT = dsMain.Tables["Bill_Trans"].DefaultView.ToTable();
                if (DT.Rows.Count > 0)
                {
                    decimal TotalOrder = DT.AsEnumerable().Sum(r => r.Field<decimal?>("Total") ?? 0);
                    // decimal TotalOrder = decimal.Parse(DT.Compute("sum(Total)", "").ToString());
                    txttotalPrice.Text = TotalOrder.ToString();

                    string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                    string DeliveryAmt = (txtDeliveryCharge.Text == "") ? "0" : txtDeliveryCharge.Text;

                    decimal NetAmt = TotalOrder + Convert.ToDecimal(DeliveryAmt) - Convert.ToDecimal(DiscAmt);
                    txtnetamount.Text = NetAmt.ToString();
                }
                else
                {
                    txttotalPrice.Text = "0";
                    txtnetamount.Text = "0";

                }
            }
            else
            {
                txttotalPrice.Text = "0";
                txtnetamount.Text = "0";
            }
        }
        private void FillBill(int Index)
        {
            int rowIndex;
            if (MBillNo > 0)
            {
                DataRow[] row = dsMain.Tables["Bill_Master"].Select("Bill_No=" + MBillNo);
                if (row.Length != 0)
                {
                    rowIndex = dsMain.Tables["Bill_Master"].Rows.IndexOf(row[0]);
                    Index = rowIndex;
                }
            }

            if (dsMain.Tables["Bill_Master"].Rows.Count > 0)
            {
                txtBillNo.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Bill_No"].ToString();
                txtBillDate.Text = Convert.ToDateTime(dsMain.Tables["Bill_Master"].Rows[Index]["Bill_Date"].ToString()).ToShortDateString();
                DateTime x = DateTime.Parse(dsMain.Tables["Bill_Master"].Rows[Index]["Bill_Time"].ToString());
                txtBillTime.Text = x.ToString("HH:mm");
                //txtBillTime.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Bill_Time"].ToString();
                txtBillType.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Bill_Type"].ToString();
                txtTableNo.Text = dsMain.Tables["Bill_Master"].Rows[Index]["TableNo"].ToString();
                txtPerson.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Person"].ToString();
                txtCustomerCode.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Customer_Code"].ToString();
                txtDiscount.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Discount"].ToString();
                txtDeliveryCharge.Text = dsMain.Tables["Bill_Master"].Rows[Index]["Delivery_Charge"].ToString();

                if (txtCustomerCode.Text != "")
                {
                    dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "";
                    dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "Customer_Code=" + int.Parse(txtCustomerCode.Text) + "";

                    DataTable DT = new DataTable();
                    DT = dsForCombo.Tables["Customer_Master"].DefaultView.ToTable();
                    if (DT.Rows.Count > 0)
                    {
                        txtName.Text = DT.Rows[0]["Customer_Name"].ToString();
                        txtPhone.Text = DT.Rows[0]["Customer_Phone"].ToString();
                        txtAddress.Text = DT.Rows[0]["Customer_Address"].ToString();
                        txtLocality.Text = DT.Rows[0]["Customer_Locality"].ToString();
                    }
                }
                DisplayDetails(txtBillNo.Text);
            }
        }
        private void Set_Grid()
        {
            dgvDetails.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn DetailId = new DataGridViewTextBoxColumn();
            DetailId.Name = "DetailId";
            DetailId.HeaderText = "DetailId";
            DetailId.ReadOnly = true;
            dgvDetails.Columns.Add(DetailId);

            DataGridViewTextBoxColumn BillNo = new DataGridViewTextBoxColumn();
            BillNo.Name = "Bill_No";
            BillNo.HeaderText = "Bill_No";
            BillNo.ReadOnly = true;
            dgvDetails.Columns.Add(BillNo);


            DataGridViewTextBoxColumn ItemColumn = new DataGridViewTextBoxColumn();
            ItemColumn.Name = "Item_Name";
            ItemColumn.HeaderText = "Item Name";
            ItemColumn.ReadOnly = false;
            dgvDetails.Columns.Add(ItemColumn);


            DataGridViewTextBoxColumn UnitPriceColumn = new DataGridViewTextBoxColumn();
            UnitPriceColumn.Name = "UnitPrice";
            UnitPriceColumn.HeaderText = "UnitPrice";
            UnitPriceColumn.ReadOnly = false;
            UnitPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetails.Columns.Add(UnitPriceColumn);

            DataGridViewTextBoxColumn QtyColumn = new DataGridViewTextBoxColumn();
            QtyColumn.Name = "Qty";
            //QtyColumn.DataPropertyName = "Qty";
            QtyColumn.HeaderText = "Qty";
            QtyColumn.ReadOnly = false;
            QtyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetails.Columns.Add(QtyColumn);

            DataGridViewTextBoxColumn TotalColumn = new DataGridViewTextBoxColumn();
            TotalColumn.Name = "Total";
            //TotalColumn.DataPropertyName = "Total";
            TotalColumn.HeaderText = "Amount";
            TotalColumn.ReadOnly = true;
            TotalColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetails.Columns.Add(TotalColumn);
            
            //dgvDetails.DataSource = dsMain.Tables["Bill_Trans"];
            dgvDetails.Columns["DetailId"].Visible = false;
            dgvDetails.Columns["Bill_No"].Visible = false;
            dgvDetails.Columns["Total"].Visible = true;
            dgvDetails.Columns["Item_Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetails.ReadOnly = true;

        }
        #endregion

        #region Transaction Functions
        private void Enable_Disable_Controls(bool Val)
        {
            
        }
        private void ClearRecord()
        {
            txtBillNo.Text = "";
            txtBillDate.Text = "";
            txtBillTime.Text = "";
            txtBillType.Text = "";

            rbDelivery.Checked = false;
            rbPickup.Checked = false;
            rbDineIn.Checked = true;

            txtTableNo.Text = "";
            txtPerson.Text = "";
            txtPhone.Text = "";
            txtName.Text = "";
            txtCustomerCode.Text = "";
            txtAddress.Text = "";
            txtLocality.Text = "";

            txttotalPrice.Text = "0";
            txtDiscount.Text = "0";
            txtDeliveryCharge.Text = "0";
            txtnetamount.Text = "0";
        }
        private void NewRecord()
        {
            ClearRecord();

            int maxid = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('Bill_Master')+1");
            txtBillNo.Text = maxid.ToString();

            txtBillDate.Text = System.DateTime.Now.ToShortDateString();
            txtBillTime.Text = System.DateTime.Now.ToString("HH:mm");

            txtBillType.Text = "DineIn";

            if (dsForCombo.Tables["Customer_Master"].Rows.Count > 0)
            {
                //txtCustmorid.Text = dsForCombo.Tables["Customer"].Rows[0]["CustomerID"].ToString();
                //cmbcustomername.SelectedValue = txtCustmorid.Text;
            }

            dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = "";
            dsMain.Tables["Bill_Trans"].DefaultView.RowFilter = " Bill_No=" + int.Parse(txtBillNo.Text);
            dgvDetails.Rows.Clear();

            objBase.RefreshGridBill();
            objBase.lblPageHead.Text = "Generate Bill-" + txtBillNo.Text;

            maxDetailid = 0;
            txtTableNo.Focus();
        }
        private void DeleteRecord()
        {
            if (MessageBox.Show("Are you Sure to delete this record ? ", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable dt = DBClass.GetTableByQuery("Select count(*) from Bill_Trans where Bill_No=" + Convert.ToInt16(txtBillNo.Text));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("Bill Details Exist of this order,Can't be delete!");
                        return;
                    }
                }
                DBClass.connection.Open();

                _MainDTAdapter.DeleteCommand = new SqlCommand(@"Delete From Bill_Trans where Bill_No= @Bill_No ", DBClass.connection);
                _MainDTAdapter.DeleteCommand.Parameters.AddWithValue("@Bill_No", txtBillNo.Text);
                _MainDTAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["Bill_Trans"].Clear();
                _MainDTAdapter.Fill(dsMain.Tables["Bill_Trans"]);

                _MainAdapter.DeleteCommand = new SqlCommand(@"Delete From Bill_Master where Bill_No= @Bill_No ", DBClass.connection);
                _MainAdapter.DeleteCommand.Parameters.AddWithValue("@Bill_No", txtBillNo.Text);
                _MainAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["Bill_Master"].Clear();
                _MainAdapter.Fill(dsMain.Tables["Bill_Master"]);

                DBClass.connection.Close();
                if (dsMain.Tables["Bill_Master"].Rows.Count > 0)
                    FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
                else
                    ClearRecord();
            }
        }
        private void SaveRecord()
        {

            DataRow[] row = dsMain.Tables["Bill_Master"].Select("Bill_No=" + int.Parse(txtBillNo.Text));
            if (row.Length == 0)
            {
                _MainAdapter.InsertCommand = new SqlCommand(@"insert into Bill_Master(Bill_Date,Bill_Time,Bill_Type,TableNo,Person,Customer_Code,Discount,Delivery_Charge,Bill_T,Amount) 
                                                                output inserted.Bill_No
                                                                Values(@Bill_Date,@Bill_Time,@Bill_Type,@TableNo,@Person,@Customer_Code,@Discount,@Delivery_Charge,@Bill_T,@Amount)", DBClass.connection);
                DBClass.connection.Open();

                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Date", txtBillDate.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Time",txtBillTime.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Type", txtBillType.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@TableNo",txtTableNo.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Person", int.Parse((txtPerson.Text == "") ? "0" : txtPerson.Text));
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Code", int.Parse((txtCustomerCode.Text == "") ? "0" : txtCustomerCode.Text));
                
                string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                string DeliveryAmt = (txtDeliveryCharge.Text == "") ? "0" : txtDeliveryCharge.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Delivery_Charge", decimal.Parse(DeliveryAmt));
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_T", 0)
                string TAmt = (txttotalPrice.Text == "") ? "0" : txttotalPrice.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Amount", decimal.Parse(TAmt));

                int id = (int)_MainAdapter.InsertCommand.ExecuteScalar();

                txtBillNo.Text = id.ToString();
                DBClass.connection.Close();
            }
            else
            {
                DBClass.connection.Open();
                _MainAdapter.UpdateCommand = new SqlCommand(@"update Bill_Master set Bill_Date=@Bill_Date,Bill_Time=@Bill_Time,Bill_Type=@Bill_Type,TableNo=@TableNo,
                                                                Person=@Person,Customer_Code=@Customer_Code,Discount=@Discount,Delivery_Charge=@Delivery_Charge,Bill_T=@Bill_T,Amount=@Amount 
                                                                where Bill_No= @Bill_No ", DBClass.connection);

                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_No", txtBillNo.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Date", txtBillDate.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Time", txtBillTime.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Type", txtBillType.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@TableNo", txtTableNo.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Person", int.Parse((txtPerson.Text == "") ? "0" : txtPerson.Text));
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Customer_Code", int.Parse((txtCustomerCode.Text == "") ? "0" : txtCustomerCode.Text));

                string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                string DeliveryAmt = (txtDeliveryCharge.Text == "") ? "0" : txtDeliveryCharge.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Delivery_Charge", decimal.Parse(DeliveryAmt));
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_T", 0);
                string TAmt = (txttotalPrice.Text == "") ? "0" : txttotalPrice.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Amount", decimal.Parse(TAmt));

                _MainAdapter.UpdateCommand.ExecuteNonQuery();
                DBClass.connection.Close();
            }

            dsMain.Tables["Bill_Master"].Clear();
            _MainAdapter.Fill(dsMain.Tables["Bill_Master"]);

            //////////////// Save Bill Detail ///////////

            
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(_MainDTAdapter);
            _MainDTAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
            _MainDTAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
            _MainDTAdapter.Update(dsMain.Tables["Bill_Trans"]);

            dsMain.Tables["Bill_Trans"].Clear();

            _MainDTAdapter = DBClass.GetAdaptor("Bill_Trans");
            _MainDTAdapter.Fill(dsMain.Tables["Bill_Trans"]);

            dsForCombo.Tables["Customer_Master"].Clear();
            _CustomerAdapter = DBClass.GetAdapterByQuery("Select * from Customer_Master");
            _CustomerAdapter.Fill(dsForCombo.Tables["Customer_Master"]);

            FillCustomerDetail(txtCustomerCode.Text);
            FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
            Enable_Disable_Controls(true);
            MessageBox.Show("Bill Save Successfully!");
        }
        #endregion

        #region Search Item
        private void txtSearchShortCode_TextChanged(object sender, EventArgs e)
        {
            dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "";
            dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "Short_Code='" + txtSearchShortCode.Text  +"'";

            DataTable DT = new DataTable();
            DT = dsForCombo.Tables["Item_Master"].DefaultView.ToTable();
            if (DT.Rows.Count > 0)
            {
                txtItemCode.Text = DT.Rows[0]["Item_Code"].ToString();
                txtSearchItem.Text = DT.Rows[0]["Item_Name"].ToString();
                txtUnitPrice.Text = DT.Rows[0]["UnitPrice"].ToString();
                txtQty.Text = "1";
            }
            else
            {
                txtItemCode.Text = "";
                txtSearchItem.Text ="";
                txtUnitPrice.Text = "";
                txtQty.Text = "";
            }
        }
        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "";
            dsForCombo.Tables["Item_Master"].DefaultView.RowFilter = "Item_Name='" + txtSearchItem.Text + "'";

            DataTable DT = new DataTable();
            DT = dsForCombo.Tables["Item_Master"].DefaultView.ToTable();
            if (DT.Rows.Count > 0)
            {
                txtItemCode.Text = DT.Rows[0]["Item_Code"].ToString();
                txtSearchShortCode.Text = DT.Rows[0]["Short_Code"].ToString();
                txtUnitPrice.Text = DT.Rows[0]["UnitPrice"].ToString();
                txtQty.Text = "1";
            }
            else
            {
                txtItemCode.Text = "";
                //txtSearchShortCode.Text = "";
                txtUnitPrice.Text = "";
                txtQty.Text = "";
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (rbDineIn.Checked  && txtTableNo.Text == "")
            {
                MessageBox.Show("Enter Table No");
                txtTableNo.Focus();
                return;
            }
            else if (rbDelivery.Checked)
            {
                if (txtPerson.Text == "")
                {
                    MessageBox.Show("Enter Customer Name");
                    txtPerson.Focus();
                    return;
                }
                if (txtPhone.Text == "")
                {
                    MessageBox.Show("Enter Phone No");
                    txtPhone.Focus();
                    return;
                }
                if (txtAddress.Text == "")
                {
                    MessageBox.Show("Enter Address");
                    txtAddress.Focus();
                    return;
                }
            }
            else if (rbPickup.Checked)
            {
                if (txtPerson.Text == "")
                {
                    MessageBox.Show("Enter Customer Name");
                    txtPerson.Focus();
                    return;
                }
            }

            dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "";
            dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "Customer_Name like '%" + txtName.Text + "%'";

            DataTable DT = new DataTable();
            DT = dsForCombo.Tables["Customer_Master"].DefaultView.ToTable();
            if (DT.Rows.Count == 0)
            {
                _CustomerAdapter.InsertCommand = new SqlCommand(@"insert into Customer_Master(Customer_Name,Customer_Address,Customer_Locality,Customer_Phone,Customer_Type) 
                                                                output inserted.Customer_Code
                                                                Values(@Customer_Name,@Customer_Address,@Customer_Locality,@Customer_Phone,@Customer_Type)", DBClass.connection);
                DBClass.connection.Open();

                _CustomerAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Name", txtName.Text);
                _CustomerAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Address", txtAddress.Text);
                _CustomerAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Locality", txtLocality.Text);
                _CustomerAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Phone", txtPhone.Text);
                _CustomerAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Type", "C");


                int id = (int)_CustomerAdapter.InsertCommand.ExecuteScalar();
                txtCustomerCode.Text = id.ToString();
                DBClass.connection.Close();

            }
            DataRow[] row = dsMain.Tables["Bill_Master"].Select("Bill_No=" + int.Parse(txtBillNo.Text));
            if (row.Length == 0)
            {
                _MainAdapter.InsertCommand = new SqlCommand(@"insert into Bill_Master(Bill_Date,Bill_Time,Bill_Type,TableNo,Person,Customer_Code,Discount,Delivery_Charge,Bill_T,Amount) 
                                                                output inserted.Bill_No
                                                                Values(@Bill_Date,@Bill_Time,@Bill_Type,@TableNo,@Person,@Customer_Code,@Discount,@Delivery_Charge,@Bill_T,@Amount)", DBClass.connection);
                DBClass.connection.Open();

                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Date", txtBillDate.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Time", txtBillTime.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_Type", txtBillType.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@TableNo", txtTableNo.Text);
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Person", int.Parse((txtPerson.Text == "") ? "0" : txtPerson.Text));
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Customer_Code", int.Parse((txtCustomerCode.Text == "") ? "0" : txtCustomerCode.Text));

                string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                string DeliveryAmt = (txtDeliveryCharge.Text == "") ? "0" : txtDeliveryCharge.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Delivery_Charge", decimal.Parse(DeliveryAmt));
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Bill_T",0);
                string TAmt = (txttotalPrice.Text == "") ? "0" : txttotalPrice.Text;
                _MainAdapter.InsertCommand.Parameters.AddWithValue("@Amount", decimal.Parse(TAmt));

                int id = (int)_MainAdapter.InsertCommand.ExecuteScalar();
                txtBillNo.Text = id.ToString();
                DBClass.connection.Close();

            }
            else
            {
                DBClass.connection.Open();
                _MainAdapter.UpdateCommand = new SqlCommand(@"update Bill_Master set Bill_Date=@Bill_Date,Bill_Time=@Bill_Time,Bill_Type=@Bill_Type,TableNo=@TableNo,
                                                                Person=@Person,Customer_Code=@Customer_Code,Discount=@Discount,Delivery_Charge=@Delivery_Charge,Bill_T=@Bill_T,Amount=@Amount 
                                                                where Bill_No= @Bill_No ", DBClass.connection);

                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_No", txtBillNo.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Date", txtBillDate.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Time", txtBillTime.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_Type", txtBillType.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@TableNo", txtTableNo.Text);
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Person", int.Parse((txtPerson.Text == "") ? "0" : txtPerson.Text));
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Customer_Code", int.Parse((txtCustomerCode.Text == "") ? "0" : txtCustomerCode.Text));

                string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                string DeliveryAmt = (txtDeliveryCharge.Text == "") ? "0" : txtDeliveryCharge.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Delivery_Charge", decimal.Parse(DeliveryAmt));
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_T", 0);
                string TAmt = (txttotalPrice.Text == "") ? "0" : txttotalPrice.Text;
                _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Amount", decimal.Parse(TAmt));

                _MainAdapter.UpdateCommand.ExecuteNonQuery();
                DBClass.connection.Close();
            }

            if (txtItemCode.Text != "")
            {
                dsMain.Tables["Bill_Master"].Clear();
                _MainAdapter.Fill(dsMain.Tables["Bill_Master"]);

                maxDetailid = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('Bill_Trans')+1");
                decimal Total = int.Parse(txtQty.Text) * decimal.Parse(txtUnitPrice.Text);

                dsMain.Tables["Bill_Trans"].Rows.Add(maxDetailid, int.Parse(txtBillNo.Text), int.Parse(txtItemCode.Text), int.Parse(txtQty.Text), decimal.Parse(txtUnitPrice.Text), Total);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(_MainDTAdapter);
                _MainDTAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                _MainDTAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                _MainDTAdapter.Update(dsMain.Tables["Bill_Trans"]);

                //string[] gridrow = new string[] {"" + maxDetailid + "",txtBillNo.Text,txtSearchItem.Text, txtQty.Text, txtUnitPrice.Text, ""+Total+"" };
                dgvDetails.Rows.Add(" /" + maxDetailid + "", txtBillNo.Text, txtSearchItem.Text, txtUnitPrice.Text, txtQty.Text, "" + Total + "");
            }
            CalculateTotal();

            txtSearchShortCode.Text = "";
            txtItemCode.Text = "";
            txtUnitPrice.Text = "";
            txtSearchItem.Text = "";
            txtSearchShortCode.Focus();
        }
        #endregion

        #region Handle Customer Textbox
        private void FillCustomerDetail(string Code)
        {
            if (Code!="" && dsForCombo.Tables["Customer_Master"].Rows.Count > 0)
            {
                DataRow[] row = dsForCombo.Tables["Customer_Master"].Select("Customer_Code=" + Code);
                if (row.Length != 0)
                {
                    if (!row[0].IsNull("Customer_Name"))
                    {
                        txtName.Text = row[0]["Customer_Name"].ToString();
                        txtAddress.Text = row[0]["Customer_Address"].ToString();
                        txtPhone.Text = row[0]["Customer_Phone"].ToString();
                        txtLocality.Text = row[0]["Customer_Locality"].ToString();
                    }
                }
            }
        }
        private void txtName_Validated(object sender, EventArgs e)
        {
            if (txtCustomerCode.Text == "" && txtName.Text != "")
            {
                int maxid = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('Customer_Master')+1");
                txtCustomerCode.Text = maxid.ToString();
                
            }
            else if (txtCustomerCode.Text != "" && txtName.Text != "")
            {
                FillCustomerDetail(txtCustomerCode.Text);
            }
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "";
            dsForCombo.Tables["Customer_Master"].DefaultView.RowFilter = "Customer_Name like '%" + txtName.Text + "%'";

            DataTable DT = new DataTable();
            DT = dsForCombo.Tables["Customer_Master"].DefaultView.ToTable();
            if (DT.Rows.Count > 0)
            {
                txtCustomerCode.Text = DT.Rows[0]["Customer_Code"].ToString();
                txtPhone.Text = DT.Rows[0]["Customer_Phone"].ToString();
                txtAddress.Text = DT.Rows[0]["Customer_Address"].ToString();

            }
            
        }
        #endregion

        #region Grid Events
        private void dgvDetails_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //if ((dgvDetails.Rows.Count != 0))
            //{
            //    e.Row.Cells["Bill_No"].Value = txtBillNo.Text;
            //    e.Row.Cells["DetailId"].Value = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('Bill_Trans')+1");
            //}
        }
        private void dgvDetails_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if ((dgvDetails.Columns[e.ColumnIndex].Name).ToUpper() == "ITEM_CODE")
            //{
            //    if (dsForCombo.Tables["Item_Master"].Rows.Count > 0 && dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            //    {
            //        DataRow[] row = dsForCombo.Tables["Item_Master"].Select("ITEM_CODE=" + int.Parse(dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
            //        if (row.Length != 0)
            //        {
            //            if (!row[0].IsNull("UnitPrice"))
            //            {
            //                dgvDetails.Rows[e.RowIndex].Cells["UnitPrice"].Value = row[0]["UnitPrice"].ToString();
            //            }
            //        }
            //    }
            //}
        }
        private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvDetails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DBClass.connection.Open();

            _MainDTAdapter.DeleteCommand = new SqlCommand(@"Delete From Bill_Trans where DetailId= @DetailId ", DBClass.connection);
            _MainDTAdapter.DeleteCommand.Parameters.AddWithValue("@DetailId",int.Parse(e.Row.Cells[0].Value.ToString()));
            _MainDTAdapter.DeleteCommand.ExecuteNonQuery();

            dsMain.Tables["Bill_Trans"].Clear();
            _MainDTAdapter.Fill(dsMain.Tables["Bill_Trans"]);

            CalculateTotal();
            DBClass.connection.Close();
        }
        #endregion

        #region BillType Radio button
        private void rbDineIn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDineIn.Checked == true)
            {
                txtBillType.Text = "DineIn";
            }
        }
        private void rbPickup_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPickup.Checked == true)
            {
                txtBillType.Text = "Pickup";
            }
        }
        private void rbDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDelivery.Checked == true)
            {
                txtBillType.Text = "Delivery";
            }
        }
        #endregion

        private void txtDiscount_Validated(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtDeliveryCharge_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void btnNewBill_Click(object sender, EventArgs e)
        {
            NewRecord();
            btnNewBill.Enabled = false;
            btnCancel.Visible = true;
        }

        private void frmBilling_Activated(object sender, EventArgs e)
        {
           // objBase.FillGridBill();
            //objBase.lblPageHead.Text = "Generate Bill-" + txtBillNo.Text;
        }

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            DBClass.connection.Open();

            _MainAdapter.UpdateCommand = new SqlCommand(@"Update Bill_Master set Bill_Print=1 where Bill_No= @Bill_No ", DBClass.connection);
            _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Bill_No",int.Parse(txtBillNo.Text));
            _MainAdapter.UpdateCommand.ExecuteNonQuery();

            dsMain.Tables["Bill_Master"].Clear();
            //_MainAdapter = DBClass.GetAdaptor("Bill_Master");
            _MainAdapter.Fill(dsMain.Tables["Bill_Master"]);

            DBClass.connection.Close();

            PrintBillUsingPrintDocument();
            //BillInExcel();
            //frmBillPrint obj = new frmBillPrint();
            //obj.ShowDialog();

            objBase.RefreshGridBill();
        }

        #region PrintInPdf
        //using iTextSharp.text;
        //using iTextSharp.text.pdf;
        private void PrintBillinPdf()
        {
            ////iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(pageWidth, pageHeight);
            //Document document = new Document();
            ////document.SetMargins(20f, 0f, 50f, 0f);

            ////Document document = new Document(PageSize.SMALL_PAPERBACK,0f,0f,0f,0f);
            //PdfWriter.GetInstance(document, new FileStream(Application.StartupPath + "Bill.pdf", FileMode.Create));
            //document.Open();

            //iTextSharp.text.Font courier = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 20f);
            ////Chunk chunk = new Chunk("Setting the Font", FontFactory.GetFont("dax-black"));
            //Paragraph p = new Paragraph("           Laziz Pizza          ", courier);
            //document.Add(p);
            //p = new Paragraph("Lunsi Kui,Opp.Parsi Hospital.Navsari", courier);
            //document.Add(p);
            //p = new Paragraph("             +91 7600752775", courier);
            //document.Add(p);
            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //p = new Paragraph(" Name:    " + txtName.Text, courier);
            //document.Add(p);
            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //p = new Paragraph(" Date: " + txtBillDate.Text + "           DineIn: " + txtTableNo.Text, courier);
            //document.Add(p);
            //p = new Paragraph(" Cashier: Kiran" + "          Bill No: " + txtBillNo.Text, courier);
            //document.Add(p);
            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //p = new Paragraph(" Item                  Qty        Price", courier);
            //document.Add(p);
            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);

            //if (dgvDetails.Rows.Count > 0)
            //{
            //    for (int row = 0; row < dgvDetails.Rows.Count; row++)
            //    {
            //        string item = dgvDetails.Rows[row].Cells["Item_Name"].Value.ToString().Trim();

            //        string line;
            //        if (item.Length >= 20)
            //        {
            //            item = item.Substring(0, 20);
            //            line = item.PadRight(20) + dgvDetails.Rows[row].Cells["Qty"].Value.ToString().PadLeft(5) + dgvDetails.Rows[row].Cells["Total"].Value.ToString().PadLeft(14);
            //        }
            //        else
            //        {
            //            item = item.PadRight(21 - item.Length, ' ');
            //            line = item.PadRight(20) + dgvDetails.Rows[row].Cells["Qty"].Value.ToString().PadLeft(5) + dgvDetails.Rows[row].Cells["Total"].Value.ToString().PadLeft(14);
            //        }


            //        p = new Paragraph(line, courier);
            //        document.Add(p);
            //    }
            //}

            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //p = new Paragraph("Total:".PadLeft(30) + txttotalPrice.Text.PadLeft(9), courier);
            //document.Add(p);

            //if (txtDiscount.Text != "0.00")
            //{
            //    p = new Paragraph("Discount:".PadLeft(30) + txtDiscount.Text.PadLeft(9), courier);
            //    document.Add(p);
            //}

            //if (txtDeliveryCharge.Text != "0.00")
            //{
            //    p = new Paragraph("Delivery:".PadLeft(30) + txtDeliveryCharge.Text.PadLeft(9), courier);
            //    document.Add(p);
            //}

            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //p = new Paragraph("Grand Total:".PadLeft(30) + txtnetamount.Text.PadLeft(9), courier);
            //document.Add(p);
            ////p = new Paragraph("-------------------------------------------------------------------------");
            //p = new Paragraph("---------------------------------------------------------------------------------------------------------------------");
            //document.Add(p);
            //document.SetMargins(0, 0, 0, 0);
            ////document.LeftMargin = 0.0;
            //document.Close();

            ////Show Report
            //ViewReport obj = new ViewReport(Application.StartupPath + "Bill.pdf");
            //obj.ShowDialog();
            //////////////////////////

            ///////Print Report (pdf Format)
            ///* ProcessStartInfo info = new ProcessStartInfo(Application.StartupPath + "Bill.pdf");
            // info.Verb = "Print";
            // info.CreateNoWindow = true;
            // info.WindowStyle = ProcessWindowStyle.Hidden;
            // Process.Start(info);
            // */
        }
        #endregion

        private void PrintBillUsingPrintDocument()
        {
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();

        }
       
        private void BillInExcel()
        {
            Excel.Application oXL = new Excel.Application();
            Excel.Workbook theWorkbook;
            Excel.Worksheet worksheet;

            if (File.Exists(Application.StartupPath + @"\LazizBill.xls"))
            {
                File.Delete(Application.StartupPath + @"\LazizBill.xls");
            }

            theWorkbook = oXL.Workbooks.Open(Application.StartupPath + @"\LazizBill.xlt", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, Type.Missing, Type.Missing);

            worksheet = (Excel.Worksheet)theWorkbook.Sheets[1];

            Excel.Range RngCName = worksheet.get_Range("CustomerName", "CustomerName");
            Excel.Range RngBillNo = worksheet.get_Range("Bill_No", "Bill_No");
            Excel.Range RngBillDate = worksheet.get_Range("Bill_Date", "Bill_Date"); Excel.Range RngBillTime = worksheet.get_Range("Bill_Time", "Bill_Time");
            Excel.Range RngDineIn = worksheet.get_Range("TableNo", "TableNo");
            Excel.Range RngItemName = worksheet.get_Range("Item_Name", "Item_Name");
            Excel.Range RngQty = worksheet.get_Range("Qty", "Qty");
            Excel.Range RngUnitPrice = worksheet.get_Range("UnitPrice", "UnitPrice");
            Excel.Range RngAmount = worksheet.get_Range("Amount", "Amount");

            RngCName.Value2 = txtName.Text;
            RngBillNo.Value2 = txtBillNo.Text;
            RngDineIn.Value2 = txtTableNo.Text;
            RngBillDate.Value2 = txtBillDate.Text;
            RngBillTime.Value2 = txtBillTime.Text;

            int i = 0;
            int Tqty = 0;
            for ( i = 0; i < dgvDetails.Rows.Count ; i++)
            {
                string item = dgvDetails.Rows[i].Cells["Item_Name"].Value.ToString();
                if (dgvDetails.Rows[i].Cells["Item_Name"].Value.ToString().Length >= 20)
                {
                    item = item.Substring(0, 20);
                }
                
                RngItemName.set_Item(i + 1, 1, item);
                RngQty.set_Item(i + 1, 1, Convert.ToString(dgvDetails.Rows[i].Cells["Qty"].Value));
                RngUnitPrice.set_Item(i + 1, 1, Convert.ToString(dgvDetails.Rows[i].Cells["UnitPrice"].Value));
                RngAmount.set_Item(i + 1, 1, Convert.ToString(dgvDetails.Rows[i].Cells["Total"].Value));

                Tqty += int.Parse(dgvDetails.Rows[i].Cells["Qty"].Value.ToString());
            }

            RngItemName.set_Item(i + 1, 1, "Sub Total");
            RngQty.set_Item(i + 1, 1, Tqty.ToString());
            RngAmount.set_Item(i + 1, 1, txttotalPrice.Text);
            i++;

            int iTotalRows = worksheet.UsedRange.Rows.Count;

            Excel.Range RngColSubTotal;
            RngColSubTotal = worksheet.get_Range("A" + iTotalRows, ReportClassObj.ColumnIndexToColumnLetter(4).ToString() + iTotalRows);
            RngColSubTotal.EntireRow.Font.Bold = true;
            RngColSubTotal.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            //RngColSubTotal.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            if ( txtDiscount.Text != "0.00")
            {
                RngItemName.set_Item(i + 1, 1, "Discount");
                RngAmount.set_Item(i + 1, 1, txtDiscount.Text);
                i++;
            }

            if (txtDeliveryCharge.Text != "0.00")
            {
                RngItemName.set_Item(i + 1, 1, "Delivery");
                RngAmount.set_Item(i + 1, 1, txtDeliveryCharge.Text);
                i++;
            }

            RngItemName.set_Item(i + 1, 1, "Grand Total");
            //RngQty.set_Item(i + 1, 1, Tqty.ToString());
            RngAmount.set_Item(i + 1, 1, txtnetamount.Text);
            i++;

            iTotalRows = worksheet.UsedRange.Rows.Count;

            Excel.Range RngColGrandTotal;
            RngColGrandTotal = worksheet.get_Range("A" + iTotalRows, ReportClassObj.ColumnIndexToColumnLetter(4).ToString() + iTotalRows);
            RngColGrandTotal.EntireRow.Font.Bold = true;
            RngColGrandTotal.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            //RngColGrandTotal.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            RngCName.Rows.AutoFit();
            RngBillNo.Rows.AutoFit();
            RngDineIn.Rows.AutoFit();

            RngBillDate.Style.WrapText = true;
            RngBillDate.Rows.AutoFit();

            RngItemName.Style.WrapText = true;
            RngItemName.Rows.AutoFit();

            //iTotalRows = worksheet.UsedRange.Rows.Count;
            //Excel.Range RngLastLine;
            //RngLastLine = worksheet.get_Range("A" + iTotalRows, ReportClassObj.ColumnIndexToColumnLetter(4).ToString() + iTotalRows);
            ////RngLastLine.Merge();
            //RngLastLine.set_Item(i+1, 1, "Thank you,visit again!");

            oXL.ActiveWorkbook.SaveAs(Application.StartupPath + @"\LazizBill", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(worksheet);
            oXL.Workbooks.Close();
            Marshal.FinalReleaseComObject(theWorkbook);
            oXL.Application.Quit();
            Marshal.FinalReleaseComObject(oXL);
            oXL = null;

            Excel.Application xlApp = new Excel.Application();  // create new Excel application
            Excel.Workbook xlWorkbook;
            Excel.Worksheet xlworksheet;

            xlApp.Visible = true;                               // application becomes visible
            //xlWorkbook = xlApp.Workbooks.Open(Application.StartupPath + @"\LazizBill.xls", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, Type.Missing, Type.Missing);
            xlWorkbook = xlApp.Workbooks.Open(Application.StartupPath + @"\LazizBill.xls",0);
            xlworksheet = (Excel.Worksheet)xlWorkbook.Sheets[1];
            //xlworksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            MessageBox.Show("Press Enter To Continue.......", "", MessageBoxButtons.OK);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(xlworksheet);
            xlApp.Workbooks.Close();
            Marshal.FinalReleaseComObject(xlWorkbook);
            xlApp.Application.Quit();
            Marshal.FinalReleaseComObject(xlApp);
            xlApp = null;

        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
            objBase.RefreshGridBill();
            objBase.lblPageHead.Text = "Generate Bill-" + txtBillNo.Text;
            btnNewBill.Enabled = true;
            btnCancel.Visible = false;
        }

        public static string PrependSpaces(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(str);
            for (int i = 0; i < (20 - str.Length); i++)
            {
                sb.Insert(str.Length, "    ");
            }
            return sb.ToString();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region OldPrinting
          //  e.Graphics.DrawString("                   Laziz Pizza                 ", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 10));
          //  e.Graphics.DrawString("\nLunsi Kui,Opp.Parsi Hospital.Navsari", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular), Brushes.Black, new PointF(10, 20));
          //  e.Graphics.DrawString("\n +91 9824100903,+91 7600752775", new System.Drawing.Font("Times New Roman", 10), Brushes.Black, new PointF(10, 50));
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 70));
          //  e.Graphics.DrawString("\n Name:    " + txtName.Text, new System.Drawing.Font("Times New Roman", 12), Brushes.Black, new PointF(10, 90));
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 110));
          //  e.Graphics.DrawString("\n                                 Time: " + txtBillTime.Text, new System.Drawing.Font("Times New Roman", 12), Brushes.Black, new PointF(10, 130));
          //  e.Graphics.DrawString("\n Date: " + txtBillDate.Text + "     DineIn: " + txtTableNo.Text, new System.Drawing.Font("Times New Roman", 12), Brushes.Black, new PointF(10, 150));
          //  e.Graphics.DrawString("\n Cashier: Kiran" + "        Bill No: " + txtBillNo.Text, new System.Drawing.Font("Times New Roman", 12), Brushes.Black, new PointF(10, 170));
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 190));
          //  e.Graphics.DrawString("\n Item                      Qty      Price", new System.Drawing.Font("Times New Roman", 12), Brushes.Black, new PointF(10, 210));
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 230));

          //  int Ypos = 250;
          //  if (dgvDetails.Rows.Count > 0)
          //  {
          //      for (int row = 0; row < dgvDetails.Rows.Count; row++)
          //      {
          //          string item = dgvDetails.Rows[row].Cells["Item_Name"].Value.ToString().ToLower().TrimStart();
                    
          //          if (dgvDetails.Rows[row].Cells["Item_Name"].Value.ToString().Length >= 20)
          //          {
          //              item = item.Substring(0, 20);
          //          }
          //          else
          //          {
          //              item = item.PadRight(20, ' ');
          //          }

          //          string line = item + "    " + dgvDetails.Rows[row].Cells["Qty"].Value.ToString().PadLeft(10,' ') + "   " + dgvDetails.Rows[row].Cells["Total"].Value.ToString().PadLeft(10, ' ');
          //          e.Graphics.DrawString("\n" + line, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular), Brushes.Black, new PointF(10, Ypos), StringFormat.GenericTypographic);
          //          Ypos += 20;
          //      }
          //  }
            
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 40;
          //  e.Graphics.DrawString("                                 Total:" + txttotalPrice.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;

          //  if (txtDiscount.Text != "0.00")
          //  {
          //      e.Graphics.DrawString("                         Discount:" + txtDiscount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;
          //  }
          //  if (txtDeliveryCharge.Text != "0.00")
          //  {
          //      e.Graphics.DrawString("                         Delivery:" + txtDeliveryCharge.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;
          //  }


          //  //System.Drawing.Font arialBold = new System.Drawing.Font("Times New Roman", 30, FontStyle.Bold);
          //  //String Gtext = "                             Grand Total:" + txtnetamount.Text;
          //  //Size textSize = TextRenderer.MeasureText(Gtext, arialBold);

          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 40;
          //  e.Graphics.DrawString("                             Grand Total:" + txtnetamount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 10;
          ////e.Graphics.DrawString("                             Grand Total:" + txtnetamount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new System.Drawing.Rectangle(new Point(10, Ypos), textSize)); Ypos += 10;
          //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;
            #endregion

            Graphics graphic = e.Graphics;
            Font font = new Font("Times New Roman", 12);
            float fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 0;

            string item1 = "Laziz Pizza";
            item1 = item1.PadLeft(20, ' ');
            graphic.DrawString(item1, new Font("Courier New", 9, FontStyle.Bold), new SolidBrush(Color.Black),startX,startY);

            offset = offset + (int)fontHeight + 3;
            graphic.DrawString("Shop 9,Lunsikui,Opp.Parsi Hospital.Navsari", new Font("Courier New", 7, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 5;
            graphic.DrawString("+91 9824100903,+91 7600752775", new Font("Courier New", 7, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "Name:" + txtName.Text;
            item1 = item1.PadRight(30 - ((item1.Length) >= 30 ? 0 : item1.Length), ' ');
            graphic.DrawString(item1, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "Date: " + txtBillDate.Text;
            string item2 =" DineIn:" + txtTableNo.Text;
            item1 = item1.PadRight(30 - ((item1.Length) >= 30 ? 0 : item1.Length), ' ');
            graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "Cashier: Kiran";
            item2 = "Bill No: " + txtBillNo.Text;
            item1 = item1.PadRight(30 - ((item1.Length) >= 30 ? 0 : item1.Length), ' ');
            graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item2 = " Time " + txtBillTime.Text;
            item1 = item1.PadRight(15 - ((item1.Length) >= 15 ? 0 : item1.Length), ' ');
            graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "Items";
            item2 = "Qty";
            string item3 = "Price";
            item1 = item1.PadRight(25 - ((item1.Length) >= 25 ? 0 : item1.Length), ' ');
            item3 = item3.PadLeft(12 - ((item3.Length) >= 12 ? 0 : item3.Length), ' ');
            graphic.DrawString(item1 + item2 + item3, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            if (dgvDetails.Rows.Count > 0)
            {
                for (int row = 0; row < dgvDetails.Rows.Count; row++)
                {
                    item1 = dgvDetails.Rows[row].Cells["Item_Name"].Value.ToString().ToLower().TrimStart();

                    if (dgvDetails.Rows[row].Cells["Item_Name"].Value.ToString().Length >= 20)
                    {
                        item1 = item1.Substring(0, 20);
                    }
                    else
                    {
                        //item1 = item1.PadRight(40-((item1.Length)>=30?0:item1.Length), '-');
                        item1 = item1.PadRight(20);
                    }

                    //string x = "";
                    offset = offset + (int)fontHeight + 3;
                    //x = item1;
                    //x = x.PadRight(20-(x.Length), ' ');

                    item2 = dgvDetails.Rows[row].Cells["Qty"].Value.ToString();
                    item2 = string.Format("{0:c}", item2);
                    //item2 = item2.PadLeft(10, ' ');

                    //x = x + item2;
                    //x = x.PadRight(30-(x.Length),' ');
                    //item2 = item2.PadLeft(10, ' ');
                    

                    item3 = dgvDetails.Rows[row].Cells["Total"].Value.ToString();
                    item3 = string.Format("{0:c}", item3);
                    //x = x + item3;
                    item3 = item3.PadLeft(10, ' ');

                    string line = item1+ " " +item2+item3;
                    graphic.DrawString(line, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
                    
                }
            }

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item2 = " Total:" + txttotalPrice.Text;
            item1 = item1.PadRight(18 - ((item1.Length)>=18?0:item1.Length), ' ');
            graphic.DrawString(item1 + item2, new Font("Courier New", 8,FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            if (txtDiscount.Text != "0.00")
            {
                offset = offset + (int)fontHeight + 3;
                item1 = "";
                item2 = " Discount:" + txtDiscount.Text;
                item1 = item1.PadRight(15 - ((item1.Length) >= 15 ? 0 : item1.Length), ' ');
                graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            }

            if (txtDeliveryCharge.Text != "0.00")
            {
                offset = offset + (int)fontHeight + 3;
                item1 = "";
                item2 = " Delivery:" + txtDeliveryCharge.Text;
                item1 = item1.PadRight(15 - ((item1.Length) >= 15 ? 0 : item1.Length), ' ');
                graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            }

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item2 = "Grand Total:" + txtnetamount.Text;
            item1 = item1.PadRight(13 - ((item1.Length) >= 13 ? 0 : item1.Length), ' ');
            graphic.DrawString(item1 + item2, new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 3;
            item1 = "";
            item1 = item1.PadLeft(32, '-');
            graphic.DrawString(item1, new Font("Courier New", 8), new SolidBrush(Color.Black), startX, startY + offset);

            //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 40;
            //  e.Graphics.DrawString("                                 Total:" + txttotalPrice.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;

            //  if (txtDiscount.Text != "0.00")
            //  {
            //      e.Graphics.DrawString("                         Discount:" + txtDiscount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;
            //  }
            //  if (txtDeliveryCharge.Text != "0.00")
            //  {
            //      e.Graphics.DrawString("                         Delivery:" + txtDeliveryCharge.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;
            //  }


            //  //System.Drawing.Font arialBold = new System.Drawing.Font("Times New Roman", 30, FontStyle.Bold);
            //  //String Gtext = "                             Grand Total:" + txtnetamount.Text;
            //  //Size textSize = TextRenderer.MeasureText(Gtext, arialBold);

            //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 40;
            //  e.Graphics.DrawString("                             Grand Total:" + txtnetamount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 10;
            ////e.Graphics.DrawString("                             Grand Total:" + txtnetamount.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new System.Drawing.Rectangle(new Point(10, Ypos), textSize)); Ypos += 10;
            //  e.Graphics.DrawString("\n-----------------------------------------", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new PointF(10, Ypos)); Ypos += 20;

        }
        
        int IntRowIndex = 0;
        private void btn_First_Click(object sender, EventArgs e)
        {
            FillBill(0);
            IntRowIndex = 0;
        }
        private void btn_previous_Click(object sender, EventArgs e)
        {
            IntRowIndex--;
            if (IntRowIndex >= 0)
            {
                FillBill(IntRowIndex);
            }
            else
            {
                FillBill(0);
                IntRowIndex = 0;
            }
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            IntRowIndex++;
            if (IntRowIndex <= dsMain.Tables["Bill_Master"].Rows.Count - 1)
            {
                FillBill(IntRowIndex);
            }
            else
            {
                FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
                IntRowIndex = dsMain.Tables["Bill_Master"].Rows.Count - 1;
            }
        }
        private void btn_last_Click(object sender, EventArgs e)
        {
            FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
            IntRowIndex = dsMain.Tables["Bill_Master"].Rows.Count - 1;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string BillNo = txtBillNo.Text;
            if (BillNo != "")
            {
                DBClass.connection.Open();

                _MainDTAdapter.DeleteCommand = new SqlCommand(@"Delete From Bill_Trans where Bill_No= @Bill_No ", DBClass.connection);
                _MainDTAdapter.DeleteCommand.Parameters.AddWithValue("@Bill_No", int.Parse(BillNo));
                _MainDTAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["Bill_Trans"].Clear();
                _MainDTAdapter.Fill(dsMain.Tables["Bill_Trans"]);

                _MainAdapter.DeleteCommand = new SqlCommand(@"Delete From Bill_Master where Bill_No= @Bill_No ", DBClass.connection);
                _MainAdapter.DeleteCommand.Parameters.AddWithValue("@Bill_No", int.Parse(BillNo));
                _MainAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["Bill_Master"].Clear();
                _MainAdapter.Fill(dsMain.Tables["Bill_Master"]);
                

                DBClass.connection.Close();
            }
            btnNewBill.Enabled = true;
            btnCancel.Visible = false;
            FillBill(dsMain.Tables["Bill_Master"].Rows.Count - 1);
        }
    }
}
