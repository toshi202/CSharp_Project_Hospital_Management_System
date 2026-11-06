using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string Email = txtEmail.Text;
            String Password = txtPass.Text;
            if (Email == "toshi@gmail.com" && Password == "Toshi123")
            {
                Admin ad = new Admin();
                ad.Show(this);
                this.Hide();

            }
            else
            {
                MessageBox.Show("Invalid USerName or Password");
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
