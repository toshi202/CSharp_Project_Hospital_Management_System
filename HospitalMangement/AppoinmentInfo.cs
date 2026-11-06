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
    public partial class AppoinmentInfo : Form
    {
        public AppoinmentInfo()
        {
            InitializeComponent();
        }

        private void AppoinmentInfo_Load(object sender, EventArgs e)
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
                cmd.CommandText = "select AppointmentInfo.APID, Patient.FullName'PatientName', Doctor.FullName'DoctorName', AppointmentInfo.AppointmentDate , Patient.PID, Doctor.DocID from AppointmentInfo inner join Patient on Patient.PID = AppointmentInfo.PID inner join Doctor on Doctor.DocID = AppointmentInfo.DocID; select * from Patient; select * from Doctor;";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                con.Close();

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = ds.Tables[0];
                dgvData.Refresh();
                dgvData.ClearSelection();

                cmbpn.DataSource = ds.Tables[1];
                cmbpn.DisplayMember = "FullName";
                cmbpn.ValueMember = "PID";

                cmbdn.DataSource = ds.Tables[2];
                cmbdn.DisplayMember = "FullName";
                cmbdn.ValueMember = "DocID";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void AppoinmentInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
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
            txtad.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbpn.SelectedValue = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbdn.SelectedValue = dgvData.Rows[e.RowIndex].Cells[5].Value.ToString();
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void NewData()
        {
            txtId.Text = "Auto Generated";
            cmbpn.SelectedValue = -1;
            cmbdn.SelectedValue = -1;
            txtad.Text = "";


            dgvData.ClearSelection();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                cmd.CommandText = $"delete from AppointmentInfo where APID={ID}";
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
            
            string PID = cmbpn.SelectedValue.ToString();
            string DocID = cmbdn.SelectedValue.ToString();

            string AppointmentDate = DateTime.Now.ToString("yyyy-MM-dd");
            
            string query = "";
            if (ID == "Auto Generated")
            {
                query = $"INSERT INTO AppointmentInfo  VALUES  ({PID}, {DocID},{AppointmentDate})";
            }

            else
            {
                query = $"update AppointmentInfo set PID='{PID}', DocID='{DocID}', AppointmentDate='{AppointmentDate}' where ApID ={ID} ";
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
