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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HospitalMangement
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            hp.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String Email = txtEmail.Text;
            String Password = txtPass.Text;

            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select * from UserInfo where Email= '{Email}' and Password = '{Password}'";
                
                DataTable dt = new DataTable();
                SqlDataAdapter adp= new SqlDataAdapter(cmd);
                adp.Fill(dt);

                if (dt.Rows.Count!=1)
                {
                    MessageBox.Show("Invalid UserName or Password");
                    return; 
                }

                String Fullname = dt.Rows[0]["FullName"].ToString();
                String UserType = dt.Rows[0]["UserType"].ToString();

                ApplicationHelper.FullName = Fullname;
                ApplicationHelper.UserType= UserType;


                if(UserType=="Admin" || UserType=="Doctor")
                {
                    Admin af = new Admin();
                    af.Show(this);
                    this.Hide();
                }
                else
                {
                    Patient pf = new Patient();
                    pf.Show(this);
                    this.Hide();
                }

                    con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SignIn si = new SignIn();
            si.Show();
            this.Hide();
        }

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
        }

        private void btnEye_MouseHover(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
        }

        private void btnEye_MouseLeave(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Hide();
            }
        }
    }
}
