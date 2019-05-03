using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact.econtsctClasses
{
    class ContactClass
    {

        // Getter Setter Properties
        // Acts as Data Carrier in our Apllication
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        // Selecting Data from Database

        public DataTable Select()
        {
            // Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_Contact";
                // creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool Insert(ContactClass c)
        {
            // Creating a default returntype and setting its value to false
            bool isSuccess = false;
            // Step 1: Connect to database
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // Step 2: Create a SQL Query to Insert Data
                string sql = "INSERT INTO tbl_Contact (FirstName, LastName, TelNo, MobileNo, Address, Gender) VALUES (@FirstName, @LastName, @TelNo, @MobileNo, @Address, @Gender)";
                // creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create Parameters to add data
                cmd.Parameters.AddWithValue("FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("LastName", c.LastName);
                cmd.Parameters.AddWithValue("TelNo", c.TelNo);
                cmd.Parameters.AddWithValue("MobileNo", c.MobileNo);
                cmd.Parameters.AddWithValue("Address", c.Address);
                cmd.Parameters.AddWithValue("Gender", c.Gender);


                // Open Database 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If query successfull rows value > 0. Failed rows value = 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        public bool Update(ContactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // sql to Upadte the database
                string sql = "UPDATE tbl_Contact SET FirstName=@FirstName, LastName=@LastName, TelNo=@TelNo, MobileNo=@MobileNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                // creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create Parameters to add data
                cmd.Parameters.AddWithValue("FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("LastName", c.LastName);
                cmd.Parameters.AddWithValue("TelNo", c.TelNo);
                cmd.Parameters.AddWithValue("MobileNo", c.MobileNo);
                cmd.Parameters.AddWithValue("Address", c.Address);
                cmd.Parameters.AddWithValue("Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                // Open Database 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If query successfull rows value > 0. Failed rows value = 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;

        }

        public bool DELETE(ContactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // sql to Delete data in the database
                string sql = "DELETE FROM tbl_Contact WHERE ContactID=@ContactID";
                // creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create Parameters to DELETE data
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                // Open Database 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If query successfull rows value > 0. Failed rows value = 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;

        }

        public DataTable Search(ContactClass c)
        {
            // Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try

            {
                int searchNo = 0;
                string sql = "";

                if (c.FirstName.Length > 0) { searchNo = searchNo + 1; }
                if (c.LastName.Length > 0) { searchNo = searchNo + 2; }

                switch (searchNo)
                {
                                      
                    case 1:
                        sql = "SELECT * FROM tbl_Contact WHERE FirstName LIKE '%" + c.FirstName + "%'";
                        break;
                    case 2:
                        sql = "SELECT * FROM tbl_Contact WHERE LastName LIKE '%" + c.LastName + "%'";
                        break;
                    case 3:
                        sql = "SELECT * FROM tbl_Contact WHERE FirstName LIKE '%" + c.FirstName + "%' AND LastName LIKE '%" + c.LastName + "%'";
                        break;
                    default:
                        break;
                }

                if (sql.Length > 0)
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // creating SQL DataAdapter using cmd
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                } 


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            
                return dt;
        }


    }
}
