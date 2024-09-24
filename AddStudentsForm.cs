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
using System.IO;

namespace Portal
{
    public partial class AddStudentsForm : Form
    {
        private byte[] studentPhoto = null; // To store the picture data
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public AddStudentsForm()
        {
            InitializeComponent();
            customDesign();
        }

        // Method to insert data into the Student table
        private void insertBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(studentId.Text) || string.IsNullOrWhiteSpace(studentName.Text))
            {
                MessageBox.Show("Student ID and Name are required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (StudentId, StudentName, Address, Status, DateOfBirth, Gender, Photo) VALUES (@StudentId, @StudentName, @Address, @Status, @DateOfBirth, @Gender, @Photo)", con);

                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(studentId.Text));
                    cmd.Parameters.AddWithValue("@StudentName", studentName.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);
                    cmd.Parameters.AddWithValue("@Status", status.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.Value);
                    cmd.Parameters.AddWithValue("@Gender", gender.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Photo", studentPhoto ?? new byte[0]);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student inserted successfully.");
                    LoadDataIntoGridView(); // Refresh the DataGridView
                    ClearFields(); // Clear fields after insert
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to load data into the DataGridView
        private void LoadDataIntoGridView()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Student", con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt; // Bind data to DataGridView
            }
        }

        private void customDesign()
        {
            panel5.Visible = false;
            panel6.Visible = false;

        }

        private void hideSubmenu()
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;

            }
            else
                submenu.Visible = false;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ClearFields()
        {
            studentId.Clear();
            studentName.Clear();
            address.Clear();
            status.SelectedIndex = -1;
            gender.SelectedIndex = -1;
            dateOfBirth.Value = DateTime.Now;
            pictureBox1.Image = null;
            studentPhoto = null;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void deleteBtn_Click(object sender, EventArgs e)
        
        {
            if (string.IsNullOrWhiteSpace(idDelTF.Text))
            {
                MessageBox.Show("Please enter a valid Student ID to delete.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Student WHERE StudentId = @StudentId", con);
                    checkCmd.Parameters.AddWithValue("@StudentId", int.Parse(idDelTF.Text));

                    int recordCount = (int)checkCmd.ExecuteScalar();
                    if (recordCount == 0)
                    {
                        MessageBox.Show("Student ID does not exist.");
                        return;
                    }

                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Student WHERE StudentId = @StudentId", con);
                    deleteCmd.Parameters.AddWithValue("@StudentId", int.Parse(idDelTF.Text));
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Student deleted successfully.");
                    LoadDataIntoGridView(); // Refresh 
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                studentPhoto = File.ReadAllBytes(openFileDialog.FileName); // Store the image as byte array
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Set the data source for DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(studentId.Text) || string.IsNullOrWhiteSpace(studentName.Text))
            {
                MessageBox.Show("Student ID and Name are required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (StudentId, StudentName, Address, Status, DateOfBirth, Gender, Photo) VALUES (@StudentId, @StudentName, @Address, @Status, @DateOfBirth, @Gender, @Photo)", con);

                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(studentId.Text));
                    cmd.Parameters.AddWithValue("@StudentName", studentName.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);
                    cmd.Parameters.AddWithValue("@Status", status.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.Value);
                    cmd.Parameters.AddWithValue("@Gender", gender.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Photo", studentPhoto ?? new byte[0]);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student inserted successfully.");
                    LoadDataIntoGridView(); // Refresh the DataGridView
                    ClearFields(); // Clear fields after insert
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddTeachersForm f= new AddTeachersForm();   
            f.Show();
            hideSubmenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            showSubmenu(panel6);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            DialogResult check = MessageBox.Show("Are you sure you want to Cancel the process?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form2 lForm = new Form2();
                lForm.Show();
                this.Hide();
            }
            hideSubmenu();
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

            hideSubmenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
