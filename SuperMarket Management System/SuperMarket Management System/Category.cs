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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("server=DESKTOP-PV4FE4S\\SQLEXPRESS;database=supermarket;integrated security=true");
        private void populate()
        {
            conn.Open();
            string query = "select * from categories";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVCategory.DataSource = ds.Tables[0];
            conn.Close();

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string query = "insert into categories values('" + txtCategoryID.Text + "','" + txtCategoryName.Text + "','" + txtCategoryDescription.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added successfuly.", "Add Information  ", MessageBoxButtons.OK,MessageBoxIcon.Information);
                conn.Close();
                cmd.Parameters.Clear();
                populate();
                txtCategoryID.Text = "";
                txtCategoryName.Text = "";
                txtCategoryDescription.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DGVCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryID.Text = DGVCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCategoryName.Text = DGVCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCategoryDescription.Text = DGVCategory.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryID.Text == "" || txtCategoryName.Text == "" || txtCategoryDescription.Text == "")
                    MessageBox.Show("Missing information", "information Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                {
                    conn.Open();
                    string query = "update categories set catName= '" + txtCategoryName.Text + "',catDesc = '" + txtCategoryDescription.Text + "' where catID = " + txtCategoryID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category  Updated successfuly.", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    cmd.Parameters.Clear();
                    txtCategoryID.Text = "";
                    txtCategoryName.Text = "";
                    txtCategoryDescription.Text = "";

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
                if (txtCategoryID.Text == "")
                    MessageBox.Show("Select ID to Delete","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                {
                    {
                        conn.Open();
                        string query = "delete from categories where catID=" + txtCategoryID.Text + "";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category Deleted successfuly.", "Delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        populate();
                        cmd.Parameters.Clear();
                        txtCategoryID.Text = "";
                        txtCategoryName.Text = "";
                        txtCategoryDescription.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
          Products prod = new Products();   
            prod.Show();
            this.Hide();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller seller = new Seller();
           this .Hide();
            seller.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }

      

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling sell = new Selling();
            this.Hide();
            sell.Show();
        }
    }
} 
