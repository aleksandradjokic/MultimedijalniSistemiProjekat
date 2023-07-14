using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace _17080_Aleksandra_Djokic
{
    public partial class Form2 : Form
    {
        public Bitmap slika;
        public string path;
        public bool grayscale = false;
        public bool rgb = true;
        public bool matrix = false;
        private List<Bitmap> Undo = new List<Bitmap>();
        private List<Bitmap> Redo = new List<Bitmap>();
        private int topU = -1;
        private int topR = -1;
        private int BufferSize;
        public Bitmap crvena;
        public Bitmap zelena;
        public Bitmap plava;
        public bool downs = false;
        public Form2()
        {
            InitializeComponent();
            this.BufferSize = 10;
        }
        public void ClearBuffers()
        {
            this.Redo.Clear();
          
            this.Undo.Clear();
           
        }
        private void isBufferFull(Bitmap m)
        {

            if (this.BufferSize == this.topU)
            {
                this.topU--;
                for (int i = 0; i <= this.topU; i++)
                {
                    this.Undo[i] = (Bitmap)this.Undo[i + 1].Clone(); //pomeranje
                }
                this.topU--;
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
                slika = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                filePath = openFileDialog.FileName;
                this.path = filePath;
                this.pictureBox1.Image = new Bitmap(new Bitmap(openFileDialog.FileName), 530, 840);
                //this.processing.setImage(new Bitmap(this.pictureBox1.Image));
                this.Invalidate();
                //this.sviHistogrami();
            }
            this.Kanalske();
        }
        public void Kanalske()
        {
            Bitmap B = (Bitmap)this.slika.Clone(), G = (Bitmap)this.slika.Clone(), R = (Bitmap)this.slika.Clone();
            Color c, d;

            for (int y = 0; y < this.slika.Height; y++)
            {
                for (int x = 0; x < this.slika.Width; x++)
                {
                    c = this.slika.GetPixel(x, y);

                    d = Color.FromArgb(0, 0, c.B);
                    B.SetPixel(x, y, d);
                    d = Color.FromArgb(0, c.G, 0);
                    G.SetPixel(x, y, d);
                    d = Color.FromArgb(c.R, 0, 0);
                    R.SetPixel(x, y, d);

                }
            }
            if (B != null && G != null && R != null)
            {
                this.pictureBox2.Image = B;
                this.pictureBox3.Image = G;
                this.pictureBox4.Image = R;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.chartB.Visible = false;
            this.chartG.Visible = false;
            this.chartR.Visible = false;
            this.chartRGB.Visible = false;
            if (this.slika != null)
            {
                this.pictureBox1.Image = this.slika;

                if (grayscale == false)
                {
                    this.Kanalske();

                }
                else
                {
                    this.GS();
                    this.grayscale = false;
                }
            }
            if(this.downs==true)
            {
                this.Downsampling();
                saveDownsampledPictureToolStripMenuItem.Visible = true;
                this.downs = false;
            }
            else
            {
                saveDownsampledPictureToolStripMenuItem.Visible = false;
            }
            this.crvena = (Bitmap)this.pictureBox4.Image;
            this.zelena = (Bitmap)this.pictureBox3.Image;
            this.plava = (Bitmap)this.pictureBox2.Image;
           
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
                if (Filters.Gamma(this.slika, dlg.red, dlg.green, dlg.blue))
                {    //this.Invalidate();
                    pictureBox1.Image = this.slika;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
            this.Kanalske();
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
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
                pictureBox1.Image = Filters.Sharpen(this.slika, dlg.nValue, 3);
                this.Redo.Clear();
                this.topR = -1;
            }
            this.Kanalske();
        }
        public void Downsampling()
        {
           
            Bitmap pom1=this.Transform((Bitmap)this.pictureBox4.Image,1); 
            this.pictureBox4.Image = (Image)pom1;
            Bitmap pom2 = this.Transform((Bitmap)this.pictureBox3.Image,2);
            this.pictureBox3.Image = (Image)pom2;
            Bitmap pom3 = this.Transform((Bitmap)this.pictureBox2.Image,3);
            this.pictureBox2.Image = (Image)pom3;

        }
        public Bitmap Transform(Bitmap bitmap,int br)
        {
            MemoryStream stream = new MemoryStream();
            Bitmap pom = this.slika;
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
                byte[] outpom = Processing8x8Downsampling(pomocni,br);
                Array.Copy(outpom, 0, outbyte, i, 256);
            }
            
            var bmp = new Bitmap(new MemoryStream(outbyte));
            return bmp;
        }
        public  byte[] Processing8x8Downsampling(byte[] inbytes, int br)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            // M i A kanal ostaju isti
            switch (br)
            {
                case 1:
                    for (int i = 0; i < 256; i += 4)
                    {
                        outbyte[i + 2] = inbytes[i + 2];
                        outbyte[i + 3] = inbytes[i + 3];
                        // outbyte[i +2] = inbytes[i + 2];
                        // outbyte[i + 1] = inbytes[i + 2];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 256; i += 4)
                    {
                        outbyte[i + 3] = inbytes[i + 3];
                        outbyte[i + 1] = inbytes[i + 1];
                        // outbyte[i +2] = inbytes[i + 2];
                        // outbyte[i + 1] = inbytes[i + 2];
                    }
                    break;
                default:
                    for (int i = 0; i < 256; i += 4)
                    {
                        outbyte[i + 2] = inbytes[i + 2];
                        outbyte[i + 1] = inbytes[i + 1];
                        // outbyte[i +2] = inbytes[i + 2];
                        // outbyte[i + 1] = inbytes[i + 2];
                    }
                    break;
            }
            for (int i = 0; i < 256; i += 4)
            {
                outbyte[i+2 ] = inbytes[i+2 ];
                outbyte[i+1] = inbytes[i+1];
                // outbyte[i +2] = inbytes[i + 2];
                // outbyte[i + 1] = inbytes[i + 2];
            }
            
            // downsampling za C i Y kanal
            for (int i = 0; i < 256; i += 4)
            {
                // if (i % 4 == 0 && i != 0)
                //    i += 4;
                outbyte[i] = inbytes[i];
                outbyte[i+br] = inbytes[i+br];
                i += 4;
                
            }
           
            return outbyte;
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
            }
            this.Kanalske();
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
            this.Kanalske();
        }
        private void GS()
        {
            Bitmap gs1 = new Bitmap((Bitmap)this.slika.Clone());
            Bitmap gs2 = new Bitmap((Bitmap)this.slika.Clone());
            Bitmap gs3 = new Bitmap((Bitmap)this.slika.Clone());
            Filters.GrayScaleAritm(gs1);
            Filters.GrayScaleMax(gs2);
            GrayScaleInput dlg1 = new GrayScaleInput();
            if (DialogResult.OK == dlg1.ShowDialog())
            {
                Filters.GrayScaleCrCgCb(gs3, dlg1.cr, dlg1.cg, dlg1.cb);
            }
            this.pictureBox2.Image = gs1;

            this.pictureBox3.Image = gs2;

            this.pictureBox4.Image = gs3;
        }
        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }

            this.GS();

        }

        private void crossDomainColorizeToolStripMenuItem1_Click(object sender, EventArgs e)
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
            }
            this.Kanalske();
        }

        public void ChartsHist(Bitmap img, Bitmap r, Bitmap g, Bitmap b)
        {
            this.chartRGB.Series["Red"].Points.Clear();
            this.chartRGB.Series["Green"].Points.Clear();
            this.chartRGB.Series["Blue"].Points.Clear();
            this.chartR.Series["Red"].Points.Clear();
            this.chartG.Series["Green"].Points.Clear();
            this.chartB.Series["Blue"].Points.Clear();

            List<int> red = new List<int>(256), green = new List<int>(256), blue = new List<int>(256);
            List<int> red1 = new List<int>(256), green1 = new List<int>(256), blue1 = new List<int>(256);

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
                    Color pixel = img.GetPixel(j, i);
                    red[pixel.R]++;
                    green[pixel.G]++;
                    blue[pixel.B]++;

                    /* Bitmap r1 = (Bitmap)this.pictureBox4.Image;
                     Color pixelR = r1.GetPixel(j, i);
                     red1[pixelR.R]++;
                     Bitmap g1 = (Bitmap)this.pictureBox3.Image;
                      Color pixelG = g1.GetPixel(j, i);
                     green1[pixelG.G]++;
                     Bitmap b1 = (Bitmap)this.pictureBox2.Image;
                     Color pixelB = b1.GetPixel(j, i);
                     blue1[pixelB.B]++;*/

                }
            }
            Bitmap p = (Bitmap)this.pictureBox4.Image;
            for (int i = 0; i < 256; i++)
            {
                red1.Add(0);
                green1.Add(0);
                blue1.Add(0);
            }
            for (int i = 0; i < p.Height; i++)
            {
                for (int j = 0; j < p.Width; j++)
                {


                    Bitmap r1 = r;
                    Color pixelR = r1.GetPixel(j, i);
                    red1[pixelR.R]++;
                    Bitmap g1 = g;
                    Color pixelG = g1.GetPixel(j, i);
                    green1[pixelG.G]++;
                    Bitmap b1 = b;
                    Color pixelB = b1.GetPixel(j, i);
                    blue1[pixelB.B]++;

                }
            }
            for (int i = 0; i < 256; i++)
            {
                chartRGB.Series["Red"].Points.AddY(red[i]);
                chartRGB.Series["Green"].Points.AddY(green[i]);
                chartRGB.Series["Blue"].Points.AddY(blue[i]);
                chartR.Series["Red"].Points.AddY(red1[i]);
                chartG.Series["Green"].Points.AddY(green1[i]);
                chartB.Series["Blue"].Points.AddY(blue1[i]);
            }
        }
        public void ChartToRGB()
        {
            this.chartRGB.Visible = false;
            this.chartR.Visible = false;
            this.chartG.Visible = false;
            this.chartB.Visible = false;
            this.pictureBox1.Visible = true;
            this.pictureBox2.Visible = true;
            this.pictureBox3.Visible = true;
            this.pictureBox4.Visible = true;
            this.rgb = true;
            this.Kanalske();
        }
        public void RGBToChartHist(Bitmap img, Bitmap r, Bitmap g, Bitmap b)
        {
            this.chartRGB.Visible = true;
            this.chartR.Visible = true;
            this.chartG.Visible = true;
            this.chartB.Visible = true;
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Visible = false;
            this.pictureBox4.Visible = false;
            this.rgb = false;
            this.ChartsHist(img, r, g, b);
        }
        public void RGBToChart()
        {
            this.chartRGB.Visible = true;
            this.chartR.Visible = true;
            this.chartG.Visible = true;
            this.chartB.Visible = true;
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Visible = false;
            this.pictureBox4.Visible = false;
            this.rgb = false;
            this.Charts();
        }
        public void Charts()
        {
            this.chartRGB.Series["Red"].Points.Clear();
            this.chartRGB.Series["Green"].Points.Clear();
            this.chartRGB.Series["Blue"].Points.Clear();
            this.chartR.Series["Red"].Points.Clear();
            this.chartG.Series["Green"].Points.Clear();
            this.chartB.Series["Blue"].Points.Clear();

            List<int> red = new List<int>(256), green = new List<int>(256), blue = new List<int>(256);
            List<int> red1 = new List<int>(256), green1 = new List<int>(256), blue1 = new List<int>(256);

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

                    /* Bitmap r1 = (Bitmap)this.pictureBox4.Image;
                     Color pixelR = r1.GetPixel(j, i);
                     red1[pixelR.R]++;
                     Bitmap g1 = (Bitmap)this.pictureBox3.Image;
                      Color pixelG = g1.GetPixel(j, i);
                     green1[pixelG.G]++;
                     Bitmap b1 = (Bitmap)this.pictureBox2.Image;
                     Color pixelB = b1.GetPixel(j, i);
                     blue1[pixelB.B]++;*/

                }
            }
            Bitmap p = (Bitmap)this.pictureBox4.Image;
            for (int i = 0; i < 256; i++)
            {
                red1.Add(0);
                green1.Add(0);
                blue1.Add(0);
            }
            for (int i = 0; i < p.Height; i++)
            {
                for (int j = 0; j < p.Width; j++)
                {


                    Bitmap r1 = (Bitmap)this.pictureBox4.Image;
                    Color pixelR = r1.GetPixel(j, i);
                    red1[pixelR.R]++;
                    Bitmap g1 = (Bitmap)this.pictureBox3.Image; ;
                    Color pixelG = g1.GetPixel(j, i);
                    green1[pixelG.G]++;
                    Bitmap b1 = (Bitmap)this.pictureBox2.Image;
                    Color pixelB = b1.GetPixel(j, i);
                    blue1[pixelB.B]++;

                }
            }
            for (int i = 0; i < 256; i++)
            {
                chartRGB.Series["Red"].Points.AddY(red[i]);
                chartRGB.Series["Green"].Points.AddY(green[i]);
                chartRGB.Series["Blue"].Points.AddY(blue[i]);
                chartR.Series["Red"].Points.AddY(red1[i]);
                chartG.Series["Green"].Points.AddY(green1[i]);
                chartB.Series["Blue"].Points.AddY(blue1[i]);
            }
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
               
            }
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            if (this.rgb == true)
            {
                this.RGBToChart();

            }
            else
            {
                this.ChartToRGB();
            }



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
            this.Kanalske();
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

            pictureBox1.Image = this.slika;
            pictureBox1.Image = this.slika; this.Redo.Clear();
            this.topR = -1;
            this.Kanalske();
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

        private void histogramWithInputChangeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            /*if(this.rgb==true)
            {
                this.RGBToChart();

            }
            else
            {
                this.ChartToRGB();
            }*/
            /* if(this.rgb==true)
             {
                 // this.RGBToChart();
                 this.chartRGB.Visible = true;
                 this.chartR.Visible = true;
                 this.chartG.Visible = true;
                 this.chartB.Visible = true;
             }*/
            Histogram dlg = new Histogram();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                Bitmap img = this.slika;
                Bitmap r = (Bitmap)this.pictureBox4.Image;
                Bitmap g = (Bitmap)this.pictureBox3.Image;
                Bitmap b = (Bitmap)this.pictureBox2.Image;
                if (Filters.HistogramZ(dlg.min, dlg.max, r, g, b, img))
                {
                    if (this.rgb == true)
                    {
                        this.RGBToChartHist(img, r, g, b);

                    }
                    else
                    {
                        this.ChartToRGB();
                    }


                }
                // this.Charts();
            }
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
            this.Kanalske();

        }

        private void kuwaharaToolStripMenuItem_Click(object sender, EventArgs e)
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
                Bitmap pom = Filters.KuwaharaBlur(this.slika, dlg.blur);
                //this.Invalidate();
                this.slika = pom;
                pictureBox1.Image = this.slika;
                this.Redo.Clear();
                this.topR = -1;
            }
            this.Kanalske();
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
            }
            this.Kanalske();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Downsampling();
        }

        private void downsampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Downsampling();
            saveDownsampledPictureToolStripMenuItem.Visible = true;
        }

        private void saveDownsampledPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nista izabrali sliku");
                return;
            }
            Picture dlg = new Picture();
            Bitmap p=this.slika;
            if (DialogResult.OK == dlg.ShowDialog())
            {
               
                switch (dlg.br)
                {
                    case 1:
                        {
                           // this.pictureBox5.Image = this.pictureBox4.Image;
                            p= (Bitmap)this.pictureBox4.Image;
                        }
                        break;
                    case 2:
                        {
                           // this.pictureBox5.Image = this.pictureBox3.Image;
                            p = (Bitmap)this.pictureBox3.Image;
                        }
                        break;
                    default:
                        {
                           // this.pictureBox5.Image = this.pictureBox2.Image;
                            p = (Bitmap)this.pictureBox2.Image;
                        }
                        break;
                }
                
            }
            this.pictureBox2.Visible = true ;
            NewFormat cf = new NewFormat();
            Bitmap p1 = cf.saveImageToCustomFormat(p);
            this.Invalidate();
            // this.pictureBox5.Image =(Image)p1;
        }
       
        private void chartR_Click(object sender, EventArgs e)
        {

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
            this.Kanalske();
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
            this.Kanalske();
        }
    }
}

