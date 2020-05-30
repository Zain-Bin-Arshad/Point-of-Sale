/* This POS is created by Zain Bin Arshad FA16-BCS-204*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{

    public partial class frmMain : Form
    {
        public frmMain()
        {      
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }
        
        DataTable dt = new DataTable();
        public void controlAccess(bool user)
        {
            if (user)
            {
                manageSuppliersBtn.Enabled = false;
                manageUsersBtn.Enabled = false;
                checkout_GroupBox.Show();
            }
            else
            {
                checkout_GroupBox.Show();
            }

        }
        //.....///////////////// Employee ///////////////////////////////////////////

        private void manageUsersBtn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectEmployee();
            usersDataGridView.DataSource = dt;
            usersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            usersDataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //usersDataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            usersDataGridView.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            reports_GroupBox.Hide();
            manageEmployeesGroupBox.Show();
            manageSuppliersGroupBox.Hide();
            manageInventoryGroupBox.Hide();
            checkout_GroupBox.Hide();
        }

        private void createNewUsersBtn_Click(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = false;
            updateUserBtn.Enabled = false;
            deleteUserBtn.Enabled = false;
            createNewUsersBtn.Enabled = false;
            editAndDeleteUserPopup.Hide();
            addNewUser_userDetails_popup.Show();
        }

        private void userAddCancelBtn_Click_1(object sender, EventArgs e)
        {
            addNewUser_userDetails_popup.Hide();
        }


        private void updateUserBtn_Click_1(object sender, EventArgs e)
        {
            editAndDeleteUserPopup.Show();
            delete_employee_popup.Hide();
        }

        private void done_delete_employee_popup_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            bool success = false;
            if (username_delete_employee_popup_txt.Text.Equals("masteradmin") && password_delete_employee_popup_txt.Text.Equals("shimlabakery"))
            {
                helper.id = Convert.ToInt32(userId_usersInfo_txtField.Text);
                success = helper.deleteEmployee(helper);
                if (success)
                {
                    delete_employee_popup.Hide();
                    dt = helper.selectEmployee();
                    suppliersDataGridView.DataSource = dt;
                    foreach (Control c in panel9.Controls)
                    { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in delete_Supplier_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel16.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel18.Controls) { if (c is TextBox) { c.Text = ""; } }
                    delete_Supplier_popup.Hide();

                }
                else
                {
                    error_employee_popup.Show();
                }
                refresh_Users_btn.Enabled = true;
                updateUserBtn.Enabled = true;
                deleteUserBtn.Enabled = true;
                createNewUsersBtn.Enabled = true;
                delete_employee_popup.Hide();
                foreach (Control c in delete_employee_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
                foreach (Control c in panel10.Controls) { if (c is TextBox) { c.Text = ""; } }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password ! Try Again");
            }
            dt = helper.selectEmployee();
            usersDataGridView.DataSource = dt;
        }

        private void back_employee_delete_popup_Click(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = true;
            updateUserBtn.Enabled = true;
            deleteUserBtn.Enabled = true;
            createNewUsersBtn.Enabled = true;
            delete_employee_popup.Hide();
            foreach (Control c in delete_employee_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel10.Controls) { if (c is TextBox) { c.Text = ""; } }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = true;
            updateUserBtn.Enabled = true;
            deleteUserBtn.Enabled = true;
            createNewUsersBtn.Enabled = true;
            error_employee_popup.Hide();

        }

        private void deleteUserBtn_Click_1(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = false;
            updateUserBtn.Enabled = false;
            deleteUserBtn.Enabled = false;
            createNewUsersBtn.Enabled = false;
            editAndDeleteUserPopup.Hide();
            addNewUser_userDetails_popup.Hide();
            delete_employee_popup.Show();
        }

        private void updateUserBtn_Click(object sender, EventArgs e)
        {

            if (!(userId_usersInfo_txtField.Text.Equals("") || fname_usersInfo_txtField.Text.Equals("") ||
                lname_usersInfo_txtField.Text.Equals("") || userName_usersInfo_txtField.Text.Equals("") ||
                designation_usersInfo_txtField.Text.Equals("") || contact_usersInfo_txtField.Text.Equals("")))
            {
                bool success = false;
                helperDB helper = new helperDB
                {
                    id = Convert.ToInt32(userId_usersInfo_txtField.Text),
                    fname = fname_usersInfo_txtField.Text,
                    lname = lname_usersInfo_txtField.Text,
                    username = userName_usersInfo_txtField.Text,
                    designation = designation_usersInfo_txtField.Text,
                    contact = contact_usersInfo_txtField.Text
                };
                success = helper.UpdateEmployee(helper);
                if (success) ;
                //         MessageBox.Show("Cannot update employee !"); 
                else
                    dt = helper.selectEmployee();
                usersDataGridView.DataSource = dt;
                refresh_Users_btn.Enabled = false;
                updateUserBtn.Enabled = false;
                deleteUserBtn.Enabled = false;
                createNewUsersBtn.Enabled = false;
                editAndDeleteUserPopup.Show();
                addNewUser_userDetails_popup.Hide();
                foreach (Control c in panel10.Controls) { if (c is TextBox) { c.Text = ""; } }
            }
            else
            {
                MessageBox.Show("Incomplete Input !");
            }
        }


        private void updateAndDelete_DoneBtn_Click_1(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = true;
            updateUserBtn.Enabled = true;
            deleteUserBtn.Enabled = true;
            createNewUsersBtn.Enabled = true;
            editAndDeleteUserPopup.Hide();
        }

        private void userAddCancelBtn_Click(object sender, EventArgs e)
        {
            refresh_Users_btn.Enabled = true;
            updateUserBtn.Enabled = true;
            deleteUserBtn.Enabled = true;
            createNewUsersBtn.Enabled = true;
            addNewUser_userDetails_popup.Hide();
            foreach (Control c in addNewUser_userDetails_popup.Controls) { if (c is TextBox) { c.Text = ""; } }

        }



        private void userAddNewBtn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            if (!(fname_usersDetails_manageEmployee_popup_txtField.Text.Equals("") || lname_usersDetails_manageEmployee_popup_txtField.Text.Equals("") ||
                username_usersDetails_manageEmployee_popup_txtField.Text.Equals("") || designation_usersDetails_manageEmployee_popup_txtField.Text.Equals("") ||
                contact_usersDetails_manageEmployee_popup_txtField.Text.Equals("")))
            {
                bool success = false;
                helper.fname = fname_usersDetails_manageEmployee_popup_txtField.Text;
                helper.lname = lname_usersDetails_manageEmployee_popup_txtField.Text;
                helper.username = username_usersDetails_manageEmployee_popup_txtField.Text;
                helper.designation = designation_usersDetails_manageEmployee_popup_txtField.Text;
                helper.contact = contact_usersDetails_manageEmployee_popup_txtField.Text;
                
                success = helper.InsertEmployee(helper);
                if (success)
                    foreach (Control c in addNewUser_userDetails_popup.Controls) { if (c is TextBox) { c.Text = ""; } }

                else
                {
                    MessageBox.Show("Cannot Insert Employee !");
                }
            }
            else
            {
                MessageBox.Show("Incomplete Input !");
            }
            dt = helper.selectEmployee();
            usersDataGridView.DataSource = dt;

        }
        private void refresh_Users_btn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectEmployee();
            usersDataGridView.DataSource = dt;
            foreach (Control c in search_employee_txtField.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in addNewUser_userDetails_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in delete_employee_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel10.Controls) { if (c is TextBox) { c.Text = ""; } }

        }




        //..............//////////// SUPPLIER ///////////////..............................//

        private void manageSuppliersBtn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectSupplier();
            suppliersDataGridView.DataSource = dt;
            // autosize all columns according to their content
            suppliersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // fill the empty space
            suppliersDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            suppliersDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // let the last column fill the empty space when the grid or any column is resized (more natural/expected behaviour) 
            suppliersDataGridView.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            reports_GroupBox.Hide();
            manageEmployeesGroupBox.Hide();
            manageSuppliersGroupBox.Show();
            manageInventoryGroupBox.Hide();
            checkout_GroupBox.Hide();

        }


        private void updateAndDeleteSupplierDoneBtn_Click(object sender, EventArgs e)
        {
            updateAndDeleteSupplierPopup.Hide();

            updateSupplierBtn.Enabled = true;
            deleteSupplierBtn.Enabled = true;
            refresh_Supplier_Btn.Enabled = true;
            createNewSupplierBtn.Enabled = true;
        }



        private void supplierCancelBtn_Click(object sender, EventArgs e)
        {
            addNewSupplierDetailsPopup.Hide();
        }

        private void createNewSupplierBtn_Click(object sender, EventArgs e)
        {
            addNewSupplierDetailsPopup.Show();
            updateAndDeleteSupplierPopup.Hide();
            updateSupplierBtn.Enabled = false;
            deleteSupplierBtn.Enabled = false;
            refresh_Supplier_Btn.Enabled = false;
            createNewSupplierBtn.Enabled = false;
        }


        private void cancel_suppliersDetails_btn_Click_1(object sender, EventArgs e)
        {
            foreach (Control c in addNewSupplierDetailsPopup.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            addNewSupplierDetailsPopup.Hide();
            updateSupplierBtn.Enabled = true;
            deleteSupplierBtn.Enabled = true;
            refresh_Supplier_Btn.Enabled = true;
            createNewSupplierBtn.Enabled = true;
        }

        private void yesBtn_delete_Supplier_popup_Click(object sender, EventArgs e)
        {
            bool success = false;
            if (username_delete_Supplier_popup_txt.Text.Equals("masteradmin") && password_delete_Supplier_popup_txt.Text.Equals("shimlabakery"))
            {
                helperDB helper = new helperDB
                {
                    id = Convert.ToInt32(userId_SuppliersInfo_txtField.Text)
                };
                success = helper.deleteSupplier(helper);
                if (success)
                {
                    delete_Supplier_popup.Hide();
                    dt = helper.selectSupplier();
                    suppliersDataGridView.DataSource = dt;
                    foreach (Control c in panel9.Controls){ if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in delete_Supplier_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel16.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel18.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel15.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel17.Controls) { if (c is TextBox) { c.Text = ""; } }

                    delete_Supplier_popup.Hide();
                    updateSupplierBtn.Enabled = true;
                    deleteSupplierBtn.Enabled = true;
                    refresh_Supplier_Btn.Enabled = true;
                    createNewSupplierBtn.Enabled = true;

                }
                else
                {
                    error_supplier_popup.Show();
                }
            }
            else
            {
                    MessageBox.Show("Invalid Username or Password ! Try Again");
            }
        }

        private void noBtn_delete_Supplier_popup_Click(object sender, EventArgs e)
        {
            delete_Supplier_popup.Hide();

            foreach (Control c in delete_Supplier_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel16.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel18.Controls) { if (c is TextBox) { c.Text = ""; } }

            updateSupplierBtn.Enabled = true;
            deleteSupplierBtn.Enabled = true;
            refresh_Supplier_Btn.Enabled = true;
            createNewSupplierBtn.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {


            updateSupplierBtn.Enabled = true;
            deleteSupplierBtn.Enabled = true;
            refresh_Supplier_Btn.Enabled = true;
            createNewSupplierBtn.Enabled = true;
            error_supplier_popup.Hide();
        }


        private void updateSupplierBtn_Click(object sender, EventArgs e)
        {
            try
            {
                helperDB helper = new helperDB();
                if (!(fname_SuppliersInfo_txtField.Text.Equals("") || lname_SuppliersInfo_txtField.Text.Equals("") || contact_suppliersInfo_txtField.Text.Equals("")))
                {

                    helper.id = Convert.ToInt32(userId_SuppliersInfo_txtField.Text);
                    helper.fname = fname_SuppliersInfo_txtField.Text;
                    helper.lname = lname_SuppliersInfo_txtField.Text;
                    helper.contact = contact_suppliersInfo_txtField.Text;
                    
                    bool success = helper.UpdateSupplier(helper);
                    if (!success)
                        MessageBox.Show("Failed to Update Supplier !");


                    updateAndDeleteSupplierPopup.Show();
                    addNewSupplierDetailsPopup.Hide();
                    delete_Supplier_popup.Hide();
                    foreach (Control c in panel15.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel17.Controls) { if (c is TextBox) { c.Text = ""; } }

                    foreach (Control c in panel16.Controls) { if (c is TextBox) { c.Text = ""; } }
                    foreach (Control c in panel18.Controls) { if (c is TextBox) { c.Text = ""; } }
                    updateSupplierBtn.Enabled = false;
                    deleteSupplierBtn.Enabled = false;
                    refresh_Supplier_Btn.Enabled = false;
                    createNewSupplierBtn.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Incomplete input !");
                }
                dt = helper.selectSupplier();
                suppliersDataGridView.DataSource = dt;

            }
            catch(Exception )
            { }

        }

        private void deleteSupplierBtn_Click(object sender, EventArgs e)
        {
            updateAndDeleteSupplierPopup.Hide();
            addNewSupplierDetailsPopup.Hide();
            delete_Supplier_popup.Show();
            updateSupplierBtn.Enabled = false;
            deleteSupplierBtn.Enabled = false;
            refresh_Supplier_Btn.Enabled = false;
            createNewSupplierBtn.Enabled = false;
        }


        private void refresh_Supplier_Btn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectSupplier();
            suppliersDataGridView.DataSource = dt;
            foreach (Control c in addNewSupplierDetailsPopup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in delete_Supplier_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel16.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel18.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel21.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in search_supplier_txtField.Controls) { if (c is TextBox) { c.Text = ""; } }

        }

        private void addNew_suppliersDetails_btn_Click(object sender, EventArgs e)
        {
            try
            {
                helperDB helper = new helperDB();
                if (!(fname_suppliersDetails_popup_txtField.Text.Equals("") || lname_suppliersDetails_popup_txtField.Equals("") || contact_suppliersDetails_popup_txtField.Text.Equals("")))
                {
                    helper.fname = fname_suppliersDetails_popup_txtField.Text;
                    helper.lname = lname_suppliersDetails_popup_txtField.Text;
                    helper.contact = contact_suppliersDetails_popup_txtField.Text;
                   
                    bool success = helper.InsertSupplier(helper);
                    if (!success)
                        MessageBox.Show("Failed to insert data !");

                    foreach (Control c in addNewSupplierDetailsPopup.Controls) { if (c is TextBox) { c.Text = ""; } }

                }
                else
                {
                    MessageBox.Show("incomplete input");
                }

                dt = helper.selectSupplier();
                suppliersDataGridView.DataSource = dt;
            }
            catch (Exception)
            {

            }
        }
        // ////////////////////  INVENTORY ////////////////////////////////////////////////////////

        private void inventoryBtn_Click(object sender, EventArgs e)
        {
            
            helperDB helper = new helperDB();
            dt = helper.selectProduct();
            inventoryGridBox.DataSource = dt;
            inventoryGridBox.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            inventoryGridBox.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            inventoryGridBox.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            inventoryGridBox.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            reports_GroupBox.Hide();
            manageInventoryGroupBox.Show();
            manageSuppliersGroupBox.Hide();
            manageEmployeesGroupBox.Hide();
            checkout_GroupBox.Hide();
        }

        private void addItem_Inventory_btn_Click(object sender, EventArgs e)
        {
            refresh_Inventory_btn.Enabled = false;
            updateItem_Inventory_Btn.Enabled = false;
            delete__Inventory_Btn.Enabled = false;
            addItem_Inventory_btn.Enabled = false;
            update_inventory_popup.Hide();
            devID_addItem_inventory_popup.Show();
            Delete_inventory_popup.Hide();
        }

        private void done_updateAndDelete_Popup_Btn_Click(object sender, EventArgs e)
        {
            foreach (Control c in time_addDetails_inventory_popup_txt.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            foreach (Control c in panel4.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            refresh_Inventory_btn.Enabled = true;
            updateItem_Inventory_Btn.Enabled = true;
            delete__Inventory_Btn.Enabled = true;
            addItem_Inventory_btn.Enabled = true;
            update_inventory_popup.Hide();
            Delete_inventory_popup.Hide();
        }

        private void updateItem_Inventory_Btn_Click(object sender, EventArgs e)
        {
            bool success = false;
            helperDB helper = new helperDB();
            if (!(pName_inventory_txtField.Text.Equals("") || quantity_inventory_txtField.Text.Equals("") || increasedBy_inventory_txtField.Text.Equals("")))
            {
               
                helper.id = Convert.ToInt32(prodId_inventory_txtField.Text);
                helper.fname = pName_inventory_txtField.Text;
                helper.price = Convert.ToInt32(increasedBy_inventory_txtField.Text);
                helper.quantity = Convert.ToInt32(quantity_inventory_txtField.Text);
                success = helper.UpdateProduct(helper);
                if (!(success))
                {
                    msgUpdated_Inventory_txt.Text = "Product Not Updated";
                    update_inventory_popup.Show();
                //    MessageBox.Show("Cannot update product !");
                }

            }
            else
            {
                msgUpdated_Inventory_txt.Text = "Product Updated";
                update_inventory_popup.Show();
            }
            
            refresh_Inventory_btn.Enabled = false;
            updateItem_Inventory_Btn.Enabled = false;
            delete__Inventory_Btn.Enabled = false;
            addItem_Inventory_btn.Enabled = false;
            devID_addItem_inventory_popup.Hide();
            Delete_inventory_popup.Hide();
            update_inventory_popup.Show();
            if (!(success))
                foreach (Control c in time_addDetails_inventory_popup_txt.Controls)
                {
                if (c is TextBox)
                {
                    c.Text = "";
                }
                }
            foreach (Control c in panel4.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            dt = helper.selectProduct();
            inventoryGridBox.DataSource = dt;

        }

        private void delete__Inventory_Btn_Click(object sender, EventArgs e)
        {
            refresh_Inventory_btn.Enabled = false;
            updateItem_Inventory_Btn.Enabled = false;
            delete__Inventory_Btn.Enabled = false;
            addItem_Inventory_btn.Enabled = false;
            devID_addItem_inventory_popup.Hide();
            update_inventory_popup.Hide();
            Delete_inventory_popup.Show();
        }


        private void done_deletePopup_Inventory_Btn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            bool success = false;
            if (username_delete_inventory_popup.Text.Equals("masteradmin") && password_delete_inventory_popup.Text.Equals("shimlabakery"))
            {
                helper.id = Convert.ToInt32(prodId_inventory_txtField.Text);
                success = helper.deleteProduct(helper);
                if (success)
                {
                    delete_employee_popup.Hide();
                    dt = helper.selectProduct();
                    inventoryGridBox.DataSource = dt;

                }
                else
                {
                    error_inventory_popup_box.Show();
                }
                refresh_Inventory_btn.Enabled = true;
                updateItem_Inventory_Btn.Enabled = true;
                delete__Inventory_Btn.Enabled = true;
                addItem_Inventory_btn.Enabled = true;
                Delete_inventory_popup.Hide();
                update_inventory_popup.Hide();

                foreach (Control c in time_addDetails_inventory_popup_txt.Controls) { if (c is TextBox) { c.Text = ""; } }
                foreach (Control c in panel4.Controls) { if (c is TextBox) { c.Text = ""; } }
                foreach (Control c in Delete_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password! Try Again...");
            }
        }

        private void back_deletePopup_Inventory_Btn_Click(object sender, EventArgs e)
        {
            refresh_Inventory_btn.Enabled = true;
            updateItem_Inventory_Btn.Enabled = true;
            delete__Inventory_Btn.Enabled = true;
            addItem_Inventory_btn.Enabled = true;
            Delete_inventory_popup.Hide();
            update_inventory_popup.Hide();

            foreach (Control c in time_addDetails_inventory_popup_txt.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel4.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in Delete_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
        }

        private void cancel_addDetails_inventory_btn_Click_1(object sender, EventArgs e)
        {
            refresh_Inventory_btn.Enabled = true;
            updateItem_Inventory_Btn.Enabled = true;
            delete__Inventory_Btn.Enabled = true;
            addItem_Inventory_btn.Enabled = true;
            devID_addItem_inventory_popup.Hide();
            foreach (Control c in devID_addItem_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            refresh_Inventory_btn.Enabled = true;
            updateItem_Inventory_Btn.Enabled = true;
            delete__Inventory_Btn.Enabled = true;
            addItem_Inventory_btn.Enabled = true;
            update_inventory_popup.Hide();
            devID_addItem_inventory_popup.Hide();
            Delete_inventory_popup.Hide();
            error_inventory_popup_box.Hide();
        }


        private void addNew_addDetails_inventory_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(name_addDetails_inventory_popup.Text.Equals("") || quantity_addDetails_inventory_popup.Text.Equals("") ||
                  originalPrice_addDetails_inventory_popup.Text.Equals("") || devID_addDetails_inventory_popup.Text.Equals("") ||
                  supplierID_addDetails_inventory_popup_txt.Text.Equals("")))
                {
                    helperDB helper = new helperDB();

                    bool success = helper.checkSupplier(Convert.ToInt32(supplierID_addDetails_inventory_popup_txt.Text));
                    if (success)
                    {
                        helper.fname = name_addDetails_inventory_popup.Text;
                        helper.price = Convert.ToInt32(originalPrice_addDetails_inventory_popup.Text);
                        helper.quantity = Convert.ToInt32(quantity_addDetails_inventory_popup.Text);
                        helper.devID = Convert.ToInt32(devID_addDetails_inventory_popup.Text);
                        helper.suppID = Convert.ToInt32(supplierID_addDetails_inventory_popup_txt.Text);
                        success = helper.InsertProduct(helper);
                        if (success) ;
                        else
                        {
                            MessageBox.Show("Cannot Insert Product !");
                        }
                        dt = helper.selectProduct();
                        inventoryGridBox.DataSource = dt;
                    }
                    else
                    { MessageBox.Show("Shupplier Not Found !"); }
                }
                else
                { MessageBox.Show("Incomplete Input !"); }
            }
            catch(Exception)
            {

            }


            supplierID_addDetails_inventory_popup_txt.Enabled = true;
            foreach (Control c in devID_addItem_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
        }

        private void refresh_Inventory_btn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectProduct();
            inventoryGridBox.DataSource = dt;
            foreach (Control c in search_inventory_txtField.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in devID_addItem_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in time_addDetails_inventory_popup_txt.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel4.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in Delete_inventory_popup.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in manageInventoryGroupBox.Controls) { if (c is TextBox) { c.Text = ""; } }
        }

        ///////////////////////////////CHCECKOUT///////////////////////////////////////////
        
        private void checkoutBtn_Click(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            empID_Invoice_txtField.Text = Convert.ToString(frmLogin.loginID);
            int custid = helper.selectCust();
            custID_Invoice_txtField.Text = Convert.ToString(custid + 1);
            int oid = helper.selectOrder();
            orderId_Invoice_txtBox.Text = Convert.ToString(oid + 1);
            

            searchProduct_Invoice_dataGridView1.DataSource = helper.selectProduct();
            searchProduct_Invoice_dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            searchProduct_Invoice_dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            searchProduct_Invoice_dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            searchProduct_Invoice_dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            reports_GroupBox.Hide();
            manageEmployeesGroupBox.Hide();
            manageSuppliersGroupBox.Hide();
            manageInventoryGroupBox.Hide();
            checkout_GroupBox.Show();
        }

        private void button5_Click(object sender, EventArgs e) // checkout buttin
        {
            helperDB helper = new helperDB();
            try
            {
                if (!(cashReceived_Checkout_txtField.Text.Equals("")))
                { 
                    helper.fname = custFName_Checkout_txtField.Text;
                    helper.lname = custLName_Checkout_txtField.Text;
                    helper.contact = custContact_Checkout_txtField.Text;

                    if (helper.InsertCustomer(helper))
                    {
                        helper.price = Convert.ToInt32(totalSale_Checkout_txtField.Text); //amount
                        helper.devID = Convert.ToInt32(custID_Invoice_txtField.Text);//custID
                        helper.suppID = Convert.ToInt32(empID_Invoice_txtField.Text); //Emp ID
                        if (helper.InsertOrder(helper))
                        {
                            foreach (DataGridViewRow row in checkout_DataGridView.Rows)
                            {
                                helper.devID = Convert.ToInt32(orderId_Invoice_txtBox.Text);//OrderID
                                helper.id = Convert.ToInt32(row.Cells[0].Value);// pID
                                helper.quantity = Convert.ToInt32(row.Cells[2].Value); // quanityt
                                bool ok = helper.InsertOrderDec(helper);
                                
                                helper.quantity = Convert.ToInt32(qtyAvailable_checkout_txtField.Text)-helper.quantity ;
                                helper.fname = row.Cells[1].Value.ToString();
                                helper.price = Convert.ToInt32(currentPrice_checkout_txtField.Text);
                                bool ok2 = helper.UpdateProductOrder(helper);
                                if (!(ok || ok2 ))
                                {
                                    msg_checkout_popup.Show();
                                    msg_checkout_lbl.Text = "Cannot insert OrderDec OR Update Product";
                                //    MessageBox.Show("Cannot insert OrderDec OR Update Product");
                                    return;
                                }
                            }
                           
                        }
                        else
                        {
                            msg_checkout_popup.Show();
                            msg_checkout_lbl.Text = "Cannot Insert order !";

//                            MessageBox.Show("Cannot Insert order !");
                        }
                    }
                    else
                    {

                        msg_checkout_popup.Show();
                        msg_checkout_lbl.Text = "Cannot Insert Customer !";
  //                      MessageBox.Show("Cannot Insert Customer !");

                    }
                }
                else
                {

                    msg_checkout_popup.Show();
                    msg_checkout_lbl.Text = "Invalid Amount Received!";
                   // MessageBox.Show("Invalid Amount Received !");
                }

                empID_Invoice_txtField.Text = Convert.ToString(frmLogin.loginID);
                int custid = helper.selectCust();
                custID_Invoice_txtField.Text = Convert.ToString(custid + 1);
                int oid = helper.selectOrder();
                orderId_Invoice_txtBox.Text = Convert.ToString(oid + 1);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            button5.Enabled = false;
            addToCart_checkout_btn.Enabled = false;
            deleteFromCart_checkout_btn.Enabled = false;
            msg_checkout_popup.Show();
            msg_checkout_lbl.Text = "Checkout Completed!";
            dt = helper.selectProduct();
            searchProduct_Invoice_dataGridView1.DataSource = dt;

        }

        private void addToCart_checkout_txtField_Click(object sender, EventArgs e)
        {
            try
            {
                int requiredQuantity = Convert.ToInt32(qtyRequired_checkout_txtField.Text);
                int totalSale = 0;
                bool found = false, sale= false;
                if (requiredQuantity <= Convert.ToInt32(qtyAvailable_checkout_txtField.Text))
                {
                    int pid = Convert.ToInt32(textBox6.Text);
                    foreach (DataGridViewRow currentRow in checkout_DataGridView.Rows)
                    {
                        sale = true;
                        if (Convert.ToInt32(currentRow.Cells[0].Value) == pid)
                        {
                            if (Convert.ToInt32(currentRow.Cells[2].Value) + requiredQuantity > Convert.ToInt32(qtyAvailable_checkout_txtField.Text))
                            {
                                panel22.Show();
                                label17.Text = "Required Quantity is not available!";
                              //  MessageBox.Show("Required Quantity is not available !");
                                return;
                            }
                            currentRow.Cells[2].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[2].Value) + requiredQuantity);
                            currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) + Convert.ToInt32(finalPrice_checkout_txtField.Text));
                            found = true;
                        }
                        else
                            found = false;
                        totalSale += Convert.ToInt32(currentRow.Cells[3].Value); 
                    }
                   
                    if (!found)
                    {
                        string[] row = new string[10];
                        row = new[]
                        {
                            searchProduct_Invoice_dataGridView1.Rows[checkout_DataGridView_SlectedRow_index].Cells[0].Value.ToString(),
                            searchProduct_Invoice_dataGridView1.Rows[checkout_DataGridView_SlectedRow_index].Cells[1].Value.ToString(),
                            qtyRequired_checkout_txtField.Text,
                            finalPrice_checkout_txtField.Text
                        };
                        checkout_DataGridView.Rows.Add(row);
                        totalSale += Convert.ToInt32(finalPrice_checkout_txtField.Text);
                    }
                    else
                    {
                        helperDB helper = new helperDB();
                        helper.id = Convert.ToInt32(textBox6.Text);
                        helper.quantity = Convert.ToInt32(qtyRequired_checkout_txtField.Text);

                    }
                   
                    if (sale)
                        totalSale_Checkout_txtField.Text = Convert.ToString(totalSale);
                    else
                        totalSale_Checkout_txtField.Text = finalPrice_checkout_txtField.Text;

                    
                }
                else
                {
                    MessageBox.Show("Required Quantity is not available !");
                }
                foreach (Control c in panel8.Controls) { if (c is TextBox) { c.Text = ""; } }
                foreach (Control c in panel6.Controls) { if (c is TextBox) { c.Text = ""; } }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        int checkout_DataGridView_SlectedRow_index= 0;
        private void deleteFromCart_checkout_txtField_Click(object sender, EventArgs e)
        {
            try
            {
                int totalSale = 0;
                checkout_DataGridView.Rows.RemoveAt(checkout_DataGridView_SlectedRow_index);
                foreach (DataGridViewRow currentRow in checkout_DataGridView.Rows)
                {
                    totalSale += Convert.ToInt32(currentRow.Cells[3].Value);
                }
                totalSale_Checkout_txtField.Text = Convert.ToString(totalSale);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            foreach (Control c in panel14.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel8.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel6.Controls) { if (c is TextBox) { c.Text = ""; } }
        }

        private void done_checkout_btn_Click(object sender, EventArgs e)
        {
            addToCart_checkout_btn.Enabled = true;
            deleteFromCart_checkout_btn.Enabled = true;
            button5.Enabled = true;

            msg_checkout_popup.Hide();
            foreach (Control c in checkout_GroupBox.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel5.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel14.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel8.Controls) { if (c is TextBox) { c.Text = ""; } }
            foreach (Control c in panel6.Controls) { if (c is TextBox) { c.Text = ""; } }

            helperDB helper = new helperDB();
            empID_Invoice_txtField.Text = Convert.ToString(frmLogin.loginID);
            int custid = helper.selectCust();
            custID_Invoice_txtField.Text = Convert.ToString(custid + 1);
            int oid = helper.selectOrder();
            orderId_Invoice_txtBox.Text = Convert.ToString(oid + 1);
            checkout_DataGridView.Rows.Clear();
            checkout_DataGridView.Refresh();
        }



        ///////////////////////////////////////// Reports ///////////////////////////////////////

        private void generateReportBtn_Click(object sender, EventArgs e)
        {

            reports_GroupBox.Show();
            manageEmployeesGroupBox.Hide();
            manageSuppliersGroupBox.Hide();
            manageInventoryGroupBox.Hide();
            checkout_GroupBox.Hide();
        }

        private void sales_Report_Btn_Click(object sender, EventArgs e)
        {
            design_report_lbl.Text = "Sales Report";
            designLabel_Report_panel.Show();
            DoneView_Report_Panel.Show();
            reportGridView.Show();
        }

        private void Inventory_Report_Btn_Click(object sender, EventArgs e)
        {
            design_report_lbl.Text = "Inventory Report";
            designLabel_Report_panel.Show();
            DoneView_Report_Panel.Show();
            reportGridView.Show();
        }

        private void doneView_Report_btn_Click(object sender, EventArgs e)
        {
            designLabel_Report_panel.Hide();
            DoneView_Report_Panel.Hide();
            reportGridView.Hide();
        }

        //////////////////////////////////////////// Logout ///////////////////////////////

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin f2 = new frmLogin();
            f2.Show();
          
        }

        private void manageSuppliersGroupBox_Enter(object sender, EventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectSupplier();
            suppliersDataGridView.DataSource = dt;
        }

        private void suppliersDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from datagrid view and fill the respective textfields
            int rowIndex = e.RowIndex; // get the selected row's index
            userId_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            fname_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[1].Value.ToString(); 
            lname_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[2].Value.ToString(); // name 
            contact_suppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[3].Value.ToString(); // contact of supplier
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CloneControl(suppliersDataGridView, searchProduct_Invoice_dataGridView1);
            CloneControl(suppliersDataGridView, checkout_DataGridView);
            CloneControl(suppliersDataGridView, inventoryGridBox);
        }

        private void search_employee_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (materialRadioButton3.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("Supp_ID like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else if(materialRadioButton2.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("FName like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("Supp_Contact like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
        }

        private void search_supplier_txtField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (materialRadioButton3.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("convert(Supp_ID, 'System.String') like '%{0}%'", search_supplier_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else if (materialRadioButton2.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("FName like '%{0}%'", search_supplier_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("Supp_Contact like '%{0}%'", search_supplier_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
        }

        private void usersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            helperDB helper = new helperDB();
            dt = helper.selectEmployee();
            usersDataGridView.DataSource = dt;
        }

        private void userId_usersInfo_txtField_TextChanged(object sender, EventArgs e)
        {

        }

        private void usersDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            helperDB helper = new helperDB();
            int rowIndex = e.RowIndex; // get the selected row's index
            userId_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            fname_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            lname_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[2].Value.ToString();
            userName_usersInfo_txtField.Text = helper.selectUsername(Convert.ToInt32(userId_usersInfo_txtField.Text));
            designation_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[3].Value.ToString();
            contact_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void search_employee_txtField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (materialRadioButton6.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("convert(Emp_ID, 'System.String') like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else if (materialRadioButton5.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("FName like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
            else
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("Emp_Contact like '%{0}%'", search_employee_txtField.Text);
                suppliersDataGridView.DataSource = dv.ToTable();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void checkout_GroupBox_Enter(object sender, EventArgs e)
        {

        }

        //clones selected properties of one contro;  ToolBar the other
        public static void CloneControl(Control SourceControl, Control DestinationControl)
        {
            String[] PropertiesToClone = new String[] { "AlternatingRowsDefaultCellStyle", "AutoSizeColumnsMode", "AutoSizeRowsMode",
                "BorderStyle", "BackgroundColor", "DefaultCellStyle", "GridColor", "RowHeadersDefaultCellStyle", "RowsDefaultCellStyle" };
            PropertyInfo[] controlProperties = SourceControl.GetType().GetProperties();

            foreach (String Property in PropertiesToClone)
            {
                PropertyInfo ObjPropertyInfo = controlProperties.First(a => a.Name == Property);
                ObjPropertyInfo.SetValue(DestinationControl, ObjPropertyInfo.GetValue(SourceControl));
            }
        }

        private void devID_addDetails_inventory_popup_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

     
        private void devID_addDetails_inventory_popup_Leave(object sender, EventArgs e)
        {
            try
            {
                helperDB helper = new helperDB();
                helper.devID = Convert.ToInt32(devID_addDetails_inventory_popup.Text);
                int success = helper.selectSupplierID(helper);
                if (success != 0)
                {
                    supplierID_addDetails_inventory_popup_txt.Text = success.ToString();
                    supplierID_addDetails_inventory_popup_txt.Enabled = false;
                }
                else
                {
                    supplierID_addDetails_inventory_popup_txt.Text = "";
                    supplierID_addDetails_inventory_popup_txt.Enabled = true;
                }
            }
            catch(Exception)
            { }
        }

        private void inventoryGridBox_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            helperDB helper = new helperDB();
            //get data from datagrid view and fill the respective textfields
            int rowIndex = e.RowIndex; // get the selected row's index
            prodId_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            pName_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[1].Value.ToString();
            increasedBy_inventory_txtField.Text = currentPrice_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[2].Value.ToString(); // name 
            quantity_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[3].Value.ToString(); // contact of supplier
            helper.devID = helper.selectDeliveryID(Convert.ToInt32(prodId_inventory_txtField.Text));
            deliveryId_inventory_txtField.Text = Convert.ToString(helper.devID);
            suppliers_inventory_txtField.Text = Convert.ToString(helper.selectSupplierID(helper));
        }

        private void deliveryId_inventory_txtField_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_inventory_txtField_Click(object sender, EventArgs e)
        {

        }

        private void search_inventory_txtField_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (pid_radioBtn_inventory.Checked)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format("convert(P_ID, 'System.String') like '%{0}%'", search_inventory_txtField.Text);
                    inventoryGridBox.DataSource = dv.ToTable();
                }
                else if (pName_radioBtn_inventory.Checked)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format("P_Dec like '%{0}%'", search_inventory_txtField.Text);
                    inventoryGridBox.DataSource = dv.ToTable();
                }
                else
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format("convert(P_Quantity, 'System.String') like '%{0}%'", search_inventory_txtField.Text);
                    inventoryGridBox.DataSource = dv.ToTable();    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Integer value ! ");
            }
        }

        private void inventoryGridBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            helperDB helper = new helperDB();
            //get data from datagrid view and fill the respective textfields
            int rowIndex = e.RowIndex; // get the selected row's index
            
            prodId_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            pName_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[1].Value.ToString();
            increasedBy_inventory_txtField.Text = currentPrice_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[2].Value.ToString(); // name 
            quantity_inventory_txtField.Text = inventoryGridBox.Rows[rowIndex].Cells[3].Value.ToString(); // contact of supplier
            helper.devID = helper.selectDeliveryID(Convert.ToInt32(prodId_inventory_txtField.Text));
            deliveryId_inventory_txtField.Text = Convert.ToString(helper.devID);
            suppliers_inventory_txtField.Text = Convert.ToString(helper.selectSupplierID(helper));
        }

        private void checkout_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchProduct_Invoice_dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            checkout_DataGridView_SlectedRow_index = e.RowIndex;
            int rowIndex = e.RowIndex;
            textBox6.Text = searchProduct_Invoice_dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            qtyAvailable_checkout_txtField.Text = searchProduct_Invoice_dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            currentPrice_checkout_txtField.Text = searchProduct_Invoice_dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void checkout_DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                textBox6.Text = checkout_DataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                qtyRequired_checkout_txtField.Text = checkout_DataGridView.Rows[rowIndex].Cells[3].Value.ToString();
                currentPrice_checkout_txtField.Text = checkout_DataGridView.Rows[rowIndex].Cells[2].Value.ToString();
                checkout_DataGridView_SlectedRow_index = e.RowIndex;
            }
            catch(Exception)
            {

            }
        }

        private void qtyRequired_checkout_txtField_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(qtyRequired_checkout_txtField.Text) <= Convert.ToInt32(qtyAvailable_checkout_txtField.Text))
                {
                    finalPrice_checkout_txtField.Text = Convert.ToString(Convert.ToInt32(qtyRequired_checkout_txtField.Text) * Convert.ToInt32(currentPrice_checkout_txtField.Text));
                }
                else
                {
                    panel22.Show();
                    label17.Text = "Required Quantity is not available!";
                    //          MessageBox.Show("Required Quantity is not avalaible !");
                }
            }
            catch (Exception) { }
       }


        private void cashReceived_Checkout_txtField_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cashReceived_Checkout_txtField.Text) >= Convert.ToInt32(totalSale_Checkout_txtField.Text))
                {
                    int change = Convert.ToInt32(cashReceived_Checkout_txtField.Text) - Convert.ToInt32(totalSale_Checkout_txtField.Text);
                    changeAmnt_checkout_txtField.Text = Convert.ToString(change);
                    cashReceived_special_Checkout_txtField.Text = cashReceived_Checkout_txtField.Text;
                }
                else
                {
                    panel22.Show();
                    label17.Text = "Cash Recieved is Less than Total Amount!";
              //      MessageBox.Show("Cash REcieved than sale !");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void searchProduct_checkout_txtField_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataView dv = dt.DefaultView;
                if (pid_invoice_materialRadioButton1.Checked)
                {
                    dv.RowFilter = string.Format("convert(P_ID, 'System.String') like '%{0}%'", searchProduct_checkout_txtField.Text);
                    searchProduct_Invoice_dataGridView1.DataSource = dv.ToTable();
                }
                else 
                {
                    dv.RowFilter = string.Format("P_Dec like '%{0}%'", searchProduct_checkout_txtField.Text);
                    searchProduct_Invoice_dataGridView1.DataSource = dv.ToTable();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + "\nPlease Enter Integer value ! ");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void suppliersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //get data from datagrid view and fill the respective textfields
            int rowIndex = e.RowIndex; // get the selected row's index
            userId_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            fname_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            lname_SuppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[2].Value.ToString(); // name 
            contact_suppliersInfo_txtField.Text = suppliersDataGridView.Rows[rowIndex].Cells[3].Value.ToString(); // contact of supplier

        }

        private void usersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            helperDB helper = new helperDB();
            int rowIndex = e.RowIndex; // get the selected row's index
            userId_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[0].Value.ToString(); // fills Supp ID field 
            fname_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            lname_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[2].Value.ToString();
            userName_usersInfo_txtField.Text = helper.selectUsername(Convert.ToInt32(userId_usersInfo_txtField.Text));
            designation_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[3].Value.ToString();
            contact_usersInfo_txtField.Text = usersDataGridView.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void originalPrice_addDetails_inventory_popup_TextChanged(object sender, EventArgs e)
        {

        }

        private void time_addDetails_inventory_popup_txt_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel22.Hide();
            cashReceived_Checkout_txtField.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void qtyRequired_checkout_txtField_TextChanged(object sender, EventArgs e)
        {

        }

        private void generateReportBtn_Click_1(object sender, EventArgs e)
        {
            reports_GroupBox.Show();
            manageEmployeesGroupBox.Hide();
            manageSuppliersGroupBox.Hide();
            manageInventoryGroupBox.Hide();
            checkout_GroupBox.Hide();
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

