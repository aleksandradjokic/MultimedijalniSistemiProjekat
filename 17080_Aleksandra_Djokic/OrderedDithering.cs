using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17080_Aleksandra_Djokic
{
    public class OrderedDithering : DitheringBase
    {
        public OrderedDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
        {
            this.methodLongName = "Floyd-Steinberg";
            this.fileNameAddition = "_FS";
        }

        override protected void PushError(int x, int y, short[] quantError)
        {
           
            int xMinusOne = x - 1;
            int xPlusOne = x + 1;
            int yPlusOne = y + 1;

            if (this.IsValidCoordinate(xPlusOne, y))
            {
                this.ModifyImageWithErrorAndMultiplier(xPlusOne, y, quantError, 7.0f / 16.0f);
            }

            if (this.IsValidCoordinate(xMinusOne, yPlusOne))
            {
                this.ModifyImageWithErrorAndMultiplier(xMinusOne, yPlusOne, quantError, 3.0f / 16.0f);
            }

            if (this.IsValidCoordinate(x, yPlusOne))
            {
                this.ModifyImageWithErrorAndMultiplier(x, yPlusOne, quantError, 5.0f / 16.0f);
            }

            if (this.IsValidCoordinate(xPlusOne, yPlusOne))
            {
                this.ModifyImageWithErrorAndMultiplier(xPlusOne, yPlusOne, quantError, 1.0f / 16.0f);
            }
        }
    }
}
