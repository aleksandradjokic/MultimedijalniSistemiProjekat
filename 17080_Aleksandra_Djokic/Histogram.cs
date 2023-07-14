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
    public partial class Histogram : Form
    {
        public Histogram()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.textBox5.Text = "0";
			this.textBox6.Text = "255";
        }
		public int min
		{
			get
			{
				if (textBox5.Text == "")
					return 0;
				else
					return (Convert.ToInt32(textBox5.Text));
			}
			set { textBox5.Text = value.ToString(); }
		}

		public int max
		{
			get
			{
				if (textBox6.Text == "")
					return 255;
				else return (Convert.ToInt32(textBox6.Text));
			}
			set { textBox6.Text = value.ToString(); }
		}
		private void button1_Click(object sender, EventArgs e)
        {
			
			if (int.Parse(textBox5.Text) > 255 || int.Parse(textBox5.Text) <0 || textBox5.Text == "")
			{
				textBox5.Text = "0";
			}
			if (int.Parse(textBox6.Text) > 255 || int.Parse(textBox6.Text) < 0 || textBox6.Text == "")
			{
				textBox6.Text = "255";
			}
			if(int.Parse(textBox6.Text)< int.Parse(textBox5.Text))
            {
				int pom = int.Parse(textBox6.Text);
				textBox6.Text = textBox5.Text;
				textBox5.Text = pom.ToString();

			}
		}
    }
}
