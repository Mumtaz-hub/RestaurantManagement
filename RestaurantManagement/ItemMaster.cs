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
    public partial class ItemMaster : Form
    {
        public ItemMaster()
        {
            InitializeComponent();
        }

        DataSet dsMain = new DataSet();
        DataSet ds = new DataSet();
        SqlDataAdapter _MainAdapter;
        SqlDataAdapter adpCateggory;
        
        DataTable dt;
        private void ItemMaster_Load(object sender, EventArgs e)
        {
            displayCategory();
            displayRecord();

        }

        private void displayCategory()
        {
            adpCateggory = DBClass.GetAdaptor("Category_Master");
            dt = new DataTable();

            adpCateggory.Fill(dt);
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Category_Master";

            cmbCategory.DisplayMember = "Category_Name";
            cmbCategory.ValueMember = "Category_Id";
            cmbCategory.DataSource = ds.Tables["Category_Master"];
            

        }

        private void displayRecord()
        {
            _MainAdapter = DBClass.GetAdaptor("Item_Master");
            dsMain = new DataSet();

            _MainAdapter.Fill(dsMain);
            dsMain.Tables[0].TableName = "Item_Master";
            dgvItem.DataSource = null;
            dgvItem.DataSource = dsMain.Tables["Item_Master"];

            
            dgvItem.Columns["Item_Code"].ReadOnly = true;
            dgvItem.Columns["Item_Code"].HeaderText = "No.";
            dgvItem.Columns["Item_Code"].Width = 50;
            dgvItem.Columns["Item_Code"].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;

            dgvItem.Columns["Item_Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvItem.Columns["Item_Name"].HeaderText = "Items Name";

            
            dgvItem.Columns["UnitPrice"].Width = 100;
            dgvItem.Columns["UnitPrice"].HeaderText = "Items Price";

            dgvItem.Columns["Item_Number"].HeaderText = "Menu No.";
            dgvItem.Columns["Item_Number"].Width = 50;

            dgvItem.Columns["Short_Code"].HeaderText = "Short Code";
            dgvItem.Columns["Short_Code"].Width = 50;

            dgvItem.Columns["Category_ID"].HeaderText = "Category_ID";
            dgvItem.Columns["Category_ID"].Width = 50;
            dgvItem.Columns["Category_ID"].Visible = false;

            dsMain.Tables["Item_Master"].DefaultView.RowFilter = "";
            dsMain.Tables["Item_Master"].DefaultView.RowFilter = "Category_Id=" + int.Parse(cmbCategory.SelectedValue.ToString());

            dgvItem.DataSource = dsMain.Tables["Item_Master"].DefaultView;

        }

        private void ItemMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvItem_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            _MainAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            _MainAdapter.Update(dsMain.Tables["Item_Master"]);

            dsMain.Tables["Item_Master"].Clear();
            _MainAdapter = DBClass.GetAdaptor("Item_Master");
            _MainAdapter.Fill(dsMain.Tables["Item_Master"]);
        }

        private void dgvItem_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
            _MainAdapter.Update(dsMain.Tables["Item_Master"]);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue != null && dsMain.Tables.Count>0)
            {
                dsMain.Tables["Item_Master"].DefaultView.RowFilter = "";
                dsMain.Tables["Item_Master"].DefaultView.RowFilter = "Category_Id=" + int.Parse(cmbCategory.SelectedValue.ToString());
                dgvItem.DataSource = dsMain.Tables["Item_Master"].DefaultView;
            }
        }

        private void dgvItem_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Category_Id"].Value = cmbCategory.SelectedValue.ToString();
        }

        

        
    }
}
