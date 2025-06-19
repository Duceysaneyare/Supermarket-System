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
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("server=DESKTOP-PV4FE4S\\SQLEXPRESS;database=supermarket;integrated security=true");

        private void populate()
        {
            conn.Open();
            string query = "select ProdName,ProdQty from products";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void populatebills()
        {
            conn.Open();
            string query = "select * from bills";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void FillCategory()
        {
            //This Method will bind the Combobox with the Database
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CatName from categories", conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            //cbSearchCategory.ValueMember = "CatName";
            //cbSearchCategory.DataSource = dt;
            comboxSelectCategory.ValueMember = "CatName";
            comboxSelectCategory.DataSource = dt;
            conn.Close();
        }
       

        private void Selling_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            FillCategory();
            lblSellerName.Text = Login.SellerName;
        }
          int Grdtotal = 0,n = 0;

      
        private void comboxSelectCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {

            conn.Open();
            string query = "select ProdName,ProdPrice from products where ProdCat='" + comboxSelectCategory.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            this.Hide();
            cat.Show();
        }

        private void btnSelling_Click_1(object sender, EventArgs e)
        {
            Seller sell = new Seller();
            this.Hide();
            sell.Show();
        }

        private void ProdDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (txtBillID.Text == "")
            {
                MessageBox.Show("Missing Bill Id","Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "insert into bills values(" + txtBillID.Text + ",'" + lblSellerName.Text + "','" + lblDate.Text + "'," + Grdtotal.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully","Add Informatoin",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    conn.Close();
                    populatebills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            lblDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void btnCustomers_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnProducts_Click_1(object sender, EventArgs e)
        {

            Products prod = new Products();
            this.Hide();
            prod.Show();
        }

        private void guna2CirclePictureBox5_Click_1(object sender, EventArgs e)
        {
        Application.Exit();
        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" || txtProductQuantity.Text == "")
            {
                MessageBox.Show("Missing Information", "Information Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                int total = Convert.ToInt32(txtProductPrice.Text) * Convert.ToInt32(txtProductQuantity.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(OrdersDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txtProductName.Text;
                newRow.Cells[2].Value = txtProductQuantity.Text;
                newRow.Cells[3].Value = txtProductPrice.Text;
                newRow.Cells[4].Value =  Convert.ToInt32(txtProductPrice.Text) * Convert.ToInt32(txtProductQuantity.Text);
                OrdersDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total;
                lblResult.Text = " Result : " + Grdtotal;
            }
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Alla-Aamin SuperMarket", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID:" + BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
            e.Graphics.DrawString("Seller Name:" + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Bill Date:" + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount:" + BillsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("Eng Duceysane", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230, 230));
        }

        private void btnprint_Click_1(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            populate();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void ProdDGV_Click(object sender, EventArgs e)
        {
            txtProductName.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtProductPrice.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void btnCategories_Click_1(object sender, EventArgs e)
        {
            Category category = new Category();
            this.Hide();
            category.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBillID.Text == "")
                {
                    MessageBox.Show("Select the Oder to Delete","Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    {
                        conn.Open();
                        string query = "delete from bills  where BillID =" + txtBillID.Text + "";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Deleted successfully", "Delete Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        populate();
                        cmd.Parameters.Clear();
                        txtBillID.Text =
                        txtProductName.Text = "";
                        txtProductQuantity.Text = "";
                        txtProductPrice.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBillID.Text = BillsDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtProductName.Text = BillsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductQuantity.Text = BillsDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtProductPrice.Text = BillsDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
