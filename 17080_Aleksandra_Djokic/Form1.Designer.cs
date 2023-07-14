
namespace _17080_Aleksandra_Djokic
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeEnhanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kuwahharaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossDomainColorizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleColorizeWithPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ditheringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderedDitheringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jerviceJudiceAndNinkeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customFormatAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWithCompressonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCompressedPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downsamplingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chartRGB = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.kernelSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRGB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kernelSizeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.moreFiltersToolStripMenuItem,
            this.colorizeToolStripMenuItem,
            this.ditheringToolStripMenuItem,
            this.customFormatAndToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1336, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.bufferSizeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // bufferSizeToolStripMenuItem
            // 
            this.bufferSizeToolStripMenuItem.Name = "bufferSizeToolStripMenuItem";
            this.bufferSizeToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.bufferSizeToolStripMenuItem.Text = "BufferSize";
            this.bufferSizeToolStripMenuItem.Click += new System.EventHandler(this.bufferSizeToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBToolStripMenuItem,
            this.gammaToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.edgeEnhanceToolStripMenuItem,
            this.pixelateToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // rGBToolStripMenuItem
            // 
            this.rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            this.rGBToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.rGBToolStripMenuItem.Text = "RGB";
            this.rGBToolStripMenuItem.Click += new System.EventHandler(this.rGBToolStripMenuItem_Click);
            // 
            // gammaToolStripMenuItem
            // 
            this.gammaToolStripMenuItem.Name = "gammaToolStripMenuItem";
            this.gammaToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.gammaToolStripMenuItem.Text = "Gamma";
            this.gammaToolStripMenuItem.Click += new System.EventHandler(this.gammaToolStripMenuItem_Click);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.sharpenToolStripMenuItem.Text = "Sharpen";
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // edgeEnhanceToolStripMenuItem
            // 
            this.edgeEnhanceToolStripMenuItem.Name = "edgeEnhanceToolStripMenuItem";
            this.edgeEnhanceToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.edgeEnhanceToolStripMenuItem.Text = "Edge Enhance";
            this.edgeEnhanceToolStripMenuItem.Click += new System.EventHandler(this.edgeEnhanceToolStripMenuItem_Click);
            // 
            // pixelateToolStripMenuItem
            // 
            this.pixelateToolStripMenuItem.Name = "pixelateToolStripMenuItem";
            this.pixelateToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.pixelateToolStripMenuItem.Text = "Pixelate";
            this.pixelateToolStripMenuItem.Click += new System.EventHandler(this.pixelateToolStripMenuItem_Click);
            // 
            // moreFiltersToolStripMenuItem
            // 
            this.moreFiltersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayScaleToolStripMenuItem,
            this.kuwahharaToolStripMenuItem});
            this.moreFiltersToolStripMenuItem.Name = "moreFiltersToolStripMenuItem";
            this.moreFiltersToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.moreFiltersToolStripMenuItem.Text = "More Filters";
            // 
            // grayScaleToolStripMenuItem
            // 
            this.grayScaleToolStripMenuItem.Name = "grayScaleToolStripMenuItem";
            this.grayScaleToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.grayScaleToolStripMenuItem.Text = "GrayScale";
            this.grayScaleToolStripMenuItem.Click += new System.EventHandler(this.grayScaleToolStripMenuItem_Click);
            // 
            // kuwahharaToolStripMenuItem
            // 
            this.kuwahharaToolStripMenuItem.Name = "kuwahharaToolStripMenuItem";
            this.kuwahharaToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.kuwahharaToolStripMenuItem.Text = "Kuwahara";
            this.kuwahharaToolStripMenuItem.Click += new System.EventHandler(this.kuwahharaToolStripMenuItem_Click);
            // 
            // colorizeToolStripMenuItem
            // 
            this.colorizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crossDomainColorizeToolStripMenuItem,
            this.simpleColorizeWithPictureToolStripMenuItem});
            this.colorizeToolStripMenuItem.Name = "colorizeToolStripMenuItem";
            this.colorizeToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.colorizeToolStripMenuItem.Text = "Colorize";
            // 
            // crossDomainColorizeToolStripMenuItem
            // 
            this.crossDomainColorizeToolStripMenuItem.Name = "crossDomainColorizeToolStripMenuItem";
            this.crossDomainColorizeToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.crossDomainColorizeToolStripMenuItem.Text = "Cross-DomainColorize";
            this.crossDomainColorizeToolStripMenuItem.Click += new System.EventHandler(this.crossDomainColorizeToolStripMenuItem_Click);
            // 
            // simpleColorizeWithPictureToolStripMenuItem
            // 
            this.simpleColorizeWithPictureToolStripMenuItem.Name = "simpleColorizeWithPictureToolStripMenuItem";
            this.simpleColorizeWithPictureToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.simpleColorizeWithPictureToolStripMenuItem.Text = "SimpleColorizeWithPicture";
            this.simpleColorizeWithPictureToolStripMenuItem.Click += new System.EventHandler(this.simpleColorizeWithPictureToolStripMenuItem_Click);
            // 
            // ditheringToolStripMenuItem
            // 
            this.ditheringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderedDitheringToolStripMenuItem,
            this.jerviceJudiceAndNinkeToolStripMenuItem});
            this.ditheringToolStripMenuItem.Name = "ditheringToolStripMenuItem";
            this.ditheringToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.ditheringToolStripMenuItem.Text = "Dithering";
            // 
            // orderedDitheringToolStripMenuItem
            // 
            this.orderedDitheringToolStripMenuItem.Name = "orderedDitheringToolStripMenuItem";
            this.orderedDitheringToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.orderedDitheringToolStripMenuItem.Text = "Ordered dithering";
            this.orderedDitheringToolStripMenuItem.Click += new System.EventHandler(this.orderedDitheringToolStripMenuItem_Click);
            // 
            // jerviceJudiceAndNinkeToolStripMenuItem
            // 
            this.jerviceJudiceAndNinkeToolStripMenuItem.Name = "jerviceJudiceAndNinkeToolStripMenuItem";
            this.jerviceJudiceAndNinkeToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.jerviceJudiceAndNinkeToolStripMenuItem.Text = "Jervice, Judice and Ninke";
            this.jerviceJudiceAndNinkeToolStripMenuItem.Click += new System.EventHandler(this.jerviceJudiceAndNinkeToolStripMenuItem_Click);
            // 
            // customFormatAndToolStripMenuItem
            // 
            this.customFormatAndToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveWithCompressonToolStripMenuItem,
            this.loadCompressedPictureToolStripMenuItem,
            this.downsamplingToolStripMenuItem});
            this.customFormatAndToolStripMenuItem.Name = "customFormatAndToolStripMenuItem";
            this.customFormatAndToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.customFormatAndToolStripMenuItem.Text = "Custom format and Downsampling ";
            // 
            // saveWithCompressonToolStripMenuItem
            // 
            this.saveWithCompressonToolStripMenuItem.Name = "saveWithCompressonToolStripMenuItem";
            this.saveWithCompressonToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.saveWithCompressonToolStripMenuItem.Text = "Save with compresson";
            this.saveWithCompressonToolStripMenuItem.Click += new System.EventHandler(this.saveWithCompressonToolStripMenuItem_Click);
            // 
            // loadCompressedPictureToolStripMenuItem
            // 
            this.loadCompressedPictureToolStripMenuItem.Name = "loadCompressedPictureToolStripMenuItem";
            this.loadCompressedPictureToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.loadCompressedPictureToolStripMenuItem.Text = "Load compressed picture";
            this.loadCompressedPictureToolStripMenuItem.Click += new System.EventHandler(this.loadCompressedPictureToolStripMenuItem_Click);
            // 
            // downsamplingToolStripMenuItem
            // 
            this.downsamplingToolStripMenuItem.Name = "downsamplingToolStripMenuItem";
            this.downsamplingToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.downsamplingToolStripMenuItem.Text = "Downsampling";
            this.downsamplingToolStripMenuItem.Click += new System.EventHandler(this.downsamplingToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(730, 399);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(779, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 100);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show Chart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartRGB
            // 
            chartArea2.Name = "ChartArea1";
            this.chartRGB.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartRGB.Legends.Add(legend2);
            this.chartRGB.Location = new System.Drawing.Point(891, 89);
            this.chartRGB.Name = "chartRGB";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "Red";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.Lime;
            series5.Legend = "Legend1";
            series5.Name = "Green";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Color = System.Drawing.Color.Blue;
            series6.Legend = "Legend1";
            series6.Name = "Blue";
            this.chartRGB.Series.Add(series4);
            this.chartRGB.Series.Add(series5);
            this.chartRGB.Series.Add(series6);
            this.chartRGB.Size = new System.Drawing.Size(419, 349);
            this.chartRGB.TabIndex = 12;
            this.chartRGB.Text = "chartrgb";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(891, 88);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(419, 350);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // kernelSizeBox
            // 
            this.kernelSizeBox.Location = new System.Drawing.Point(903, 42);
            this.kernelSizeBox.Name = "kernelSizeBox";
            this.kernelSizeBox.Size = new System.Drawing.Size(55, 22);
            this.kernelSizeBox.TabIndex = 14;
            this.kernelSizeBox.ValueChanged += new System.EventHandler(this.kernelSizeBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(786, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Kernel size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.label2.Location = new System.Drawing.Point(786, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Convolution matrix NxN (3x3, 5x5, 7x7)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 449);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kernelSizeBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.chartRGB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRGB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kernelSizeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gammaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeEnhanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossDomainColorizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ditheringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderedDitheringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jerviceJudiceAndNinkeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customFormatAndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWithCompressonToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRGB;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem loadCompressedPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleColorizeWithPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bufferSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downsamplingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kuwahharaToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown kernelSizeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

