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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            Record rf = new Record();
            rf.Show(this);
            this.Hide();
        }

        private void btnDonor_Click(object sender, EventArgs e)
        {
            UserInfo df = new UserInfo();
            df.Show(this);
            this.Hide();
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            AppoinmentInfo ai = new AppoinmentInfo();
            ai.Show(this);
            this.Hide();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            BillInfo bill = new BillInfo(); 
            bill.Show(this);
            this.Hide();
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            Depeartment de = new Depeartment();
            de.Show(this);
            this.Hide();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Hide();
            }
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }
    }
}
