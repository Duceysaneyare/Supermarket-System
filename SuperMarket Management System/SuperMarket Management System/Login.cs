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

namespace SuperMarket_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string SellerName = "";
        SqlConnection conn = new SqlConnection("server=DESKTOP-PV4FE4S\\SQLEXPRESS;database=supermarket;integrated security=true");

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtusername.Text = "";
            comboBoxRole.SelectedIndex = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtusername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter the Username and Password","Informatin Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if (comboBoxRole.SelectedIndex > -1)
                {
                    if (comboBoxRole.SelectedItem.ToString() == "Admin")
                    {
                        if (txtusername.Text == "Admin" && txtPassword.Text == "1234")
                        {
                            Category category = new Category();
                            category.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If You are Admin, Enter the Correct Username and Password","Informatin Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("You are in the Seller Section");
                        conn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(*) from sellers where SellerName='" + txtusername.Text + "' and Password='" + txtPassword.Text + "'", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = txtusername.Text;
                            Selling sell = new Selling();
                            sell.Show();
                            this.Hide();
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select the Role to Login", "Information Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
