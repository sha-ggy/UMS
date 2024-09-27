using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text;


namespace Portal
{
    public partial class Registration : Form
    {



        string studentDbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 x = new Form1();
            x.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            // Check if StudentId and Password fields are filled
            if (string.IsNullOrWhiteSpace(studentIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int studentId = int.Parse(studentIdTextBox.Text);
            string password = passwordTextBox.Text;

            // Check if the student exists in the Student table
            using (SqlConnection con = new SqlConnection(studentDbConnectionString))
            {
                con.Open();

                // Check if the student ID exists in the Student table
                SqlCommand checkStudentCmd = new SqlCommand("SELECT COUNT(*) FROM Student WHERE StudentId = @StudentId", con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", studentId);

                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0) // If student does not exist
                {
                    MessageBox.Show("Student ID not found. Only enrolled students can set a password.");
                    return;
                }

                // Check if a password already exists for this StudentId
                SqlCommand checkLoginCmd = new SqlCommand("SELECT COUNT(*) FROM LoginCredentials WHERE StudentId = @StudentId", con);
                checkLoginCmd.Parameters.AddWithValue("@StudentId", studentId);
                int loginCount = (int)checkLoginCmd.ExecuteScalar();

                if (loginCount > 0) // If a password already exists, prevent setting it again
                {
                    MessageBox.Show("A password has already been set for this Student ID. Password cannot be changed.");
                    
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();

                }

                // If no login credentials exist for this StudentId, insert a new one
                SqlCommand cmd = new SqlCommand("INSERT INTO LoginCredentials (StudentId, Password) VALUES (@StudentId, @Password)", con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password has been set successfully!");

                    // Optionally, clear the input fields
                    studentIdTextBox.Clear();
                    passwordTextBox.Clear();

                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }



        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}

