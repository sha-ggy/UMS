// File: ForgetPassword.cs
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Portal
{
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        // Function to generate a 6-digit OTP
        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Button click event handler for submitting the email
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim(); // Get email entered in the TextBox

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return;
            }

            string name;
            if (CheckEmailExists(email, out name)) // Fetch the user's name along with email existence check
            {
                string otp = GenerateOTP();
                if (SaveOtpToDatabase(email, otp))
                {
                    // If OTP saved successfully, send it via email
                    if (SendOtpEmail(email, otp, name))
                    {
                        LogMessage("OTP sent successfully to " + email);
                        MessageBox.Show("OTP generated, saved to database, and sent to your email.");
                        VerifyOtp verifyOtpForm = new VerifyOtp(email);
                        verifyOtpForm.Show();
                    }
                    else
                    {
                        LogMessage("Failed to send OTP to " + email);
                        MessageBox.Show("OTP saved, but failed to send the email. Please check your internet connection.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save OTP. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Email not found.");
            }
        }

        // Function to validate email format
        private bool IsValidEmail(string email)
        {
            // Basic email format validation using Regex
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        // Function to check if the email exists in the database and fetch the user's name
        private bool CheckEmailExists(string email, out string name)
        {
            name = string.Empty;
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT name FROM T1 WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        name = reader["name"].ToString();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                    return false;
                }
            }
        }

        // Function to save OTP to the database
        private bool SaveOtpToDatabase(string email, string otp)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE T1 SET otp2 = @OTP WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OTP", otp);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving OTP: " + ex.Message);
                    return false;
                }
            }
        }

        // Function to send the OTP via email with customized message
        private bool SendOtpEmail(string email, string otp, string name)
        {
            string fromEmail = "pretom120@gmail.com";   
            string password = "kzoxwazhvhlnkxwd";    
            string subject = "Your OTP Code";

            // Customizing the email body to include the user's name
            string body = $"Dear {name},\n\nYour OTP code is: {otp}\n\nPlease use this code to reset your password.\n\nBest regards,\nYour Company";

            MailMessage message = new MailMessage(fromEmail, email, subject, body);

            // Configure the SMTP client for Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message);
                return false;
            }
        }

        // Function to log messages to a text file
        private void LogMessage(string message)
        {
            string logFilePath = @"F:\csharp\logs\ForgetPasswordLogs.txt"; // Specify the log file path
            string logEntry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message + Environment.NewLine;

            try
            {
                File.AppendAllText(logFilePath, logEntry); // Append log to file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error logging message: " + ex.Message);
            }
        }

        private void ForgetPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
