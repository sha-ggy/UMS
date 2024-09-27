using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Portal
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgetPass forgetPass = new ForgetPass();
            forgetPass.Show();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if StudentId and Password fields are empty
            if (string.IsNullOrWhiteSpace(studentIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();

                    // SQL query to select Student based on ID and Password
                    String selectData = "SELECT * FROM LoginCredentials WHERE StudentId = @StudentId";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        // Add parameters to avoid SQL Injection
                        cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = int.Parse(studentIdTextBox.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Check if any rows are returned (valid StudentId)
                        if (table.Rows.Count == 1)
                        {
                            // Get the stored password from the database
                            string storedPassword = table.Rows[0]["Password"].ToString();

                            // Verify if the entered password matches the stored password (case-sensitive)
                            if (string.Equals(passwordTextBox.Text.Trim(), storedPassword, StringComparison.Ordinal))
                            {
                                MessageBox.Show("Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                home mainForm = new home();
                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Student ID/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Handle case where StudentId does not exist in the database
                            MessageBox.Show("Incorrect Student ID/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any exception and display the error message
                Console.WriteLine("Stack Trace: " + ex.StackTrace);  // Log the stack trace for debugging
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.Hide();
            AdminPanel ff = new AdminPanel();
            ff.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = checkBox1.Checked ? '\0' : '*';
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddTeachersForm ad = new AddTeachersForm();   
            ad.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AddStudentsForm fff = new AddStudentsForm();
            fff.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration fff = new Registration();
            fff.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentEnrollment fff = new StudentEnrollment();
            fff.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCoursesForm fff = new AddCoursesForm();
            fff.Show();
        }
    }
}
