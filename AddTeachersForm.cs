using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Portal
{
    public partial class AddTeachersForm : Form
    {

        // Define facultyPhoto as a byte array to store the image
        private byte[] facultyPhoto = null; // Initialize it as null
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";


        public AddTeachersForm()
        {
            InitializeComponent();
            customDesign();
            dataGridView1.AllowUserToAddRows = false;


            this.Load += new EventHandler(AddTeachersForm_Load);
        }

        private void AddTeachersForm_Load(object sender, EventArgs e)
        {
            // Set default row height
            dataGridView1.RowTemplate.Height = 100;

            updateBtn_Click(this, EventArgs.Empty);  // Load data into the DataGridView
        }

        // Database connection string

        // Insert Data into the Faculty table
        private void button2_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(facultyId.Text) || string.IsNullOrWhiteSpace(facultyName.Text))
            {
                MessageBox.Show("Faculty ID and Name are required.");
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Faculty (FacultyId, FacultyName, Address, Status, Position, DateOfBirth, Gender,Photo) VALUES (@FacultyId, @FacultyName, @Address, @Status, @Position, @DateOfBirth, @Gender, @Photo)", con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@FacultyId", int.Parse(facultyId.Text));
                    cmd.Parameters.AddWithValue("@FacultyName", facultyName.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);
                    cmd.Parameters.AddWithValue("@Status", status.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Position", position.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.Value.Date); // Using DateTimePicker to get date
                    cmd.Parameters.AddWithValue("@Gender", gender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Photo", facultyPhoto ?? new byte[0]);

                    //  cmd.Parameters.AddWithValue("@Photo", facultyPhoto ?? (object)DBNull.Value);


                    // Execute the insert command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Faculty Record inserted successfully.");
                    updateBtn_Click(this, EventArgs.Empty); // Refresh the DataGridView
                    ClearFields(); // Clear fields after insert



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            // Clear the input fields
            facultyId.Clear();
            facultyName.Clear();
            address.Clear();
            status.SelectedIndex = -1;
            position.SelectedIndex = -1;
            gender.SelectedIndex = -1;
            dateOfBirth.Value = DateTime.Now;
            pictureBox1.Image = null; // Clear the picture box
            facultyPhoto = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                facultyPhoto = File.ReadAllBytes(openFileDialog.FileName); // Store the image as byte array
            }
        }

        // View Data in DataGridView
        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SqlCommand cmd = new SqlCommand("SELECT * FROM Faculty");
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Faculty", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Set the data source for DataGridView
                    dataGridView1.DataSource = dt;


                    DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Photo"];
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView1.Columns["Photo"].Width = 100;  // Adjust width as needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            //
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStudentsForm f = new AddStudentsForm();
            f.Show();
            hideSubmenu();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want  to Cancel the process?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            showSubmenu(panel6);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(idDelTF.Text))
            {
                MessageBox.Show("Please enter a valid Faculty ID to delete.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if the record exists before trying to delete
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Faculty WHERE FacultyId = @FacultyId", con);
                    checkCmd.Parameters.AddWithValue("@FacultyId", int.Parse(idDelTF.Text));

                    int recordCount = (int)checkCmd.ExecuteScalar();

                    if (recordCount == 0)
                    {
                        MessageBox.Show("Faculty ID does not exist.");
                        return;
                    }

                    // Delete the record from the database
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Faculty WHERE FacultyId = @FacultyId", con);
                    deleteCmd.Parameters.AddWithValue("@FacultyId", int.Parse(idDelTF.Text));

                    deleteCmd.ExecuteNonQuery();
                    MessageBox.Show("Faculty record deleted successfully.");

                    // Optionally, clear the form after deletion
                    clearBtn_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void facultyName_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void address_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void status_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
