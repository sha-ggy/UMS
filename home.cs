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
            showSubmenu(flowLayoutPanel1);

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
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel3.Visible = false;

        }
        private void hideSubmenu()
        {
            if (flowLayoutPanel1.Visible == true)
            {
                flowLayoutPanel1.Visible = false;
            }
            if (flowLayoutPanel3.Visible == true)
            {
                flowLayoutPanel3.Visible = false;
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
            showSubmenu(flowLayoutPanel3);
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
            showSubmenu(flowLayoutPanel3);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();  
            form1.Show();
        }
    }
}
