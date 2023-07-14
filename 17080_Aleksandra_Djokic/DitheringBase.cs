using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
namespace _17080_Aleksandra_Djokic
{
     public delegate Color FindColor(Color original);

    public abstract class  DitheringBase
    {
        protected Bitmap currentBitmap = null; 
        protected byte[] currentBitmapAsByteArray = null; 

        protected int width;
        protected int height;

        protected bool isFastMode = false;

        protected string methodLongName = "";
        protected string fileNameAddition = "";

        protected FindColor colorFunction = null;

        public DitheringBase(FindColor colorfunc, bool useFastMode = false)
        {
            this.colorFunction = colorfunc;
            this.isFastMode = useFastMode;
        }

        public Bitmap DoDithering(Bitmap input)
        {
            if (this.isFastMode)
            {
                return this.DoDitheringFast(input);
            }

            return this.DoDitheringSlow(input);
        }

        private Bitmap DoDitheringFast(Bitmap input)
        {
            this.width = input.Width;
            this.height = input.Height;

            Rectangle rect = new Rectangle(0, 0, input.Width, input.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                input.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                input.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
 
            int byteCount = Math.Abs(bmpData.Stride) * input.Height;
            this.currentBitmapAsByteArray = new byte[byteCount];

         
            System.Runtime.InteropServices.Marshal.Copy(ptr, this.currentBitmapAsByteArray, 0, byteCount);

            input.UnlockBits(bmpData);

            Color originalPixel = Color.White; 
            Color newPixel = Color.White; 
            short[] quantError = null; 

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    originalPixel = GetColorFromByteArray(this.currentBitmapAsByteArray, x, y, this.width);
                    newPixel = this.colorFunction(originalPixel);

                    SetColorToByteArray(this.currentBitmapAsByteArray, x, y, this.width, newPixel);

                    quantError = GetQuantError(originalPixel, newPixel);
                    this.PushError(x, y, quantError);
                }
            }

            
            Bitmap returnBitmap = new Bitmap(this.width, this.height);
           
            bmpData =
                returnBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                input.PixelFormat);
            
            ptr = bmpData.Scan0;
    
            System.Runtime.InteropServices.Marshal.Copy(this.currentBitmapAsByteArray, 0, ptr, byteCount);

            returnBitmap.UnlockBits(bmpData);

            return returnBitmap;
        }

        private Bitmap DoDitheringSlow(Bitmap input)
        {
            this.width = input.Width;
            this.height = input.Height;

            
            this.currentBitmap = new Bitmap(input);

            Color originalPixel = Color.White;
            Color newPixel = Color.White; 
            short[] quantError = null; 

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    originalPixel = this.currentBitmap.GetPixel(x, y);
                    newPixel = this.colorFunction(originalPixel);

                    this.currentBitmap.SetPixel(x, y, newPixel);

                    quantError = GetQuantError(originalPixel, newPixel);

                    this.PushError(x, y, quantError);
                }
            }

            return this.currentBitmap;
        }

        public string GetMethodName()
        {
            return this.methodLongName;
        }

        public string GetFilenameAddition()
        {
            return this.fileNameAddition;
        }

        protected short[] GetQuantError(Color originalPixel, Color newPixel)
        {
            short[] returnValue = new short[4];

            returnValue[0] = (short)(originalPixel.R - newPixel.R);
            returnValue[1] = (short)(originalPixel.G - newPixel.G);
            returnValue[2] = (short)(originalPixel.B - newPixel.B);
            returnValue[3] = (short)(originalPixel.A - newPixel.A);

            return returnValue;
        }

        protected bool IsValidCoordinate(int x, int y)
        {
            return (0 <= x && x < this.width && 0 <= y && y < this.height);
        }

       
        protected abstract void PushError(int x, int y, short[] quantError);

        public void ModifyImageWithErrorAndMultiplier(int x, int y, short[] quantError, float multiplier)
        {
            Color oldColor = Color.White; 
            if (this.isFastMode)
            {
                oldColor = GetColorFromByteArray(this.currentBitmapAsByteArray, x, y, this.width);
            }
            else
            {
                oldColor = this.currentBitmap.GetPixel(x, y);
            }

            Color newColor = Color.FromArgb(
                                
                                GetLimitedValue(oldColor.R, (int)Math.Round(quantError[0] * multiplier)),
                                GetLimitedValue(oldColor.G, (int)Math.Round(quantError[1] * multiplier)),
                                GetLimitedValue(oldColor.B, (int)Math.Round(quantError[2] * multiplier)));

            if (this.isFastMode)
            {
                SetColorToByteArray(this.currentBitmapAsByteArray, x, y, this.width, newColor);
            }
            else
            {
                this.currentBitmap.SetPixel(x, y, newColor);
            }
        }

        private static byte GetLimitedValue(byte original, int error)
        {
            int newValue = original + error;
            return (byte)Clamp(newValue, byte.MinValue, byte.MaxValue);
        }

        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        private static Color GetColorFromByteArray(byte[] byteArray, int x, int y, int width)
        {
            int baseAddress = 3 * (y * width + x);
            return Color.FromArgb(byteArray[baseAddress + 2], byteArray[baseAddress + 1], byteArray[baseAddress]);
        }

        private static void SetColorToByteArray(byte[] byteArray, int x, int y, int width, Color color)
        {
            int baseAddress = 3 * (y * width + x);
            byteArray[baseAddress + 2] = color.R;
            byteArray[baseAddress + 1] = color.G;
            byteArray[baseAddress] = color.B;
        }
    }
}
