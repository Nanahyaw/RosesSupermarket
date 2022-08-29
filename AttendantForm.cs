using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BlessedSupermarket
{
    public partial class AttendantForm : Form
    {
        public AttendantForm()
        {
            InitializeComponent();
        }

       
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True; Connect Timeout = 30");
        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into AttendantTbl values(" + AttendantId.Text + ", '" + AttendantName.Text + "', '" + AttendantAge.Text + "', '"+ AttendantPhone.Text + "','"+AttendantPass.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Attendant Added Successfully");
                Con.Close();
                populate();
                AttendantId.Text = "";
                AttendantName.Text = "";
                AttendantAge.Text = "";
                AttendantPhone.Text = "";
                AttendantPass.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from AttendantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AttendantDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Attendant_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void AttendantDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AttendantId.Text = AttendantDGV.SelectedRows[0].Cells[0].Value.ToString();
            AttendantName.Text = AttendantDGV.SelectedRows[0].Cells[1].Value.ToString();
            AttendantAge.Text = AttendantDGV.SelectedRows[0].Cells[2].Value.ToString();
            AttendantPhone.Text = AttendantDGV.SelectedRows[0].Cells[3].Value.ToString();
            AttendantPass.Text = AttendantDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (AttendantId.Text == "")
                {
                    MessageBox.Show("Select the Attendant to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from AttendantTbl where AttendantId = " +AttendantId.Text+ "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendant deleted successfully");
                    Con.Close();
                    populate();
                    AttendantId.Text = "";
                    AttendantName.Text = "";
                    AttendantAge.Text = "";
                    AttendantPhone.Text = "";
                    AttendantPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (AttendantId.Text == "" || AttendantName.Text == "" || AttendantAge.Text == "" || AttendantPhone.Text == "" || AttendantPass.Text == "")
                {
                    MessageBox.Show("Please fill all spaces!");

                }
                else
                {
                    Con.Open();
                    string query = "update AttendantTbl set AttendantName ='" + AttendantName.Text + "', AttendantAge='" + AttendantAge.Text + "', AttendantPhone='" + AttendantPhone.Text + "',AttendantPass='" + AttendantPass.Text + "'  where AttendantId=" + AttendantId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendant updated successfully");
                    Con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
