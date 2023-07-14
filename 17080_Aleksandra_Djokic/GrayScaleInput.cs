using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace _17080_Aleksandra_Djokic
{
    public partial class GrayScaleInput : Form
    {
        
        public GrayScaleInput()
        {
            InitializeComponent();
            OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if((double.Parse(Red.Text)+ double.Parse(Green.Text)+ double.Parse(Blue.Text) )!=1)
            {
                Red.Text = "0.3";
                Green.Text = "0.59";
                Blue.Text = "0.11";
            }
                OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public double cr
        {

            get
            {
               
                double r = Convert.ToDouble(Red.Text);
                return r;
                //return (Convert.ToInt32(Red.Text, 10));
            }
            set { Red.Text = value.ToString(); }
        }

        public double cg
        {
            get
            {
                
                double g = Convert.ToDouble(Green.Text);
                return g;
            }
            set { Green.Text = value.ToString(); }
        }

        public double cb
        {
            get
            {

                
                double b = Convert.ToDouble(Blue.Text);
                return b;
            }
            set { Blue.Text = value.ToString(); }
        }

        private void Red_TextChanged(object sender, EventArgs e)
        {
            if (double.Parse(Red.Text) > 1 || double.Parse(Red.Text) < -1)
            {
                Red.Text = "0.3";
            }
            
        }

        private void Green_TextChanged(object sender, EventArgs e)
        {
            if (double.Parse(Green.Text) > 1 || double.Parse(Green.Text) < -1)
            {
                Green.Text = "0.59";
            }
            
        }

        private void Blue_TextChanged(object sender, EventArgs e)
        {
            if (double.Parse(Blue.Text) > 1 || double.Parse(Blue.Text) < -1)
            {
                Blue.Text = "0.11";
            }
           
        }

        private void GrayScaleInput_Load(object sender, EventArgs e)
        {

        }
    }
}
