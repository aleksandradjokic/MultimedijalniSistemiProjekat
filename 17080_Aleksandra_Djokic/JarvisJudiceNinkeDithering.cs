using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace _17080_Aleksandra_Djokic
{
    class JarvisJudiceNinkeDithering
    {
        private readonly byte _divisor;

        private readonly byte[,] _matrix;

        private readonly byte _matrixHeight;

        private readonly byte _matrixWidth;

        private readonly byte _startingOffset;

        private readonly bool _useShifting;

        public JarvisJudiceNinkeDithering()
        {
            _divisor = 5;
            _matrix = new byte[,]
            {
               {
                 0, 0, 0, 7, 5
               },
               {
                 3, 5, 7, 5, 3
               },
               {
                 1, 3, 5, 3, 1
               }
            };
            _matrixHeight = 3;
            _matrixWidth = 5;
            _startingOffset = 2;
        }
        public void DoDiffuse(Color[] data, Color original, Color transformed, int x, int y, int width, int height)
        {
            int redError;
            int blueError;
            int greenError;

            redError = original.R - transformed.R;
            blueError = original.G - transformed.G;
            greenError = original.B - transformed.B;

            for (int row = 0; row < _matrixHeight; row++)
            {
                int offsetY;

                offsetY = y + row;

                for (int col = 0; col < _matrixWidth; col++)
                {
                    int coefficient;
                    int offsetX;

                    coefficient = _matrix[row, col];
                    offsetX = x + (col - _startingOffset);

                    if (coefficient != 0 && offsetX > 0 && offsetX < width && offsetY > 0 && offsetY < height)
                    {
                        Color offsetPixel;
                        int offsetIndex;
                        int newR;
                        int newG;
                        int newB;
                        byte r;
                        byte g;
                        byte b;

                        offsetIndex = offsetY * width + offsetX;
                        offsetPixel = data[offsetIndex];

                        newR = (redError * coefficient) >> _divisor;
                        newG = (greenError * coefficient) >> _divisor;
                        newB = (blueError * coefficient) >> _divisor;


                        r = ToByte((offsetPixel.R + newR));
                        g = ToByte((offsetPixel.G + newG));
                        b = ToByte((offsetPixel.B + newB));

                        data[offsetIndex] = Color.FromArgb(offsetPixel.A, r, g, b);
                    }
                }
            }
            
        }
        internal static byte ToByte(int value)
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 255)
                {
                    value = 255;
                }

                return (byte)value;
            }
       
    }
}
