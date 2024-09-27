using System;
using System.Windows.Forms;

namespace Portal
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
            customDesign();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubmenu(panel5);

        }

       

        private void home_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void customDesign()
        {
            panel5.Visible = false;
            panel6.Visible = false;

        }
        private void hideSubmenu()
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;

            }
            else
                submenu.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            hideSubmenu();
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            hideSubmenu();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panel6);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 lForm = new Form1();
                lForm.Show();
                this.Hide();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }
    }
}
