// File: ForgetPass.cs
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Portal
{
    public partial class ForgetPass : Form
    {
        public ForgetPass()
        {
            InitializeComponent();
        }

        // Function to generate a 6-digit OTP
        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void ForgetPass_Load(object sender, EventArgs e)
        {
            // Any form load logic can go here
        }

        // Button click event handler for submitting the email
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim(); // Gets the email entered in the TextBox

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (CheckEmailExists(email))
            {
                string otp = GenerateOTP();
                if (SaveOtpToDatabase(email, otp))
                {
                    MessageBox.Show("OTP generated and saved. Check your email.");
                    SendOtpEmail(email, otp); // Optionally send OTP to user's email
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
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Function to check if the email exists in the database
        private bool CheckEmailExists(string email)
        {
            // string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM T1 WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    LogError("Database error when checking email: " + ex.Message);
                    MessageBox.Show("There was an error accessing the database. Please try again later.");
                    return false;
                }
            }
        }

        // Function to save OTP to the database
        private bool SaveOtpToDatabase(string email, string otp)
        {
            //  string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE T1 SET otp2 = @OTP, OTPExpiry = @Expiry WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OTP", otp);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Expiry", DateTime.Now.AddMinutes(10)); // OTP valid for 10 minutes

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    LogError("Database error when saving OTP: " + ex.Message);
                    MessageBox.Show("There was an error saving the OTP. Please try again.");
                    return false;
                }
            }
        }

        // Function to send OTP via email
        private void SendOtpEmail(string email, string otp)
        {
            string fromEmail = "pretom120@gmail.com";
            string password = "kzoxwazhvhlnkxwd";
            string subject = "Your OTP Code";
            string body = "Your OTP code is: " + otp;

            MailMessage message = new MailMessage(fromEmail, email, subject, body);
            SmtpClient smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                LogError("Email sending error: " + ex.Message);
                MessageBox.Show("Failed to send OTP email. Please try again.");
            }
        }

        // Button click event for verifying the OTP
        private void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string otp = txtOtp.Text.Trim(); // OTP entered by the user

            if (ValidateOtp(email, otp))
            {
                MessageBox.Show("OTP verified successfully.");
                ResetPasswordForm resetPasswordForm = new ResetPasswordForm(email);
                resetPasswordForm.Show();

            }
            else
            {
                MessageBox.Show("Invalid or expired OTP.");
            }
        }

        // Function to validate the OTP from the database
        private bool ValidateOtp(string email, string otp)
        {
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT otp2, OTPExpiry FROM T1 WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int savedOtp = reader.GetInt32(0);
                            DateTime expiryTime = reader.GetDateTime(1);

                            // Check if OTP matches and is not expired
                            if (savedOtp.ToString() == otp && DateTime.Now <= expiryTime)
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError("Database error when validating OTP: " + ex.Message);
                    MessageBox.Show("There was an error validating the OTP. Please try again.");
                }
                return false;
            }
        }

        // Function to log errors to a file
        private void LogError(string errorMessage)
        {
            string logFilePath = "error_log.txt";
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + errorMessage);
            }
        }
    }
}
