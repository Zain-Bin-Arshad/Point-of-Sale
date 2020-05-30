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

namespace Bakery
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();

        }
       
        private void Splash_Load(object sender, EventArgs e)
        {
            
        }

        int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            i += 10;

            rectangleShape2.Width += 65;
            num.Text =  i+"%";

            if (rectangleShape2.Width >= 650)
            {
                    timer1.Stop();
                    this.Hide();
                    frmLogin f2 = new frmLogin();
                    f2.Show();
            }
           
        }
        

        
    }
}
