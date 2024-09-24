using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Portal
{
    public partial class AdminPanel : Form
    {

        readonly SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\Admin.mdf;Integrated Security=True;Connect Timeout=30");

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connect.Open();

                    String selectData = "SELECT * FROM Admin WHERE AdminID = '100-200' AND Password = 'admin100'";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        //cmd.Parameters.AddWithValue("@AdminID", textBox2.Text.Trim());
                        //cmd.Parameters.AddWithValue("@Password", textBox1.Text.Trim());
                        cmd.Parameters.Add("@AdminID", SqlDbType.VarChar).Value = textBox2.Text.Trim();
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox1.Text.Trim();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count >= 1)
                        {
                            MessageBox.Show("Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form2 mForm = new Form2();
                            mForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect AdminID/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Stack Trace: " + ex.StackTrace);  // To see where the error is happening
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
                finally
                {
                    connect.Close();
                }


            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = ShowPass.Checked ? '\0' : '*';
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ff=new Form1();
            ff.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
