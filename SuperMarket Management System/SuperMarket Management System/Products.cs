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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
           
        }

        SqlConnection conn = new SqlConnection("server=DESKTOP-PV4FE4S\\SQLEXPRESS;database=supermarket;integrated security=true");


        private void FillCategory()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CatName from categories", conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            comboxSearch.ValueMember = "CatName";
            comboxSearch.DataSource = dt;
            comboBoxSelectCategory.ValueMember = "CatName";
            comboBoxSelectCategory.DataSource = dt;
            conn.Close();

        }
        private void populate()
        {
            conn.Open();
            string query = "select * from products";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            conn.Close();

        }
        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            FillCategory();
            populate();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                string query = "insert into products values(" + txtProductID.Text + ",'" + txtProductName.Text + "'," + txtProductQuantity.Text + "," + txtProductPrice.Text + ",'" + comboBoxSelectCategory.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully", "Add Informatin ", MessageBoxButtons.OK,MessageBoxIcon.Information);
                conn.Close();
                populate();
                //cmd.Parameters.Clear();
                populate();
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductPrice.Text = "";
                txtProductQuantity.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Hide();
        }

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.Text = ProductsDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtProductName.Text = ProductsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductQuantity.Text = ProductsDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtProductPrice.Text = ProductsDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBoxSelectCategory.SelectedValue = ProductsDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text == "" || txtProductName.Text == "" || txtProductQuantity.Text == "" || txtProductPrice.Text == "")
                {
                    MessageBox.Show("Missing Information","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                }
                else
                {
                    conn.Open();
                    string query = "update products set ProdName='" + txtProductName.Text + "',ProdQty=" + txtProductQuantity.Text + ",ProdPrice=" + txtProductPrice.Text + ",ProdCat='" + comboBoxSelectCategory.SelectedValue.ToString() + "' where ProdId=" + txtProductID.Text + "; ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated","Update Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductQuantity.Text = "";
                    txtProductPrice.Text = "";
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
                if (txtProductID.Text == "")
                {
                    MessageBox.Show("Select the Product to Delete","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    conn.Open();
                    string query = "delete from products where ProdId=" + txtProductID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully","Delete Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductQuantity.Text = "";
                    txtProductPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxSelectCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            conn.Open();
            string query = "select * from products where ProdCat='" + comboBoxSelectCategory.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller seller = new Seller();
            this.Hide();
            seller.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxSelectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling sell = new Selling();
            this.Hide();
            sell.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();                                                   
        }

        private void txtProductQuantity_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
