using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTPVerification
{
    public partial class Otp : Form
    {
        public Otp()
        {
            InitializeComponent();
        }

        private void OTP_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization logic can go here
        }

        // Verify OTP button click event
        private void button1_Click(object sender, EventArgs e)
        {
            string enteredOtp = textBox1.Text; // Get OTP entered by the user

            // Connection string to connect to the database
            //  string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True";

            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\csharp\db\Login and Registration.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection con = new SqlConnection(ConnectionString);

            try
            {
                // Open the database connection
                con.Open();

                // SQL query to check if the entered OTP matches any in the database
                string query = "SELECT COUNT(*) FROM T1 WHERE otp = @OTP";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OTP", enteredOtp);

                // Execute the query and check if any record is returned
                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    MessageBox.Show("OTP verified successfully!");
                }
                else
                {
                    MessageBox.Show("Invalid OTP. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                con.Close();
            }
        }
    }
}
