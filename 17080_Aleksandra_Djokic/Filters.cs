using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace _17080_Aleksandra_Djokic
{
    class Filters
    {
        public struct RGB
        {
            private byte _r;
            private byte _g;
            private byte _b;

            public RGB(byte r, byte g, byte b)
            {
                this._r = r;
                this._g = g;
                this._b = b;
            }

            public byte R
            {
                get { return this._r; }
                set { this._r = value; }
            }

            public byte G
            {
                get { return this._g; }
                set { this._g = value; }
            }

            public byte B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public bool Equals(RGB rgb)
            {
                return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
            }
        }
        public struct HSV
        {
            private double _h;
            private double _s;
            private double _v;

            public HSV(double h, double s, double v)
            {
                this._h = h;
                this._s = s;
                this._v = v;
            }

            public double H
            {
                get { return this._h; }
                set { this._h = value; }
            }

            public double S
            {
                get { return this._s; }
                set { this._s = value; }
            }

            public double V
            {
                get { return this._v; }
                set { this._v = value; }
            }

            public bool Equals(HSV hsv)
            {
                return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
            }
        }
        public static bool Gamma(Bitmap b, double red, double green, double blue)
        {
            if (red < .2 || red > 5) return false;
            if (green < .2 || green > 5) return false;
            if (blue < .2 || blue > 5) return false;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
            }

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        p[2] = redGamma[p[2]];
                        p[1] = greenGamma[p[1]];
                        p[0] = blueGamma[p[0]];

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }
		public static bool EdgeEnhance(Bitmap b, byte nThreshold)
		{
			
			Bitmap b2 = (Bitmap)b.Clone();

			
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				byte* p2 = (byte*)(void*)Scan02;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width * 3;

				int nPixel = 0, nPixelMax = 0;

				p += stride;
				p2 += stride;

				for (int y = 1; y < b.Height - 1; ++y)
				{
					p += 3;
					p2 += 3;

					for (int x = 3; x < nWidth - 3; ++x)
					{
						nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]);

						nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						if (nPixelMax > nThreshold && nPixelMax > p[0])
							p[0] = (byte)Math.Max(p[0], nPixelMax);

						++p;
						++p2;
					}

					p += nOffset + 3;
					p2 += nOffset + 3;
				}
			}

			b.UnlockBits(bmData);
			b2.UnlockBits(bmData2);

			return true;
		}
        public static Bitmap Sharpen(Image image, double strength, int kernelSize)
        {
            Bitmap sharpenImage = (Bitmap)image.Clone();

            int width = image.Width;
            int height = image.Height;
            int offset;
            const int filterWidth = 5;
            const int filterHeight = 5;
            switch (kernelSize)
            {
                case 5:
                    offset = (int)Math.Pow(2, 5);
                    break;
                case 7:
                    offset = (int)Math.Pow(2, 7);
                    break;
                default:
                    offset = 0;
                    break;
            }
            ConvolutionMat kernel = new ConvolutionMat(-1, Convert.ToInt32(strength), offset, kernelSize);
            var filter = new double[,]
                {
                    {-1, -1, -1, -1, -1},
                    {-1,  2,  2,  2, -1},
                    {-1,  2, 16,  2, -1},
                    {-1,  2,  2,  2, -1},
                    {-1, -1, -1, -1, -1}
                };

            double bias = 1.0 - strength;
            double factor = strength / 16.0;

            var result = new Color[image.Width, image.Height];

            if (sharpenImage != null)
            {
                BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                // Declare an array to hold the bytes of the bitmap.
                int bytes = pbits.Stride * height;
                var rgbValues = new byte[bytes];

                Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

                int rgb;
                for (int x = 0; x < width; ++x)
                {
                    for (int y = 0; y < height; ++y)
                    {
                        double red = 0.0, green = 0.0, blue = 0.0;

                        for (int filterX = 0; filterX < filterWidth; filterX++)
                        {
                            for (int filterY = 0; filterY < filterHeight; filterY++)
                            {
                                int imageX = (x - filterWidth / 2 + filterX + width) % width;
                                int imageY = (y - filterHeight / 2 + filterY + height) % height;

                                rgb = imageY * pbits.Stride + 3 * imageX;

                                red += rgbValues[rgb + 2] * filter[filterX, filterY];
                                green += rgbValues[rgb + 1] * filter[filterX, filterY];
                                blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                            }
                            rgb = y * pbits.Stride + 3 * x;

                            int r = Math.Min(Math.Max((int)(factor * red + (bias * rgbValues[rgb + 2])), 0), 255);
                            int g = Math.Min(Math.Max((int)(factor * green + (bias * rgbValues[rgb + 1])), 0), 255);
                            int b = Math.Min(Math.Max((int)(factor * blue + (bias * rgbValues[rgb + 0])), 0), 255);

                            result[x, y] = Color.FromArgb(r, g, b);
                        }
                    }
                }

                for (int x = 0; x < width; ++x)
                {
                    for (int y = 0; y < height; ++y)
                    {
                        rgb = y * pbits.Stride + 3 * x;

                        rgbValues[rgb + 2] = result[x, y].R;
                        rgbValues[rgb + 1] = result[x, y].G;
                        rgbValues[rgb + 0] = result[x, y].B;
                    }
                }

                Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
                sharpenImage.UnlockBits(pbits);
            }
            return sharpenImage;
        }
        public static bool Pixelate(Bitmap b, short pixel, bool bGrid)
        {
            int nWidth = b.Width;
            int nHeight = b.Height;

            Point[,] pt = new Point[nWidth, nHeight];

            int newX, newY;

            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = pixel - x % pixel;

                    if (bGrid && newX == pixel)
                        pt[x, y].X = -x;
                    else if (x + newX > 0 && x + newX < nWidth)
                        pt[x, y].X = newX;
                    else
                        pt[x, y].X = 0;

                    newY = pixel - y % pixel;

                    if (bGrid && newY == pixel)
                        pt[x, y].Y = -y;
                    else if (y + newY > 0 && y + newY < nHeight)
                        pt[x, y].Y = newY;
                    else
                        pt[x, y].Y = 0;
                }

            OffsetFilter(b, pt);

            return true;
        }
        public static bool OffsetFilter(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;
                int nWidth = b.Width;
                int nHeight = b.Height;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = offset[x, y].X;
                        yOffset = offset[x, y].Y;

                        if (y + yOffset >= 0 && y + yOffset < nHeight && x + xOffset >= 0 && x + xOffset < nWidth)
                        {
                            p[0] = pSrc[((y + yOffset) * stride) + ((x + xOffset) * 3)];
                            p[1] = pSrc[((y + yOffset) * stride) + ((x + xOffset) * 3) + 1];
                            p[2] = pSrc[((y + yOffset) * stride) + ((x + xOffset) * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        public static bool GrayScaleAritm(Bitmap img)
        {

            unsafe
            {

                for (int y = 0; y < img.Height; ++y)
                {
                    for (int x = 0; x < img.Width; ++x)
                    {
                        System.Drawing.Color pix = img.GetPixel(y, x);
                        byte b = pix.B;
                        byte g = pix.G;
                        byte r = pix.R;
                        
                        byte output = (byte)((r + g + b) / 3);
                        img.SetPixel(y, x, System.Drawing.Color.FromArgb((byte)output, (byte)output, (byte)output));

                    }
                    
                }
            }
            return true;
        }
        public static bool GrayScaleMax(Bitmap img)
        {

            unsafe
            {

                for (int y = 0; y < img.Height; ++y)
                {
                    for (int x = 0; x < img.Width; ++x)
                    {
                        System.Drawing.Color pix = img.GetPixel(y, x);
                        byte b = pix.B;
                        byte g = pix.G;
                        byte r = pix.R;

                        byte output = (byte)Math.Max(r, Math.Max(g, b));
                        img.SetPixel(y, x, System.Drawing.Color.FromArgb((byte)output, (byte)output, (byte)output));

                    }

                }
            }
            return true;
            
        }
        public static bool GrayScaleCrCgCb(Bitmap img, double cr, double cg, double cb)
        {

                for (int y = 0; y < img.Height; ++y)
                {
                    for (int x = 0; x < img.Width; ++x)
                    {
                        System.Drawing.Color pix = img.GetPixel(y, x);
                        byte b = pix.B;
                        byte g = pix.G;
                        byte r = pix.R;

                        byte output = (byte)(r*0.3 + g*0.59 + b*0.11);
                        img.SetPixel(y, x, System.Drawing.Color.FromArgb((byte)output, (byte)output, (byte)output));

                    }

                }
            
            return true;
        }
        public static void SimpleColorizeWithPicture(Bitmap img, Bitmap pom, Bitmap convert)
        {
            for (int i = 0; i < convert.Width; i++)
            {
                for (int j = 0; j < convert.Height; j++)
                {
                    System.Drawing.Color clr = convert.GetPixel(i, j);
                    byte b = clr.B;
                    byte g = clr.G;
                    byte r = clr.R;
                    byte outp = (byte)((Math.Max(r, Math.Max(g, b)) + Math.Min(r, Math.Min(g, b))) / 2);

                    convert.SetPixel(i, j, System.Drawing.Color.FromArgb((byte)outp, (byte)outp, (byte)outp));
                }
            }
            
            Color[] colormap = new Color[256];
            int R, G, B, gray;
            for (int x = 0; x < pom.Width; x++)
            {
                for (int y = 0; y < pom.Height; y++)
                {
                    R = pom.GetPixel(x, y).R;
                    G = pom.GetPixel(x, y).G;
                    B = pom.GetPixel(x, y).B;
                    gray = convert.GetPixel(x, y).B;
                    colormap[gray] = System.Drawing.Color.FromArgb(R, G, B);
                }
            }
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    gray = img.GetPixel(x, y).B;
                    img.SetPixel(x, y, colormap[gray]);
                }
            }
        }
        public static Bitmap ProcessPixels(Color[] pixelData, Size size, JarvisJudiceNinkeDithering jjn, bool monochrome)
        {
            Color black = Color.FromArgb(0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255);
            int trashold = 127;

            for (int row = 0; row < size.Height; row++)
            {
                for (int col = 0; col < size.Width; col++)
                {
                    int index;
                    Color current;
                    Color transformed;

                    index = row * size.Width + col;
                    current = pixelData[index];

                    if (monochrome)
                    {
                        byte gray;
                        gray = (byte)(0.299 * current.R + 0.587 * current.G + 0.114 * current.B);
                        transformed = gray < trashold ? black : white;
                        pixelData[index] = transformed;
                    }
                    else
                    {
                        transformed = current;
                    }

                    jjn.DoDiffuse(pixelData, current, transformed, col, row, size.Width, size.Height);
                }
            }

            return ToBitmap(pixelData, size);
        }
        public static Bitmap ToBitmap(Color[] data, Size size)
        {
            int height;
            int width;

            Bitmap result;

            result = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
            width = result.Width;
            height = result.Height;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color rgb = data[y * width + x];
                    Color color = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                    result.SetPixel(x, y, color);
                }
            }
            return result;
        }
        public static bool SimpleColorize(Bitmap img)
        {
            int iWidth = img.Width;
            int iHeight = img.Height;
            Color cl;
            int rgb;
            int r, g, b;
            unsafe
            {

            }
                for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    r = img.GetPixel(i, j).R;
                    g = img.GetPixel(i, j).G;
                    b = img.GetPixel(i, j).B;

                    int gray = img.GetPixel(i, j).R;
                    //img.SetPixel(i, j, Colors(gray));
                }
            }
            return true;
        }
        
        public static RGB HSVToRGB(HSV hsv)
        {
            double r = 0, g = 0, b = 0;

            if (hsv.S == 0)
            {
                r = hsv.V;
                g = hsv.V;
                b = hsv.V;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (hsv.H == 360)
                    hsv.H = 0;
                else
                    hsv.H = hsv.H / 60;

                i = (int)Math.Truncate(hsv.H);
                f = hsv.H - i;

                p = hsv.V * (1.0 - hsv.S);
                q = hsv.V * (1.0 - (hsv.S * f));
                t = hsv.V * (1.0 - (hsv.S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        r = hsv.V;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = hsv.V;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = hsv.V;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = hsv.V;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = hsv.V;
                        break;

                    default:
                        r = hsv.V;
                        g = p;
                        b = q;
                        break;
                }

            }

            return new RGB((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }
        public static HSV RGBToHSV(RGB rgb)
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(rgb.R, rgb.G), rgb.B);
            v = Math.Max(Math.Max(rgb.R, rgb.G), rgb.B);
            delta = v - min;

            if (v == 0.0)
                s = 0;
            else
                s = delta / v;

            if (s == 0)
                h = 0.0;

            else
            {
                if (rgb.R == v)
                    h = (rgb.G - rgb.B) / delta;
                else if (rgb.G == v)
                    h = 2 + (rgb.B - rgb.R) / delta;
                else if (rgb.B == v)
                    h = 4 + (rgb.R - rgb.G) / delta;

                h *= 60;

                if (h < 0.0)
                    h = h + 360;
            }

            return new HSV(h, s, (v / 255));
        }

        public static bool CrossDomainColorize(Bitmap img, double h, double S)
        {
            int newH = (int)(h + 1) * 360 / 6;
            int newS;
            if (S != 101)
            {
                newS = (int)(S * 100);
            }
            else
            {
                newS = (int)S;
            }
            int iWidth = img.Width;
            int iHeight = img.Height;
            unsafe
            {
                int R, G, B;
                for (int x = 0; x < iWidth; x++)
                {
                    for (int y = 0; y < iHeight; y++)
                    {
                        R = img.GetPixel(x, y).R;
                        G = img.GetPixel(x, y).G;
                        B = img.GetPixel(x, y).B;

                        HSV valueHSV = RGBToHSV(new RGB((byte)R, (byte)G, (byte)B));

                        RGB valueRGB = new RGB();

                        if (S == 101)
                        {
                            valueRGB = HSVToRGB(new HSV(newH, valueHSV.S, valueHSV.V));
                        }
                        else if (S != 101)
                        {
                            valueRGB = HSVToRGB(new HSV(newH, newS, valueHSV.V));
                        }
                        img.SetPixel(x, y, Color.FromArgb(valueRGB.R, valueRGB.G, valueRGB.B));
                    }
                }
            }
            return true;
        }
        public static Color[] GetPixels(Bitmap b)
        {
            int width = b.Width;
            int height = b.Height; ;

            Color[] results = new Color[width * height];

            Bitmap bSrc = (Bitmap)b.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        results[y * width + x] = Color.FromArgb(p[0], p[1], p[2]);
                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return results;
        }
        public static bool HistogramZ( int min, int max,Bitmap R,Bitmap G,Bitmap B,Bitmap img)
        {
            int mostMinC = 0, mostMaxC = 0, mostMinM = 0, mostMaxM = 0, mostMinY = 0, mostMaxY = 0;
            int indexMinC = 0, indexMaxC = 0, indexMinM = 0, indexMaxM = 0, indexMinY = 0, indexMaxY = 0;
            List<int> red = new List<int>(255), green = new List<int>(255), blue = new List<int>(255);
            for (int i = 0; i < 256; i++)
            {
                red.Add(0);
                green.Add(0);
                blue.Add(0);
            }
            //posto su iste sirine i visine moze kroz jednu
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color pixel = img.GetPixel(j, i);
                    if (pixel.R >= min && pixel.R <= max)
                        red[pixel.R]++;
                    if (pixel.G >= min && pixel.G <= max)
                        green[pixel.G]++;
                    if (pixel.B >= min && pixel.B <= max)
                        blue[pixel.B]++;
                }
            }
            
           
           /* for (int i = 0; i < 256; i++)
            {
                
                    if (red[i] <min)
                    {
                        red[i] = min;
                    }
                    if (green[i] < min)
                    {
                        green[i] = min;
                    }
                    if (blue[i] < min)
                    {
                        blue[i] = min;
                    }
                if (red[i] > max)
                {
                    red[i] = max;
                }
                if (green[i] > max)
                {
                    green[i] = max;
                }
                if (blue[i] > max)
                {
                    blue[i] = max;
                }


            }*/
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color pixel = img.GetPixel(j, i);
                    if (pixel.R < min)
                        img.SetPixel(j, i, Color.FromArgb(0, min, min));
                    else if (pixel.R > max)
                        img.SetPixel(j, i, Color.FromArgb(0, max, max));
                    //Color pixel1 = img.GetPixel(j, i);
                    if (pixel.G < min)
                        img.SetPixel(j, i, Color.FromArgb(min, 0, min));
                    else if (pixel.G > max)
                        img.SetPixel(j, i, Color.FromArgb(max, 0, max));
                   // Color pixel2 = img.GetPixel(j, i);
                    if (pixel.B < min)
                        img.SetPixel(j, i, Color.FromArgb(min, min, 0));
                    else if (pixel.B > max)
                        img.SetPixel(j, i, Color.FromArgb(max, max, 0));

                    Color pixelR = R.GetPixel(j, i);
                    if (pixelR.R < min)
                        R.SetPixel(j, i, Color.FromArgb(min, 0, 0));
                    else if (pixelR.R > max)
                        R.SetPixel(j, i, Color.FromArgb(max, 0, 0));
                    Color pixelG = G.GetPixel(j, i);
                    if (pixelG.G < min)
                        G.SetPixel(j, i, Color.FromArgb(0, min, 0));
                    else if (pixelG.G > max)
                        G.SetPixel(j, i, Color.FromArgb(0, max, 0));
                    Color pixelB =B.GetPixel(j, i);
                    if (pixelB.B < min)
                        B.SetPixel(j, i, Color.FromArgb(0, 0, min));
                    else if (pixelB.B > max)
                        B.SetPixel(j, i, Color.FromArgb(0, 0, min));
                }
            }

           // ConvertToRGB(img, C, M, Y, key);
            
            return true;
        }
        public static Bitmap KuwaharaBlur(Bitmap Image, int Size)
        {
            System.Drawing.Bitmap TempBitmap = Image;
            System.Drawing.Bitmap NewBitmap = new System.Drawing.Bitmap(TempBitmap.Width, TempBitmap.Height);
            System.Drawing.Graphics NewGraphics = System.Drawing.Graphics.FromImage(NewBitmap);
            NewGraphics.DrawImage(TempBitmap, new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
            NewGraphics.Dispose();
            Random TempRandom = new Random();
            int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
            int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
            int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
            int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    int[] RValues = { 0, 0, 0, 0 };
                    int[] GValues = { 0, 0, 0, 0 };
                    int[] BValues = { 0, 0, 0, 0 };
                    int[] NumPixels = { 0, 0, 0, 0 };
                    int[] MaxRValue = { 0, 0, 0, 0 };
                    int[] MaxGValue = { 0, 0, 0, 0 };
                    int[] MaxBValue = { 0, 0, 0, 0 };
                    int[] MinRValue = { 255, 255, 255, 255 };
                    int[] MinGValue = { 255, 255, 255, 255 };
                    int[] MinBValue = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
                        {
                            int TempX = x + x2;
                            if (TempX >= 0 && TempX < NewBitmap.Width)
                            {
                                for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
                                {
                                    int TempY = y + y2;
                                    if (TempY >= 0 && TempY < NewBitmap.Height)
                                    {
                                        Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                        RValues[i] += TempColor.R;
                                        GValues[i] += TempColor.G;
                                        BValues[i] += TempColor.B;
                                        if (TempColor.R > MaxRValue[i])
                                        {
                                            MaxRValue[i] = TempColor.R;
                                        }
                                        else if (TempColor.R < MinRValue[i])
                                        {
                                            MinRValue[i] = TempColor.R;
                                        }

                                        if (TempColor.G > MaxGValue[i])
                                        {
                                            MaxGValue[i] = TempColor.G;
                                        }
                                        else if (TempColor.G < MinGValue[i])
                                        {
                                            MinGValue[i] = TempColor.G;
                                        }

                                        if (TempColor.B > MaxBValue[i])
                                        {
                                            MaxBValue[i] = TempColor.B;
                                        }
                                        else if (TempColor.B < MinBValue[i])
                                        {
                                            MinBValue[i] = TempColor.B;
                                        }
                                        ++NumPixels[i];
                                    }
                                }
                            }
                        }
                    }
                    int j = 0;
                    int MinDifference = 10000;
                    for (int i = 0; i < 4; ++i)
                    {
                        int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
                        if (CurrentDifference < MinDifference && NumPixels[i] > 0)
                        {
                            j = i;
                            MinDifference = CurrentDifference;
                        }
                    }

                    Color MeanPixel = Color.FromArgb(RValues[j] / NumPixels[j],
                        GValues[j] / NumPixels[j],
                        BValues[j] / NumPixels[j]);
                    NewBitmap.SetPixel(x, y, MeanPixel);
                }
            }
            return NewBitmap;
        }
        public static void ODithering(Bitmap s)
        {
            int w = s.Width;
            int h = s.Height;
            int[,] mat = new int[,] { { 1, 9, 3, 11 }, { 13, 5, 15, 7 }, { 4, 12, 2, 10 }, { 16, 8, 14, 6 } };

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    int t = mat[x % 4, y % 4];
                    Color c = s.GetPixel(x, y);
                    int red, green, blue;
                    int r= ((c.R * 18) / 255);
                    int g = ((c.G * 18) / 255);
                    int b = ((c.B * 18) / 255);

                    if (r < t)
                        red = 0;
                    else red = c.R;

                    if (g < t)
                        green = 0;
                    else green = c.G;

                    if (b < t)
                        blue = 0;
                    else blue = c.B;
                    s.SetPixel(x, y, Color.FromArgb(red, green, blue));

                }
            }
        }
    }
}
