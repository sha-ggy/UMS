// File: VerifyOtp.cs
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Portal
{
    public partial class VerifyOtp : Form
    {
        private string userEmail; // Store email when this form is initialized

        // Constructor to initialize the form and accept email
        public VerifyOtp(string email)
        {
            InitializeComponent();
            userEmail = email; // Store the email passed to this form
        }

        // Button click event handler for verifying the OTP
        private void btnVerify_Click(object sender, EventArgs e)
        {
            string enteredOtp = txtOtp.Text.Trim(); // Get OTP entered in the TextBox

            if (string.IsNullOrEmpty(enteredOtp))
            {
                MessageBox.Show("Please enter the OTP.");
                return;
            }

            if (VerifyOtpInDatabase(userEmail, enteredOtp))
            {
                MessageBox.Show("OTP verified successfully! You can now reset your password.");
                // Proceed to the password reset form or functionality
                this.Close();
                // After OTP is verified successfully
                ResetPassword resetPasswordForm = new ResetPassword(userEmail);
                resetPasswordForm.Show();

            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        // Function to verify the OTP from the database
        private bool VerifyOtpInDatabase(string email, string otp)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT otp2 FROM T1 WHERE email = @Email"; // Get the stored OTP
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Get the OTP stored in the database

                    if (result != null && result.ToString() == otp)
                    {
                        return true; // OTP matches
                    }
                    return false; // OTP does not match
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
