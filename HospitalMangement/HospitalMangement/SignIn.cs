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
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;

namespace HospitalMangement
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            LogIn lg = new LogIn();
            lg.Show();
            this.Hide();

        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Hide();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            String FullName = txtFullName.Text;
            DateTime RegistrationDate = dtprd.Value;
            String Email = txtEmail.Text;
            String Password = txtPass.Text;
            String Gender = cmbGender.SelectedItem == null ? "" : cmbGender.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("Error: Gender is Required!");
                return;
            }
            

            String BloodType = cmbBloodType.SelectedItem == null ? "" : cmbBloodType.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(BloodType))
            {
                MessageBox.Show("Error: BloodType is Required!");
                return;
            }
            DateTime DateOfBirth = dtpDob.Value;
            
            String age = txtAge.Text;
            if (String.IsNullOrWhiteSpace(age))
            {
                MessageBox.Show("Error: Age field cannot be empty.");
                return;
            }
            int Age;
            try
            {
                Age = Int32.Parse(age);
            }
            catch (FormatException)
            {
                MessageBox.Show("Error: Age must be a number.");
                return;
            }

            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                
                cmd.CommandText = $"INSERT INTO Patient VALUES ('{FullName}', {Age}, '{Gender}', '{DateOfBirth}', '{BloodType}', '{Email}', '{Password}', '{RegistrationDate}')";
                cmd.ExecuteNonQuery(); 
                con.Close();

                MessageBox.Show("Sign up successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
