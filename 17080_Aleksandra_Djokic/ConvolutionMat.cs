using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace _17080_Aleksandra_Djokic
{
    class ConvolutionMat
    {
		private int Factor { get; set; }
		private int Offset { get; set; }

		public int N { get; set; }
		private int[,] Matrix;

		private int OutValue { get; set; }
		private int Center { get; set; }

		public ConvolutionMat(int o, int c, int offset, int n)
		{
			this.OutValue = o;
			this.Center = c;
			this.Offset = offset;

			if (n % 2 == 0)
				this.N = 3;
			else
				this.N = n;

			this.Matrix = new int[this.N, this.N];

			for (int i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
					this.Matrix[i, j] = this.OutValue;

			int m = (this.N - 1) / 2;
			this.Matrix[m, m] = this.Center;

			int sum = 0;
			for (int i = 0; i < ((this.N * this.N) - 1); i++)
				sum += this.OutValue;
			this.Factor = sum + this.Center;
		}

		public int[] CalculateRGB(Color[,] pic)
		{
			int[] px = new int[3];
			int kR, kG, kB;
			int sR = 0;
			int sG = 0;
			int sB = 0;

			for (int i = 0; i < this.N; i++)
				for (int j = 0; j < this.N; j++)
				{
					sR += pic[i, j].R * this.Matrix[i, j];
					sG += pic[i, j].G * this.Matrix[i, j];
					sB += pic[i, j].B * this.Matrix[i, j];
				}

			kR = (sR / this.Factor) + this.Offset;
			if (kR < 0)
				kR = 0;
			else if (kR > 255)
				kR = 255;
			px[0] = kR;

			kG = (sG / this.Factor) + this.Offset;
			if (kG < 0)
				kG = 0;
			else if (kG > 255)
				kG = 255;
			px[1] = kG;

			kB = (sB / this.Factor) + this.Offset;
			if (kB < 0)
				kB = 0;
			else if (kB > 255)
				kB = 255;
			px[2] = kB;

			return px;
		}

		public void CalculateRGBUnsafe(ref Bitmap img)
		{
			int w = img.Width;
			int h = img.Height;
			BitmapData imgData = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			Bitmap copy = (Bitmap)img.Clone();
			int wc = copy.Width;
			int hc = copy.Height;
			BitmapData copyData = copy.LockBits(new Rectangle(0, 0, wc, hc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = imgData.Stride;
			System.IntPtr Scan0 = imgData.Scan0;
			int stride2 = stride * 2;
			System.IntPtr CopyScan0 = copyData.Scan0;

			unsafe
			{
				int topLeft = this.Matrix[0, 0];
				int topMid = this.Matrix[0, 1];
				int topRight = this.Matrix[0, 2];
				int midLeft = this.Matrix[1, 0];
				int pixel = this.Matrix[1, 1];
				int midRight = this.Matrix[1, 2];
				int bottomLeft = this.Matrix[2, 0];
				int bottomMid = this.Matrix[2, 1];
				int bottomRight = this.Matrix[2, 2];

				int sum, tmpStride, index, nPixel;
				int nOffset = stride - w * 3;
				int centerIndex = ((this.N - 1) * 3) / 2;
				int next = 3;

				int b = 0;
				int g = 1;
				int r = 2;

				byte* p = (byte*)(void*)Scan0;
				byte* pCopy = (byte*)(void*)CopyScan0;

				for (int y = 0; y < h - 2; ++y)
				{
					for (int x = 0; x < w - 2; ++x)
					{
						//red
						sum = 0;
						tmpStride = 0;
						for (int i = 0; i < this.N; i++)
						{
							index = 0;
							for (int j = 0; j < this.N; j++)
							{
								sum += this.Matrix[i, j] * pCopy[index + r + tmpStride];
								index += next;
							}
							tmpStride += stride;
						}

						nPixel = (sum / this.Factor) + this.Offset;
						if (nPixel < 0)
							nPixel = 0;
						else if (nPixel > 255)
							nPixel = 255;
						p[centerIndex + r + stride] = (byte)nPixel;

						//green
						sum = 0;
						tmpStride = 0;
						for (int i = 0; i < this.N; i++)
						{
							index = 0;
							for (int j = 0; j < this.N; j++)
							{
								sum += this.Matrix[i, j] * pCopy[index + g + tmpStride];
								index += next;
							}
							tmpStride += stride;
						}

						nPixel = ((sum / this.Factor) + this.Offset);
						if (nPixel < 0)
							nPixel = 0;
						else if (nPixel > 255)
							nPixel = 255;
						p[centerIndex + g + stride] = (byte)nPixel;

						//blue
						sum = 0;
						tmpStride = 0;
						for (int i = 0; i < this.N; i++)
						{
							index = 0;
							for (int j = 0; j < this.N; j++)
							{
								sum += this.Matrix[i, j] * pCopy[index + b + tmpStride];
								index += next;
							}
							tmpStride += stride;
						}

						nPixel = (sum / this.Factor) + this.Offset;
						if (nPixel < 0)
							nPixel = 0;
						else if (nPixel > 255)
							nPixel = 255;
						p[centerIndex + b + stride] = (byte)nPixel;
						p += 3;
						pCopy += 3;
					}
					p += nOffset;
					pCopy += nOffset;
				}
			}
			img.UnlockBits(imgData);
			copy.UnlockBits(copyData);
		}
	}
}
