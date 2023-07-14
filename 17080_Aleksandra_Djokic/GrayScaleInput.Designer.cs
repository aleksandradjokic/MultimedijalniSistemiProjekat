
namespace _17080_Aleksandra_Djokic
{
    partial class GrayScaleInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Blue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Green = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Red = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Blue
            // 
            this.Blue.Location = new System.Drawing.Point(95, 132);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(120, 22);
            this.Blue.TabIndex = 35;
            this.Blue.Text = "0";
            this.Blue.TextChanged += new System.EventHandler(this.Blue_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 34;
            this.label4.Text = "Cb";
            // 
            // Green
            // 
            this.Green.Location = new System.Drawing.Point(95, 95);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(120, 22);
            this.Green.TabIndex = 33;
            this.Green.Text = "0";
            this.Green.TextChanged += new System.EventHandler(this.Green_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 19);
            this.label3.TabIndex = 32;
            this.label3.Text = "Cg";
            // 
            // Red
            // 
            this.Red.Location = new System.Drawing.Point(95, 58);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(120, 22);
            this.Red.TabIndex = 31;
            this.Red.Text = "0";
            this.Red.TextChanged += new System.EventHandler(this.Red_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 19);
            this.label2.TabIndex = 30;
            this.label2.Text = "Cr";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 37);
            this.label1.TabIndex = 29;
            this.label1.Text = "Enter values between 0 and 1\r\n(Cr+Cg+Cb=1)";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(18, 178);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(90, 27);
            this.OK.TabIndex = 28;
            this.OK.Text = "OK";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(123, 178);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(90, 27);
            this.Cancel.TabIndex = 27;
            this.Cancel.Text = "Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // GrayScaleInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 237);
            this.Controls.Add(this.Blue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Red);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.Name = "GrayScaleInput";
            this.Text = "GrayScaleInput";
            this.Load += new System.EventHandler(this.GrayScaleInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Blue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Green;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Red;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
    }
}