using Econtact.econtsctClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        ContactClass c = new ContactClass();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get the value from the input Fields
            c.FirstName = (txtFirstName.Text).Trim();
            c.LastName = (txtLastName.Text).Trim();
            c.TelNo = (txtTelNo.Text).Trim();
            c.MobileNo = (txtMobile.Text).Trim();
            c.Address = (txtAddress.Text).Trim();
            c.Gender = (cboGender.Text).Trim();

            // Insert Data into datbase using method created in ContactClass
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact added Successfully");
                // call the method
                Clear();
                LoadData();
            }
            else
            {
                MessageBox.Show("Failed to add Contact Please inform Supervisor");
            }

            
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void Clear()
        {
            // Clear text boxes
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtTelNo.Text = "";
            txtMobile.Text = "";
            txtAddress.Text = "";
            cboGender.Text = "";
            txtContactID.Text = "";

        }

        public void LoadData()
        {

            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
          
            this.dataGridView1.Columns["ContactID"].Visible = false;
        }


        private void Econtact_FormClosing(object sender, FormClosingEventArgs e)
        {
            

        }

        private void Econtact_FormClosed(object sender, FormClosedEventArgs e)
        {
            // this.Close();
            this.Dispose();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Clear();
            // get data from Gridview
            int rowIndex = e.RowIndex;
            txtContactID.Text = dataGridView1.Rows[rowIndex].Cells["ContactID"].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[rowIndex].Cells["FirstName"].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[rowIndex].Cells["LastName"].Value.ToString();
            txtTelNo.Text = dataGridView1.Rows[rowIndex].Cells["TelNo"].Value.ToString();
            txtMobile.Text = dataGridView1.Rows[rowIndex].Cells["MobileNo"].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[rowIndex].Cells["Address"].Value.ToString();
            cboGender.Text = dataGridView1.Rows[rowIndex].Cells["Gender"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtContactID.Text);
            c.FirstName = (txtFirstName.Text).Trim();
            c.LastName = (txtLastName.Text).Trim();
            c.TelNo = (txtTelNo.Text).Trim();
            c.MobileNo =(txtMobile.Text).Trim();
            c.Address =(txtAddress.Text).Trim();
            c.Gender = (cboGender.Text).Trim();

            // Update Datbase
            bool success = c.Update(c);
            if (success == true)
            {
                // updated successfully
                MessageBox.Show("Contact has been successfully updated");
                LoadData();
            }
            else
            {
                MessageBox.Show("Contact failed to Update");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            c.ContactID = Convert.ToInt32(txtContactID.Text);
            // Update Datbase
            bool success = c.DELETE(c);
            if (success == true)
            {
                // updated successfully
                MessageBox.Show("Contact has been successfully Removed");
                LoadData();
                Clear();
            }
            else
            {
                MessageBox.Show("Contact failed to Delete");
            }
        }       

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the value from the input Fields
            c.FirstName = (txtFirstName.Text).Trim();
            c.LastName = (txtLastName.Text).Trim();
            c.TelNo = (txtTelNo.Text).Trim();
            c.MobileNo = (txtMobile.Text).Trim();
            c.Address = (txtAddress.Text).Trim();
            c.Gender = (cboGender.Text).Trim();


            DataTable dt = c.Search(c);
            dataGridView1.DataSource = dt;
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (btnHide.Text == "Show")
            {
                this.dataGridView1.Columns["ContactID"].Visible = true;
                btnHide.Text = "Hide";
            }
            else
            {
                this.dataGridView1.Columns["ContactID"].Visible = false;
                btnHide.Text = "Show";
            }
        }
    }
}
