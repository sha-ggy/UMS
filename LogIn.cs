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

namespace Portal
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            // Connection string to connect to the local database (SQL Server LocalDB)
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ashfaq\OneDrive - American International University-Bangladesh\Documents\RegData.mdf"";Integrated Security=True;Connect Timeout=30";

            // Creating the SQL connection object with the specified connection string
            SqlConnection con = new SqlConnection(ConnectionString);

            // Opening the connection to the database
            con.Open();

            // SQL command to insert data into the UserTable
            SqlCommand sq2 = new SqlCommand("INSERT INTO RegTable(Id, Name, Age, Password) VALUES(@Id, @Name, @Age, @Password)", con);

            sq2.Parameters.AddWithValue("@Id", textBox1.Text);
            sq2.Parameters.AddWithValue("@Name", textBox3.Text);
            sq2.Parameters.AddWithValue("@Age", int.Parse(textBox2.Text)); // Because age is integer 
            sq2.Parameters.AddWithValue("@Password", textBox4.Text);

            // Executing the SQL command to insert the data into the database
            sq2.ExecuteNonQuery();

            con.Close();

            // Displaying a message box to confirm that the user was added successfully
            MessageBox.Show("User Added");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();


        }

        

    }
}
