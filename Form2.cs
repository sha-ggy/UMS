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

namespace Portal
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            customDesign();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
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




        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 obj = new Form2();
            obj.Show();
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           // hideSubmenu();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           // hideSubmenu();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
           // showSubmenu(panel1);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           // hideSubmenu();
        }

        private void Button_submit_Click(object sender, EventArgs e)
        {
          //  hideSubmenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // hideSubmenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //showSubmenu(panel2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // hideSubmenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                this.Hide();
                Form2 obj = new Form2();
                obj.Show();
            }
            hideSubmenu();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 lForm = new Form1();
                lForm.Show();
                this.Hide();
            }
            hideSubmenu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            AddTeachersForm lForm = new AddTeachersForm();
            lForm.Show();
            hideSubmenu();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStudentsForm lForm = new AddStudentsForm();
            lForm.Show();
            hideSubmenu();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            showSubmenu(panel6);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
