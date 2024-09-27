// File: ResetPasswordForm.cs
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Portal
{
    public partial class ResetPasswordForm : Form
    {
        private string _email;  // Store the email of the user

        public ResetPasswordForm(string email)
        {
            _email = email;  // Save the email from the OTP verification step
            InitializeComponent();
        }

        // Button click event to reset the password
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Check if passwords match
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Password fields cannot be empty.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Check password strength
            string passwordStrengthMessage = ValidatePasswordStrength(newPassword);
            if (!string.IsNullOrEmpty(passwordStrengthMessage))
            {
                MessageBox.Show(passwordStrengthMessage);
                return;
            }

            // Update the password in the database
            if (UpdatePasswordInDatabase(_email, newPassword))
            {
                MessageBox.Show("Password updated successfully.");

                // Open the login form (Form1) after successful password reset
                Form1 loginForm = new Form1();
                loginForm.Show();  // Display the login form

                this.Close();  // Close the ResetPasswordForm
            }
            else
            {
                MessageBox.Show("Failed to update password. Please try again.");
            }
        }

        // Function to update the password in the database
        private bool UpdatePasswordInDatabase(string email, string newPassword)
        {
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE T1 SET password = @Password WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    LogError("Database error when updating password: " + ex.Message);
                    MessageBox.Show("There was an error updating the password. Please try again.");
                    return false;
                }
            }
        }

        // Function to validate the password strength
        private string ValidatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                return "Password must be at least 8 characters long.";
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))  // At least one uppercase letter
            {
                return "Password must contain at least one uppercase letter.";
            }

            if (!Regex.IsMatch(password, @"[a-z]"))  // At least one lowercase letter
            {
                return "Password must contain at least one lowercase letter.";
            }

            if (!Regex.IsMatch(password, @"[0-9]"))  // At least one digit
            {
                return "Password must contain at least one digit.";
            }

            if (!Regex.IsMatch(password, @"[\W_]"))  // At least one special character
            {
                return "Password must contain at least one special character (e.g., @, #, $, etc.).";
            }

            return string.Empty;  // Return empty if the password is strong
        }

        // Function to log errors to a file
        private void LogError(string errorMessage)
        {
            string logFilePath = "error_log.txt";  // Path to save the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + errorMessage);
            }
        }
    }
}
