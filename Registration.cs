using OTPVerification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portal
{
    public partial class Registration : Form
    {
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate that both password fields match
            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            // Connection string to connect to the local database (SQL Server LocalDB)
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";

            SqlConnection con = new SqlConnection(ConnectionString);

            try
            {
                con.Open();

                // Generate a random 6-digit OTP
                Random rnd = new Random();
                string otp = rnd.Next(100000, 999999).ToString();

                // SQL command to insert data into the T1 table, including the generated OTP
                SqlCommand sq2 = new SqlCommand("INSERT INTO T1(name,id,email,password,otp) VALUES(@name, @id, @email, @password, @otp)", con);

                // Adding parameters from textboxes and the OTP
                sq2.Parameters.AddWithValue("@name", textBox1.Text);
                sq2.Parameters.AddWithValue("@id", textBox2.Text);
                sq2.Parameters.AddWithValue("@email", textBox3.Text);
                sq2.Parameters.AddWithValue("@password", textBox4.Text); // Password is now confirmed
                sq2.Parameters.AddWithValue("@otp", otp);

                // Execute the SQL command to insert data
                sq2.ExecuteNonQuery();

                // Send the OTP via email
                SendOtpToEmail(textBox3.Text, otp);

                // Display a success message
                MessageBox.Show("Registration successful! OTP sent to your email.");

                // Clear textboxes after successful registration
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear(); // Clear the confirm password box too
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            Otp otp1 = new Otp();
            this.Show();

        }

        // Function to send the OTP to the user's email
        private void SendOtpToEmail(string email, string otp)
        {
            try
            {
                // Set up the SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("pretom120@gmail.com", "kzoxwazhvhlnkxwd");

                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("pretom120@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "Your OTP Code";
                mailMessage.Body = "Your OTP code is: " + otp;

                // Send the email
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email. Error: " + ex.Message);
            }
        }

        // Event handler for the Show Password checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Show passwords
                textBox4.PasswordChar = '\0';
                textBox5.PasswordChar = '\0'; // Confirm password box
            }
            else
            {
                // Hide passwords
                textBox4.PasswordChar = '*';
                textBox5.PasswordChar = '*'; // Confirm password box
            }
        }
    }
}