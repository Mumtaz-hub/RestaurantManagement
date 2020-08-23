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
    public partial class ItemCategory : Form
    {
        public ItemCategory()
        {
            InitializeComponent();
        }
        DataSet dsMain = new DataSet();
        SqlDataAdapter _MainAdapter;
        private void ItemCategory_Load(object sender, EventArgs e)
        {
            displayRecord();
        }

        private void displayRecord()
        {
            _MainAdapter = DBClass.GetAdaptor("Category_Master");
            dsMain = new DataSet();

            _MainAdapter.Fill(dsMain);
            dsMain.Tables[0].TableName = "Category_Master";
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = dsMain.Tables["Category_Master"];

            dgvCategory.Columns["Category_Id"].ReadOnly = true;
            dgvCategory.Columns["Category_Id"].HeaderText = "No.";
            dgvCategory.Columns["Category_Id"].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvCategory.Columns["Category_Id"].Width = 50;

            dgvCategory.Columns["Category_Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategory.Columns["Category_Name"].HeaderText = "Items Name";

        }
        private void ItemCategory_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvCategory_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            _MainAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            _MainAdapter.Update(dsMain.Tables["Category_Master"]);

            dsMain.Tables["Category_Master"].Clear();
            _MainAdapter = DBClass.GetAdaptor("Category_Master");
            _MainAdapter.Fill(dsMain.Tables["Category_Master"]);
        }

        private void dgvCategory_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
            _MainAdapter.Update(dsMain.Tables["Category_Master"]);
        }
    }
}
