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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
            populate();
         

        }
        SqlConnection conn = new SqlConnection("server=DESKTOP-PV4FE4S\\SQLEXPRESS;database=supermarket;integrated security=true");
        private void populate()
        {
            conn.Open();
            string query = "select * from sellers";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVSellers.DataSource = ds.Tables[0];

            conn.Close();

        }
        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "insert into sellers values(" + txtSellerID.Text + ",'" + txtSellerName.Text + "'," + txtSellerAge.Text + "," + txtSellerMobile.Text + ",'" + txtSellerPassword.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully", " Add Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
                conn.Close();
                populate();
                txtSellerID.Text = "";
                txtSellerName.Text = "";
                txtSellerAge.Text = "";
                txtSellerMobile.Text = "";
                txtSellerPassword.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Seller_Load(object sender, EventArgs e)
        {
          
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Products prod = new Products();
            prod.Show();
            this.Hide();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling selling = new Selling();
            selling.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellerID.Text == "" || txtSellerName.Text == "" || txtSellerAge.Text == "" || txtSellerMobile.Text == "" || txtSellerPassword.Text == "")
                {
                    MessageBox.Show("Missing Information","Informatin Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                }
                else
                {
                    conn.Open();
                    string query = "update sellers set SellerName='" + txtSellerName.Text + "',SellerAge=" + txtSellerAge.Text + ",SellerMobile=" + txtSellerMobile.Text + ",Password=" + txtSellerPassword.Text + " where SellerId=" + txtSellerID.Text + "; ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated", "Update Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    txtSellerID.Text = "";
                    txtSellerName.Text = "";
                    txtSellerAge.Text = "";
                    txtSellerMobile.Text = "";
                    txtSellerPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellerID.Text == "")
                {
                    MessageBox.Show("Select the Seller to Delete","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    conn.Open();
                    string query = "delete from sellers where SellerId=" + txtSellerID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully", "Delete Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    txtSellerID.Text = "";
                    txtSellerName.Text = "";
                    txtSellerAge.Text = "";
                    txtSellerMobile.Text = "";
                    txtSellerPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void DGVSellers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            this.Hide();
            category.Show();    
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DGVSellers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtSellerID.Text = DGVSellers.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSellerName.Text = DGVSellers.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSellerAge.Text = DGVSellers.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSellerMobile.Text = DGVSellers.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSellerPassword.Text = DGVSellers.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}
