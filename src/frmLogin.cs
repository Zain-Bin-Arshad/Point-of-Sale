using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public static int loginID = 0;
        private void logIn_button_Click(object sender, EventArgs e)
        {    
            helperDB log = new helperDB();
            log.username = UserName_loginForm.Text.Trim(); // removes the spaces at start and end
            log.password = Password_loginForm.Text.Trim();

            loginID = log.authenticate(log); // calling the sql function
            if (loginID !=0 )
            {
                if (adminRadiobtn.Checked)
                {
                    this.Hide(); // hides the login form
                    frmMain f1 = new frmMain();
                    f1.Show();
                    f1.controlAccess(false);
                    foreach (Control c in panel1.Controls) // clear the username $ password textbox
                    { if (c is TextBox) { c.Text = ""; } }
                }
                else if (userRadioBtn.Checked)
                {
                    this.Hide();
                    frmMain f1 = new frmMain();
                    f1.Show();
                    f1.controlAccess(true);
                    foreach (Control c in panel1.Controls) // clear the username $ password textbox
                    { if (c is TextBox) { c.Text = ""; } }
                }
                else
                {
                    MessageBox.Show(panel1, "Please! check whether you are USER or ADMIN !", "Incomplete Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(panel1, "Invalid Username or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        private void User_materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
