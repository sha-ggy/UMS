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
    public partial class AddCoursesForm : Form
    {

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public AddCoursesForm()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;


            this.Load += new EventHandler(AddCoursesForm_Load);
        }

        private void AddCoursesForm_Load(object sender, EventArgs e)
        {
            // Set default row height
            dataGridView1.RowTemplate.Height = 100;

            LoadDataIntoGridView();  // Load data into the DataGridView
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void idDelTF_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 lForm = new Form1();
                lForm.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(courseID.Text) || string.IsNullOrWhiteSpace(courseTitle.Text))
            {
                MessageBox.Show("Course ID and Title are required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Courses (CourseID, CourseTitle, FacultyID, RoomNo, Status) VALUES (@CourseID, @CourseTitle, @FacultyID, @RoomNo, @Status)", con);

                    cmd.Parameters.AddWithValue("@CourseID", int.Parse(courseID.Text));
                    cmd.Parameters.AddWithValue("@CourseTitle", courseTitle.Text);
                    cmd.Parameters.AddWithValue("@FacultyID", int.Parse(facultyID.Text));
                    cmd.Parameters.AddWithValue("@RoomNo", roomNo.Text);
                    cmd.Parameters.AddWithValue("@Status", statusComboBox.SelectedItem?.ToString() ?? "");

                    cmd.ExecuteNonQuery();                    
                    MessageBox.Show("Course added successfully.");
                    LoadDataIntoGridView();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadDataIntoGridView()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Courses", con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt; // Bind data to DataGridView

                
            }
        }
        private void dropBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(courseID.Text))
            {
                MessageBox.Show("Please enter a Course ID to delete.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID", con);
                    checkCmd.Parameters.AddWithValue("@CourseID", int.Parse(courseID.Text));

                    int recordCount = (int)checkCmd.ExecuteScalar();
                    if (recordCount == 0)
                    {
                        MessageBox.Show("Course ID does not exist.");
                        return;
                    }

                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Courses WHERE CourseID = @CourseID", con);
                    deleteCmd.Parameters.AddWithValue("@CourseID", int.Parse(courseID.Text));
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Course deleted successfully.");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            courseID.Clear();
            courseTitle.Clear();
            facultyID.Clear();
            roomNo.Clear();
            statusComboBox.SelectedIndex = -1;
            
            
        }

       
        
    }
}
