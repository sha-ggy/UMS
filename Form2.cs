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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void otp_Click(object sender, EventArgs e)
        {
            // Connection string to connect to the local database (SQL Server LocalDB)
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ashfaq\OneDrive - American International University-Bangladesh\Documents\Emailaddress.mdf"";Integrated Security=True;Connect Timeout=30";

            // Creating the SQL connection object with the specified connection string
            SqlConnection con = new SqlConnection(ConnectionString);

            // Opening the connection to the database
            con.Open();

            // SQL command to insert data into the UserTable
            SqlCommand sq2 = new SqlCommand("INSERT INTO Emailaddress(Emailaddress) VALUES(@Emailaddress)", con);

            sq2.Parameters.AddWithValue("@Emailaddress", textBox1.Text);
            

            // Executing the SQL command to insert the data into the database
            sq2.ExecuteNonQuery();

            con.Close();

            // Displaying a message box to confirm that the user was added successfully
            MessageBox.Show("DONE");

            textBox1.Clear();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 obj = new Form2();
            obj.Show();
        }

    }
}
