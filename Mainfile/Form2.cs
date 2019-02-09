using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIproject
{
    public partial class custom : Form
    {
        public custom()
        {
            InitializeComponent();
        }
        int dir = 1;
        private void str_Tick(object sender, EventArgs e)
        {
            if (bunifuCircleProgressbar1.Value == 100)
            {
                dir = -1;
            }
            else
            {
                dir = +1;
            }
        }

        private void custom_Load(object sender, EventArgs e)
        {

        }
    }
}
