using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalMangement
{
    public partial class UserInfo : Form
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        private void Donor_Load(object sender, EventArgs e)
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
                cmd.CommandText = "Select * from UserInfo";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                con.Close();

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = ds.Tables[0];
                dgvData.Refresh();
                dgvData.ClearSelection();




            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Donor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
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
            txtut.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFullName.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPass.Text = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }
        private void NewData()
        {
            txtId.Text = "Auto Generated";
            txtut.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
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
                cmd.CommandText = $"delete from UserInfo where UserID={ID}";
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
            string UserType = txtut.Text;
            string Email = txtEmail.Text;
            string FullName = txtFullName.Text;
            string Password = txtPass.Text;
            
            //int PID = 1;
            //int DocID = 1;
            string query = "";
            if (ID == "Auto Generated")
            {
                query = $"INSERT INTO UserInfo (UserType, FullName, Email, Password)  VALUES ('{UserType}', '{FullName}', '{Email}', '{Password}');";
            }

            else
            {
                query = $"update UserInfo set UserType='{UserType}', FullName='{FullName}', Email='{Email}' , Password='{Password}' where UserID ={ID} ";
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

