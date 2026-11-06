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

        
        
        }
    }

