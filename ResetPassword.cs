// File: ResetPassword.cs
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Portal
{
    public partial class ResetPassword : Form
    {
        private string userEmail; // Store the user's email

        // Constructor to initialize the form with the user's email
        public ResetPassword(string email)
        {
            InitializeComponent();
            userEmail = email; // Store the email for later use
        }

        // Button click event handler for updating the password
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter both password fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            if (UpdatePasswordInDatabase(userEmail, newPassword))
            {
                MessageBox.Show("Password updated successfully!");
                this.Close(); // Close the form after successful update
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                MessageBox.Show("Failed to update password. Please try again.");
            }
        }

        // Function to update the password in the database
        private bool UpdatePasswordInDatabase(string email, string newPassword)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE T1 SET password = @NewPassword WHERE email = @Email"; // Assuming password column exists
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewPassword", newPassword); // Note: Consider hashing the password before storing it
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if a row was updated
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