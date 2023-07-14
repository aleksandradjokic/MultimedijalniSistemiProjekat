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
    public partial class GammaInput : Form
    {
        public GammaInput()
        {
            InitializeComponent();
			ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}
		public double red
		{
			get
			{
				return (Convert.ToDouble(R.Text));
			}
			set { R.Text = value.ToString(); }
		}

		public double green
		{
			get
			{
				return (Convert.ToDouble(G.Text));
			}
			set { G.Text = value.ToString(); }
		}

		public double blue
		{
			get
			{
				return (Convert.ToDouble(B.Text));
			}
			set { B.Text = value.ToString(); }
		}

        private void ok_Click(object sender, EventArgs e)
        {
			ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

        private void Cancel_Click(object sender, EventArgs e)
        {
			Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}
    }
}
