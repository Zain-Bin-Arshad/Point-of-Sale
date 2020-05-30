using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{
    class helperDB
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string contact { get; set; }
        public string designation { get; set; }
        public int id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int devID { get; set; }
        public int suppID { get; set; }



        static readonly string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Authenticate User
        public int authenticate(helperDB person)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            int found = 0;
            try
            {
                String sql = "SELECT dbo.fn_Login(@user, @password)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@user", person.username);
                cmd.Parameters.AddWithValue("@password", person.password);
                con.Open();
                cmd.CommandType = CommandType.Text;
                if (!(cmd.ExecuteScalar() == null))
                    found = (int)cmd.ExecuteScalar();
                else
                    found = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return found;
        }
        #endregion

        #region Insert Supplier
        public bool InsertSupplier(helperDB supplier)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteSupplier";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", supplier.fname);
                cmd.Parameters.AddWithValue("@lname", supplier.lname);
                cmd.Parameters.AddWithValue("@supp_contact", supplier.contact);
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                con.Open();
                int row = cmd.ExecuteNonQuery();
                // if query executed then value of row is greater than 0
                if (row > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Select Supplier
        public DataTable selectSupplier()
        {
            SqlConnection con = new SqlConnection(myconnstring); //static method to connect to databse
            DataTable table = new DataTable(); //temporary table to hold data
            try
            {
                String sql = "select * from tbSupplier";           // SQl query to get data
                SqlCommand cmd = new SqlCommand(sql, con);         // for exeuting commands
                SqlDataAdapter adaptor = new SqlDataAdapter(cmd);  // getting data from datatable
                con.Open();                                        // openging connection to the database
                adaptor.Fill(table);                               // fill tabae in our table
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                 //if any error then show the message
            }
            finally
            {
                con.Close();                 // Clossing the connection
            }
            return table;

        }
        #endregion
        #region Delete supplier

        public bool deleteSupplier(helperDB supplier)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "SELECT COUNT([Supp_ID]) FROM tbDelivery WHERE([Supp_ID]) = @Supp_ID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Supp_ID", supplier.id);
                con.Open();
                int SupplerExist = (int)cmd.ExecuteScalar();
                if (SupplerExist == 0)
                {
                    try
                    {
                        String sql1 = "spInsertUpdateDeleteSupplier";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@supp_id", supplier.id);
                        cmd1.Parameters.AddWithValue("@StatementType", "Delete");
                        int row = cmd1.ExecuteNonQuery();
                        // if query executed then value of row is greater than 0
                        if (row > 0)
                        { isSuccess = true; }
                        else
                        { isSuccess = false; }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

        #endregion // delete pa ExecuteScaler lga dena ha  
        #region Update Supplier
        public bool UpdateSupplier(helperDB supplier)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteSupplier";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Supp_ID", supplier.id);
                cmd.Parameters.AddWithValue("@fname", supplier.fname);
                cmd.Parameters.AddWithValue("@lname", supplier.lname);
                cmd.Parameters.AddWithValue("@supp_contact", supplier.contact);
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                con.Open();
                int row = cmd.ExecuteNonQuery();
                // if query executed then value of row is greater than 0
                if (row > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Insert Employee
        public bool InsertEmployee(helperDB emp)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteEmployee";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", emp.fname);
                cmd.Parameters.AddWithValue("@lname", emp.lname);
                cmd.Parameters.AddWithValue("@emp_contact", emp.contact);
                cmd.Parameters.AddWithValue("@emp_designation", emp.designation);
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                con.Open();
                int row = cmd.ExecuteNonQuery();
                // if query executed then value of row is greater than 0
                if (row > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

            return (InsertUsername(emp) && isSuccess) ? true : false;
        }
        #endregion
        #region Select Employee
        public DataTable selectEmployee()
        {
            SqlConnection con = new SqlConnection(myconnstring); //static method to connect to databse
            DataTable table = new DataTable(); //temporary table to hold data
            try
            {
                String sql = "select * from tbEmployee";           // SQl query to get data
                SqlCommand cmd = new SqlCommand(sql, con);         // for exeuting commands
                SqlDataAdapter adaptor = new SqlDataAdapter(cmd);  // getting data from datatable
                con.Open();                                        // openging connection to the database
                adaptor.Fill(table);                               // fill tabae in our table
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                 //if any error then show the message
            }
            finally
            {
                con.Close();                 // Clossing the connection
            }
            return table;

        }
        #endregion
        #region Delete Employee

        public bool deleteEmployee(helperDB emp)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "SELECT COUNT([Emp_ID]) FROM tbOrder WHERE([Emp_ID]) = @Emp_ID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Emp_ID", emp.id);
                con.Open();
                int SupplerExist = (int)cmd.ExecuteScalar();
                if (SupplerExist == 0)
                {
                    try
                    {
                        //delete from emloyee table
                        String sql1 = "spInsertUpdateDeleteEmployee";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@emp_id", emp.id);
                        cmd1.Parameters.AddWithValue("@StatementType", "Delete");
                        int row = cmd1.ExecuteNonQuery();
                        // if query executed then value of row is greater than 0
                        if (row > 0)
                        { isSuccess = true; }
                        else
                        { isSuccess = false; }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

        #endregion // delete pa ExecuteScaler lga dena ha  
        #region Select Username for Employee
        public String selectUsername(int ID)
        {
            SqlConnection con = new SqlConnection(myconnstring); //static method to connect to databse
            string username11 = "";
            try
            {
                String sql = "select [Username] from tbLogin WHERE Emp_ID = @ID";  // SQl query to get data
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                SqlDataReader usernameRdr = null;
                usernameRdr = cmd.ExecuteReader();
                while (usernameRdr.Read())
                {
                    username11 = Convert.ToString(usernameRdr["Username"]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                 //if any error then show the message
            }
            finally
            {
                con.Close();                 // Clossing the connection
            }
            return username11;

        }
        #endregion
        #region Update Employee
        public bool UpdateEmployee(helperDB emp)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteEmployee";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_ID", emp.id);
                cmd.Parameters.AddWithValue("@fname", emp.fname);
                cmd.Parameters.AddWithValue("@lname", emp.lname);
                cmd.Parameters.AddWithValue("@emp_contact", emp.contact);
                cmd.Parameters.AddWithValue("@emp_designation", emp.designation);
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                con.Open();
                int row = cmd.ExecuteNonQuery();
                //update username
                sql = "spInsertUpdateDeleteLogins";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", emp.id);
                cmd.Parameters.AddWithValue("@username", emp.username);
                int row1 = cmd.ExecuteNonQuery();

                // if query executed then value of row is greater than 0
                if (row > 0 && row1 > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Select Product
        public DataTable selectProduct()
        {
            SqlConnection con = new SqlConnection(myconnstring); //static method to connect to databse
            DataTable table = new DataTable(); //temporary table to hold data
            try
            {
                String sql = "select * from tbProduct";           // SQl query to get data
                SqlCommand cmd = new SqlCommand(sql, con);         // for exeuting commands
                SqlDataAdapter adaptor = new SqlDataAdapter(cmd);  // getting data from datatable
                con.Open();                                        // openging connection to the database
                adaptor.Fill(table);                               // fill tabae in our table
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                 //if any error then show the message
            }
            finally
            {
                con.Close();                 // Clossing the connection
            }
            return table;

        }
        #endregion
        #region Insert Product
        public bool InsertProduct(helperDB product)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "SELECT Dev_ID from tbDelivery WHERE Dev_ID = @devID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@devID", product.devID); // fname used as Product description
                con.Open();


                int row = 0;
                if (cmd.ExecuteScalar() != null)
                    row = (int)cmd.ExecuteScalar();

                if (row == 0)
                {
                    frmMain dateTimeObject = new frmMain();
                    sql = "Insert into tbdelivery VALUES(@dev_ID,@Supp_ID,@Dev_Date,@Dev_TIME)";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@dev_ID", product.devID);
                    cmd.Parameters.AddWithValue("@Supp_ID", product.suppID);
                    cmd.Parameters.AddWithValue("@Dev_Date", SqlDbType.Date).Value = dateTimeObject.date_addDetails_inventory_popup_txt.Value.Date;
                    cmd.Parameters.AddWithValue("@Dev_TIME", SqlDbType.Time).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();

                }
                
                    sql = "spInsertUpdateDeleteProduct";
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_dec", product.fname); // fname used as Product description
                    cmd.Parameters.AddWithValue("@P_price", product.price);
                    cmd.Parameters.AddWithValue("@P_quantity", product.quantity);
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    int row2 = cmd.ExecuteNonQuery();

                    sql = "Select MAX(P_ID) from tbProduct";
                    cmd = new SqlCommand(sql, con);
                    int pid = (int)cmd.ExecuteScalar();

                    sql = "spInsertUpdateDeleteDeliveryDec";
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dev_id", product.devID);
                    cmd.Parameters.AddWithValue("@p_id", pid);
                    cmd.Parameters.AddWithValue("@quantity", product.quantity);
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    int row1 = cmd.ExecuteNonQuery();
                    if (row1 > 0 && row2 > 0)
                    {
                        isSuccess = true;
                    }
                    else
                        isSuccess = false;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Delete Product

        public bool deleteProduct(helperDB pro)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "SELECT COUNT([P_ID]) FROM tbOrderDec WHERE([P_ID]) = @P_ID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@P_ID", pro.id);
                con.Open();

                int proExist = (int)cmd.ExecuteScalar();
                
                if (proExist == 0)
                {
                    try
                    {
                        //delete from emloyee table
                        sql = "spInsertUpdateDeleteProduct";
                        cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@P_id", pro.id);
                        cmd.Parameters.AddWithValue("@StatementType", "Delete");
                        int row = cmd.ExecuteNonQuery();
                        
                        // if query executed then value of row is greater than 0
                        if (row > 0)
                        { isSuccess = true; }
                        else
                        { isSuccess = false; }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

        #endregion   
        #region Update Product
        public bool UpdateProduct(helperDB pro)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteProduct";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID", pro.id);
                cmd.Parameters.AddWithValue("@P_Dec", pro.fname); //fname used as description
                cmd.Parameters.AddWithValue("@P_price", pro.price);
                cmd.Parameters.AddWithValue("@P_quantity", pro.quantity);
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                con.Open();
                int row1 = cmd.ExecuteNonQuery();

                //update username
                sql = "UPDATE tbDeliveryDec SET pro_quantity = @p_quantity WHERE P_ID = @P_ID";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@P_ID", pro.id);
                cmd.Parameters.AddWithValue("@p_quantity", pro.quantity);
                int row = cmd.ExecuteNonQuery();
                // if query executed then value of row is greater than 0
                if (row > 0 && row1 > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Insert OrderDec
        // devID as custID
        // id as P_ID
        // suppID as empID
        public bool InsertOrderDec(helperDB order)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "INSERT INTO tbOrderDec VALUES (@O_ID,@P_ID,@Quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@O_ID", order.devID);
                cmd.Parameters.AddWithValue("@P_ID", order.id);
                cmd.Parameters.AddWithValue("@Quantity", order.quantity);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Insert Order
        // devID as custID
        // id as O_ID
        // suppID as empID
        public bool InsertOrder(helperDB order)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                frmMain timepick = new frmMain();
                String sql = "INSERT INTO tbOrder VALUES (@Cust_ID,@Emp_ID,@Amount,@Date,@Time)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Cust_ID", order.devID);
                cmd.Parameters.AddWithValue("@Emp_ID", order.suppID);
                cmd.Parameters.AddWithValue("@Amount", order.price);
                cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = timepick.dateTimePicker3.Value.Date;
                cmd.Parameters.AddWithValue("@TIME", SqlDbType.Time).Value = DateTime.Now;
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

            return isSuccess;
        }
        #endregion

        // helper methods to entertain multitable queries
        public bool InsertUsername(helperDB emp)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            int userid = 0;
            try
            {
                con.Open();
                // gets the ID of the employee that is inserted latest
                SqlCommand maxID = new SqlCommand("Select MAX(Emp_ID) from tbEmployee", con);
                userid = (int)maxID.ExecuteScalar();
                // insert in the login table 
                String sql1 = "spInsertUpdateDeleteLogins";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@emp_id", userid);
                cmd1.Parameters.AddWithValue("@username", emp.username);
                cmd1.Parameters.AddWithValue("@password", emp.fname + emp.contact);
                cmd1.Parameters.AddWithValue("@StatementType", "Insert");

                ///using userid as row checker 
                userid = cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return (userid > 0) ? true : false;
        }

        public bool checkSupplier(int ID)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String Sql = "SELECT * from tbSupplier WHERE Supp_ID = @ID";
                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                return (cmd.ExecuteScalar() == null) ? false : true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public int selectSupplierID(helperDB delivery)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                SqlCommand cmd = new SqlCommand("Select Supp_ID from tbDelivery WHERE Dev_ID = @devID", con);
                cmd.Parameters.AddWithValue("@devID", delivery.devID);
                con.Open();
                if (cmd.ExecuteScalar() != null)

                    return (int)cmd.ExecuteScalar();
                else
                    return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
            ;
        }

        public int selectDeliveryID(int dev)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                SqlCommand cmd = new SqlCommand("Select Dev_ID from tbDeliveryDec WHERE P_ID = @devID", con);
                cmd.Parameters.AddWithValue("@devID", dev);
                con.Open();
                if (cmd.ExecuteScalar() != null)

                    return (int)cmd.ExecuteScalar();
                else
                    return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int selectCust()
        {
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String Sql = "SELECT MAX(Cust_ID) from tbCustomer";
                SqlCommand cmd = new SqlCommand(Sql, con);
                con.Open();
                if (!(cmd.ExecuteScalar() == null))
                {
                    return (int)cmd.ExecuteScalar();
                }
                else
                    return 0;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int selectOrder()
        {
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String Sql = "SELECT MAX(O_ID) from tbOrder";
                SqlCommand cmd = new SqlCommand(Sql, con);
                con.Open();
                if (!(cmd.ExecuteScalar() == null))
                {
                    return (int)cmd.ExecuteScalar();
                }
                else
                    return 0;

            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public bool updateProOrder(helperDB pro)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteProduct";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID", pro.id);
                cmd.Parameters.AddWithValue("@P_Dec", pro.fname); //fname used as description
                cmd.Parameters.AddWithValue("@P_price", pro.price);
                cmd.Parameters.AddWithValue("@P_quantity", pro.quantity);
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                con.Open();
                int row1 = cmd.ExecuteNonQuery();
                if (row1 > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

        public bool InsertCustomer(helperDB cust)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "INSERT INTO tbCustomer VALUES (@FName,@LName,@Cust_Contact)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@FName", cust.fname);
                cmd.Parameters.AddWithValue("@LName", cust.lname);
                cmd.Parameters.AddWithValue("@Cust_Contact", cust.contact);
                con.Open();
                
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

            return isSuccess;
        }


        public bool UpdateProductOrder(helperDB pro)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);
            try
            {
                String sql = "spInsertUpdateDeleteProduct";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID", pro.id);
                cmd.Parameters.AddWithValue("@P_Dec", pro.fname); //fname used as description
                cmd.Parameters.AddWithValue("@P_price", pro.price);
                cmd.Parameters.AddWithValue("@P_quantity", pro.quantity);
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;


            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
    }
}

