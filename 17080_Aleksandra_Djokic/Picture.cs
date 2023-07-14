using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17080_Aleksandra_Djokic
{
    public partial class Picture : Form
    {
        public Picture()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.radioButton1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public int br
        {

            get
            {
                if (this.radioButton1.Checked == true)
                    return 1;
                else if(this.radioButton1.Checked == true)
                    return 2;
                else
                    return 3;
            }
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
