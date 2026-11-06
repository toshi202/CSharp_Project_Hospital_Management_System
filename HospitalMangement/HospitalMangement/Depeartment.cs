using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalMangement
{
    public partial class Depeartment : Form
    {
        public Depeartment()
        {
            InitializeComponent();
        }

        private void Depeartment_Load(object sender, EventArgs e)
        {
            this.LoadData();
            if (this.Owner != null)
            {
                this.Owner.Hide();
            }
        }

        private void LoadData()
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Department";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                con.Close();

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = dt;
                dgvData.Refresh();
                dgvData.ClearSelection();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Depeartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            txtId.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDeptName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void NewData()
        {
            txtId.Text = "Auto Generated";
            txtDeptName.Text = "";
            dgvData.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string ID = txtId.Text;
            if (ID == "Auto Generated")
            {
                MessageBox.Show("Please Select a row first!");
                return;
            }

            var result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"delete from Department where DeptID={ID}";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Operation Complete");
                this.LoadData();
                this.NewData();

                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ID = txtId.Text;
            string DeptName = txtDeptName.Text;
            
            string query = "";
            if (ID == "Auto Generated")
            {
                query = $"INSERT INTO Department values ('{DeptName}')";
            }

            else
            {
                query = $"update Department set DeptName = '{DeptName} 'where DeptID ={ID} ";
            }

            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Operation Complete");
                this.LoadData();
                this.NewData();

                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
