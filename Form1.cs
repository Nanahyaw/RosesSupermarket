using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlessedSupermarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Attendantname = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True; Connect Timeout = 30");

        private void label3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
            }
            else
            {
                if (comboBox1.SelectedIndex > -1)
                {
                    if (comboBox1.SelectedItem.ToString() == "Admin")
                    {
                        if (textBox1.Text == "Admin" && textBox2.Text == "Admin123")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are an Admin enter correct ID and Password");
                        }
                    }
                    else
                    {
                        // MessageBox.Show("You are in the Shop Attendant Section");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from AttendantTbl where AttendantName = '" + textBox1.Text + "' and AttendantPassword= '" + textBox2.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {

                            Attendantname = textBox1.Text;
                            Sales attend = new Sales();
                            attend.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Role");
                }
            }

        }

        private void RoleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RoleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
