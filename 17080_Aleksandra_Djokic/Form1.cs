using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace _17080_Aleksandra_Djokic
{
    public partial class Form1 : Form
    {
 
        private string path;
        public Bitmap slika;
        private int pointerU = -1;
        private int pointerR = -1;
        public Bitmap compBitmap;
        public bool chart = false;
        private List<Bitmap> Undo = new List<Bitmap>();
        private List<Bitmap> Redo = new List<Bitmap>();
        private int topU = -1;
        private int topR = -1;
        private bool win32 = false;
        private int BufferSize;
        private int KernelSize;
        public Form1()
        {
            InitializeComponent();

            this.BufferSize = 10;
               
           // this.Undo = new Buffer<Bitmap>(this.BufferSize);
           // this.Redo = new Buffer<Bitmap>(this.BufferSize);
            this.KernelSize = 3;
        }
        public void ClearBuffers()
        {
            this.Redo.Clear();
            this.pointerR = -1;
            this.Undo.Clear();
            this.pointerU = -1;
        }
        private void isBufferFull(Bitmap m)
        {
            

            if (this.BufferSize == this.topU)
            {
                this.topU--;
                for (int i = 0; i <= this.topU; i++)
                {
                    this.Undo[i ] = (Bitmap)this.Undo[i+1].Clone(); //pomeranje
                }
                this.topU --;
                ClearBuffer(BufferSize);
                
            }
            if (this.BufferSize == this.topR)
            {
                this.topR--;
                for (int i = 0; i <= this.topR; i++)
                {
                    this.Redo[i] = (Bitmap)this.Redo[i + 1].Clone(); //pomeranje
                }
                this.topR--;
                ClearBuffer(BufferSize);

            }
        }
        private void ClearBuffer(int counter)
        {
            for (int i = counter; i <= this.topU; i++)
            {
                this.Undo[counter] = null;
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|PNG files(*.png)|*.png|All valid files|*.bmp/*.jpg/*.png";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            var filePath = string.Empty;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                
                slika = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false); //this.Buffer(this.slika);
                //this.Buffer(this.slika);
                filePath = openFileDialog.FileName;
                this.path = filePath;
                this.pictureBox1.Image = new Bitmap(new Bitmap(openFileDialog.FileName), 530, 840);
                //this.processing.setImage(new Bitmap(this.pictureBox1.Image));
                this.Invalidate();
                this.button1.Visible = true;
                this.kernelSizeBox.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                
            }
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            Form2 dlg = new Form2();
            dlg.slika = (Bitmap)this.slika.Clone();

            dlg.path = this.path;
            if (DialogResult.OK == dlg.ShowDialog())
            {

            }
            this.ClearBuffers();
        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            GammaInput dlg = new GammaInput();
            dlg.red = dlg.green = dlg.blue = 1;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.slika);
                this.Undo.Add((Bitmap)this.slika.Clone());
                this.topU++;
                //this.Buffer(this.slika);
                if (Filters.Gamma(this.slika, dlg.red, dlg.green, dlg.blue))
                { //this.Invalidate();
                    pictureBox1.Image = this.slika;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
        }

        private void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parametar dlg = new Parametar();
            dlg.nValue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.slika);
                this.Undo.Add((Bitmap)this.slika.Clone());
                this.topU++;
                if (Filters.EdgeEnhance(this.slika, (byte)dlg.nValue))
                {
                    pictureBox1.Image = this.slika;
                    this.Redo.Clear();
                    this.topR = -1;
                }
              //  this.Buffer(this.slika);
            }
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int k = this.KernelSize;
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }

            Parametar dlg = new Parametar();
            dlg.nValue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {

                isBufferFull(this.slika);
                this.Undo.Add((Bitmap)this.slika.Clone());
                this.topU++;
                pictureBox1.Image = Filters.Sharpen(this.slika, dlg.nValue,k);
                this.Redo.Clear();
                this.topR = -1;
                // this.Buffer(this.slika);

            }
        }

        private void pixelateToolStripMenuItem_Click(object sender, EventArgs e)
        {
           isBufferFull(this.slika);
            this.Undo.Add((Bitmap)this.slika.Clone());
            this.topU++;
            if (Filters.Pixelate(this.slika, 15, false))
            {
                pictureBox1.Image = this.slika;
                this.Redo.Clear();
                this.topR = -1;
            }
           
           
            // this.Buffer(this.slika);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            

            Form2 dlg = new Form2();
            dlg.slika = (Bitmap)this.slika.Clone();
            dlg.path = this.path;
            dlg.grayscale = true;
            dlg.ShowDialog();
            this.slika = (Bitmap)dlg.slika.Clone();
            dlg.path = this.path;
            this.pictureBox1.Image = this.slika;
            this.ClearBuffers();
        }

        private void simpleColorizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            isBufferFull(this.slika);
            this.Undo.Add((Bitmap)this.slika.Clone());
            this.topU++;

            this.Redo.Clear();
            this.topR = -1;
        }

        private void crossDomainColorizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            CrossDomainColorize dlg = new CrossDomainColorize();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.slika);
                this.Undo.Add((Bitmap)this.slika.Clone());
                this.topU++;

               
                if (Filters.CrossDomainColorize(this.slika, dlg.Hue, dlg.Saturation))
                {
                    pictureBox1.Image = this.slika;
                     this.Redo.Clear();
                this.topR = -1;
                }
              // this.Buffer(this.slika);
            }
        }

        private void orderedDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            bool fastMode = true;
            isBufferFull(this.slika);
            this.Undo.Add((Bitmap)this.slika.Clone());
            this.topU++;

            Filters.ODithering(this.slika);
            //Dither(new OrderedDithering(TrueColorToWebSafeColor, fastMode), this.slika);

            pictureBox1.Image = this.slika;this.Redo.Clear();
            this.topR = -1;
        }
        public void Dither(DitheringBase method, Bitmap input)
        {
            Bitmap dithered = method.DoDithering(input);
            this.slika = (Bitmap)dithered.Clone();
        }
        public static Color TrueColorToWebSafeColor(Color inputColor)
        {
            Color returnColor = Color.FromArgb((byte)Math.Round(inputColor.R / 51.0) * 51,
                                                (byte)Math.Round(inputColor.G / 51.0) * 51,
                                                (byte)Math.Round(inputColor.B / 51.0) * 51);
            return returnColor;
        }
        private void jerviceJudiceAndNinkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;

            }
            isBufferFull(this.slika);
            this.Undo.Add((Bitmap)this.slika.Clone());
            this.topU++;

            JarvisJudiceNinkeDithering dlg = new JarvisJudiceNinkeDithering();
            Size size = this.slika.Size;
            Color[] pixelData = Filters.GetPixels(this.slika);

            this.slika = Filters.ProcessPixels(pixelData, size, dlg, true);
            pictureBox1.Image = this.slika;
            this.Redo.Clear();
            this.topR = -1;
        }

        private void saveWithCompressonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Visible = true ;
            NewFormat cf = new NewFormat();
            this.compBitmap = cf.saveImageToCustomFormat(this.slika);
            this.Invalidate();
             this.pictureBox2.Image =(Image)this.compBitmap;
                
            

        }
        public void ChartToRGB()
        {
            this.chartRGB.Visible = false;
            this.button1.Text = "View Chart";
            this.pictureBox2.Visible = false;
            this.chart = false;

        }
        public void RGBToChart()
        {
            this.chartRGB.Visible = true;
            this.button1.Text = "Hide Chart";
            this.pictureBox2.Visible = false;
            this.Chart();
            this.chart = true;
        }
        public void Chart()
        {
            this.chartRGB.Series["Red"].Points.Clear();
            this.chartRGB.Series["Green"].Points.Clear();
            this.chartRGB.Series["Blue"].Points.Clear();

            List<int> red = new List<int>(255), green = new List<int>(255), blue = new List<int>(255);
            
            for (int i = 0; i < 256; i++)
            {
                red.Add(0);
                green.Add(0);
                blue.Add(0);
            }
            for (int i = 0; i < this.slika.Height; i++)
            {
                for (int j = 0; j < this.slika.Width; j++)
                {
                    Color pixel = this.slika.GetPixel(j, i);
                    red[pixel.R]++;
                    green[pixel.G]++;
                    blue[pixel.B]++;


                }
            }
            for (int i = 0; i < 256; i++)
            {
                chartRGB.Series["Red"].Points.AddY(red[i]);
                chartRGB.Series["Green"].Points.AddY(green[i]);
                chartRGB.Series["Blue"].Points.AddY(blue[i]);
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            if (this.chart == true)
            {
               
                this.ChartToRGB();
            }
            else
            {
                 this.RGBToChart();
            }

        }

        private void loadCompressedPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Image=null;
            NewFormat cf = new NewFormat();
            this.slika = cf.loadImageToCustomFormat();
            this.Invalidate();
            this.pictureBox1.Image = this.slika;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.kernelSizeBox.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.button1.Visible = false;
            this.chartRGB.Visible = false;
           // this.pictureBox2.Visible = false;
        }

        private void simpleColorizeWithPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap pom = new Bitmap(this.path), convert;
            
            var fileContent = string.Empty;
            var filePath = string.Empty;
            isBufferFull(this.slika);
            this.Undo.Add((Bitmap)this.slika.Clone());
            this.topU++;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Title = "Open image";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    pom = (Bitmap)Bitmap.FromFile(filePath);
                    this.Redo.Clear();
                    this.topR = -1;

                }
            }
            convert = (Bitmap)pom.Clone();
            
                Filters.SimpleColorizeWithPicture(this.slika, pom, convert);
            
            this.pictureBox1.Image = this.slika;

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topU >= 0)
            {
                this.Redo.Add(this.slika);
                this.topR++;
                //isBufferFull(this.slika);

               
               
                this.slika = (Bitmap)this.Undo[this.topU].Clone();
                this.Undo.RemoveAt(this.topU);
                this.topU--;
            }
            this.pictureBox1.Image = this.slika;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topR >= 0)
            {
                this.Undo.Add(this.slika);
                this.topU++;
                this.slika = (Bitmap)this.Redo[this.topR].Clone();
                this.Undo.RemoveAt(this.topR);
                this.topR--;
            }
            this.pictureBox1.Image = this.slika;
        }
        


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG";
            ImageFormat format = ImageFormat.Png;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(dlg.FileName);
                switch (ext)
                {
                    case ".bmp":
                            format = ImageFormat.Bmp;
                        break;
                    case ".jpg":
                         format = ImageFormat.Jpeg;
                        break;
                }
                this.slika.Save(dlg.FileName, format); 
                //this.LoadBuffer(this.BufferSize);
            }

        }

        private void downsamplingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            Form2 dlg = new Form2();
            dlg.slika = (Bitmap)this.slika.Clone();
            dlg.downs = true;
            dlg.path = this.path;
            dlg.ShowDialog();
            
            
            
            this.ClearBuffers();
            /* MemoryStream stream = new MemoryStream();
             Bitmap pom =this.slika;
             pom.Save(stream, ImageFormat.Bmp);
             byte[] bytes = stream.ToArray();

             int header = bytes[10] + 256 * (bytes[11] + 256 * (bytes[12] + 256 * bytes[13]));

             byte[] outbyte = new byte[bytes.Length];
             // cuvam header
             for (int i = 0; i < header; i++)
             {
                 outbyte[i] = bytes[i];
             }

             // format: BGRA, y=255-b, m=255-g,  c=255-r
             for (int i = header; i < bytes.Length; i += 4)
             {
                 outbyte[i] = (byte)(255 - bytes[i]);
                 outbyte[i + 1] = (byte)(255 - bytes[i + 1]);
                 outbyte[i + 2] = (byte)(255 - bytes[i + 2]);
                 outbyte[i + 3] = bytes[i + 3];
             }

             // 8*8*4 = 256
             byte[] pomocni = new byte[256];
             for (int i = header; i < outbyte.Length; i += 256)
             {
                 Array.Copy(outbyte, i, pomocni, 0, 256);
                 byte[] outpom = Processing8x8Downsampling(pomocni);
                 Array.Copy(outpom, 0, outbyte, i, 256);
             }

             var bmp = new Bitmap(new MemoryStream(outbyte));
             this.pictureBox2.Image = bmp;*/
        }
        public static byte[] Processing8x8Downsampling(byte[] inbytes)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            // M i A kanal ostaju isti
            /*for (int i = 0; i < 256; i += 4)
            {
                outbyte[i ] = inbytes[i + 0];
                outbyte[i +2] = inbytes[i + 2];
            }*/

            // downsampling za C i Y kanal
            for (int i = 0; i < 256; i += 4)
            {
               // if (i % 4 == 0 && i != 0)
                //    i += 4;
                outbyte[i] = inbytes[i];
                i += 4;
               // outbyte[i + 2] = inbytes[i + 2];
            }
            /* for (int i = 0; i < 56; i += 2)
            {
                if (i % 4 == 0 && i != 0)
                    i += 4;
                outbyte[i] = inbytes[i];
                outbyte[i + 2] = inbytes[i + 2];
            }

            for (int i = 64; i < 128; i += 2)
            {
                if (i % 8 == 0)
                    i += 8;

                outbyte[i] = inbytes[i];
                outbyte[i + 2] = inbytes[i + 2];
            }

            for (int i = 128; i < 184; i += 2)
            {
                if (i % 8 == 0 && i != 128)
                    i += 8;
                outbyte[i] = inbytes[i];
                outbyte[i + 2] = inbytes[i + 2];
            }

            for (int i = 192; i < 254; i += 4)
            {
                if (i % 8 == 0)
                    i += 8;

                outbyte[i] = inbytes[i];
                outbyte[i + 2] = inbytes[i + 2];
            }*/
            return outbyte;
        }
        
        private void kuwahharaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            KuwaharaBlur dlg = new KuwaharaBlur();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.slika);
                this.Undo.Add((Bitmap)this.slika.Clone());
                this.topU++; 
                // this.Buffer(this.slika);
                Bitmap pom = Filters.KuwaharaBlur(this.slika, dlg.blur);
                //this.Invalidate();
                this.slika = pom;
                pictureBox1.Image = this.slika;
                this.Redo.Clear();
                this.topR = -1;
            }
        }

        private void kernelSizeBox_ValueChanged(object sender, EventArgs e)
        {
            if(this.kernelSizeBox.Value==3 || this.kernelSizeBox.Value==5 || this.kernelSizeBox.Value==7)
            {
                 this.KernelSize = Convert.ToInt32(this.kernelSizeBox.Value);
            }
        }

        private void bufferSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferSize dlg = new BufferSize();
            dlg.buffSize = this.BufferSize;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.BufferSize = dlg.buffSize;
            }
        }
    }
}
