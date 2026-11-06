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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {ApplicationHelper.FullName} ,[{ApplicationHelper.UserType}]";
            if (ApplicationHelper.UserType == "Doctor")
            {
                btnAdmin.Visible = false;
                //btnPatient.Visible = false;
            }

            if (this.Owner != null)
            {
                this.Owner.Hide();
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show(this);
            this.Hide();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }

        }
    }
}
