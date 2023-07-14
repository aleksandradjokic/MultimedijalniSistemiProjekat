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
    public partial class KuwaharaBlur : Form
    {
        public KuwaharaBlur()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.textBox1.Text = "2";

        }

        private void KuwaharaBlur_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;

        }
        public int blur
        {
            
            get
            {
                if (this.textBox1.Text == "")
                        return 2;
                    else
                        return (Convert.ToInt32(textBox1.Text));
                }
                set { textBox1.Text = value.ToString(); }
         
        }
    }
}
