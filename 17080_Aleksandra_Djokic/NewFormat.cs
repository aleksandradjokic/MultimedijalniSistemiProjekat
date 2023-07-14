using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace _17080_Aleksandra_Djokic
{
    class NewFormat
    {
        public static int imgWitdth;
        public static int imgHeight;

        public static double[] yCh;
        public static double[] uCh;
        public static double[] vCh;

        public static byte[] yChByte;
        public static byte[] uChByte;
        public static byte[] vChByte;

        public static int yChPos;
        public static int uChPos;
        public static int vChPos;

        public static byte[] compSirina;

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

        public struct YUV
        {
            private double _y;
            private double _u;
            private double _v;

            public YUV(double y, double u, double v)
            {
                this._y = y;
                this._u = u;
                this._v = v;
            }

            public double Y
            {
                get { return this._y; }
                set { this._y = value; }
            }

            public double U
            {
                get { return this._u; }
                set { this._u = value; }
            }

            public double V
            {
                get { return this._v; }
                set { this._v = value; }
            }

            public bool Equals(YUV yuv)
            {
                return (this.Y == yuv.Y) && (this.U == yuv.U) && (this.V == yuv.V);
            }
        }


        public static byte[] GetBytes(double[] values)
        {
            var result = new byte[values.Length * sizeof(double)];
            Buffer.BlockCopy(values, 0, result, 0, result.Length);
            return result;
        }
        public static YUV RGBToYUV(RGB rgb)
        {
            double y = rgb.R * .299000 + rgb.G * .587000 + rgb.B * .114000;
            double u = rgb.R * -.168736 + rgb.G * -.331264 + rgb.B * .500000 + 128;
            double v = rgb.R * .500000 + rgb.G * -.418688 + rgb.B * -.081312 + 128;

            return new YUV(y, u, v);
        }
        public static RGB YUVToRGB(YUV yuv)
        {
            byte r = (byte)(yuv.Y + 1.4075 * (yuv.V - 128));
            byte g = (byte)(yuv.Y - 0.3455 * (yuv.U - 128) - (0.7169 * (yuv.V - 128)));
            byte b = (byte)(yuv.Y + 1.7790 * (yuv.U - 128));

            return new RGB(r, g, b);
        }
        public void toByteArray()
        {
            yChByte = new byte[imgWitdth * imgHeight];
            for (int i = 0; i < yChPos; i++)
            {
                yChByte[i] = (byte)yCh[i];
            }

            uChByte = new byte[(imgWitdth * imgHeight) / 2 + 1];
            for (int i = 0; i < uChPos; i++)
            {
                uChByte[i] = (byte)uCh[i];
            }

            vChByte = new byte[(imgWitdth * imgHeight) / 2 + 1];
            for (int i = 0; i < vChPos; i++)
            {
                vChByte[i] = (byte)vCh[i];
            }
        }
        #region Commpress_Decompress_Functions

        //compress
        public class BitStream
        {
            public byte[] BytePointer;
            public uint BitPosition;
            public uint Index;
        }

        public struct Symbol
        {
            public uint Sym;
            public uint Count;
            public uint Code;
            public uint Bits;
        }
        private static void initBitStream(ref BitStream stream, byte[] buffer)
        {
            stream.BytePointer = buffer;
            stream.BitPosition = 0;
        }
        private static void writeBits(ref BitStream stream, uint x, uint bits)
        {
            byte[] buffer = stream.BytePointer;
            uint bit = stream.BitPosition;
            uint mask = (uint)(1 << (int)(bits - 1));

            for (uint count = 0; count < bits; ++count)
            {
                buffer[stream.Index] = (byte)((buffer[stream.Index] & (0xff ^ (1 << (int)(7 - bit)))) + ((Convert.ToBoolean(x & mask) ? 1 : 0) << (int)(7 - bit)));
                x <<= 1;
                bit = (bit + 1) & 7;

                if (!Convert.ToBoolean(bit))
                {
                    ++stream.Index;
                }
            }

            stream.BytePointer = buffer;
            stream.BitPosition = bit;
        }
        private static void histogram(byte[] input, Symbol[] sym, uint size)
        {
            Symbol temp;
            int i, swaps;
            int index = 0;

            for (i = 0; i < 256; ++i)
            {
                sym[i].Sym = (uint)i;
                sym[i].Count = 0;
                sym[i].Code = 0;
                sym[i].Bits = 0;
            }

            for (i = (int)size; Convert.ToBoolean(i); --i, ++index)
            {
                sym[input[index]].Count++;
            }
            do
            {
                swaps = 0;

                for (i = 0; i < 255; ++i)
                {
                    if (sym[i].Count < sym[i + 1].Count)
                    {
                        temp = sym[i];
                        sym[i] = sym[i + 1];
                        sym[i + 1] = temp;
                        swaps = 1;
                    }
                }
            } while (Convert.ToBoolean(swaps));
        }
        private static void makeTree(Symbol[] sym, ref BitStream stream, uint code, uint bits, uint first, uint last)
        {
            uint i, size, sizeA, sizeB, lastA, firstB;

            if (first == last)
            {
                writeBits(ref stream, 1, 1);
                writeBits(ref stream, sym[first].Sym, 8);
                sym[first].Code = code;
                sym[first].Bits = bits;
                return;
            }
            else
            {
                writeBits(ref stream, 0, 1);
            }

            size = 0;

            for (i = first; i <= last; ++i)
            {
                size += sym[i].Count;
            }

            sizeA = 0;

            for (i = first; sizeA < ((size + 1) >> 1) && i < last; ++i)
            {
                sizeA += sym[i].Count;
            }

            if (sizeA > 0)
            {
                writeBits(ref stream, 1, 1);

                lastA = i - 1;

                makeTree(sym, ref stream, (code << 1) + 0, bits + 1, first, lastA);
            }
            else
            {
                writeBits(ref stream, 0, 1);
            }

            sizeB = size - sizeA;

            if (sizeB > 0)
            {
                writeBits(ref stream, 1, 1);

                firstB = i;

                makeTree(sym, ref stream, (code << 1) + 1, bits + 1, firstB, last);
            }
            else
            {
                writeBits(ref stream, 0, 1);
            }
        }
        public static int Compress(byte[] input, byte[] output, uint inputSize)
        {
            Symbol[] sym = new Symbol[256];
            Symbol temp;
            BitStream stream = new BitStream();
            uint i, totalBytes, swaps, symbol, lastSymbol;

            if (inputSize < 1)
                return 0;

            initBitStream(ref stream, output);
            histogram(input, sym, inputSize);

            for (lastSymbol = 255; sym[lastSymbol].Count == 0; --lastSymbol) ;

            if (lastSymbol == 0)
                ++lastSymbol;

            makeTree(sym, ref stream, 0, 0, 0, lastSymbol);

            do
            {
                swaps = 0;

                for (i = 0; i < 255; ++i)
                {
                    if (sym[i].Sym > sym[i + 1].Sym)
                    {
                        temp = sym[i];
                        sym[i] = sym[i + 1];
                        sym[i + 1] = temp;
                        swaps = 1;
                    }
                }
            } while (Convert.ToBoolean(swaps));

            for (i = 0; i < inputSize; ++i)
            {
                symbol = input[i];
                writeBits(ref stream, sym[symbol].Code, sym[symbol].Bits);
            }

            totalBytes = stream.Index;

            if (stream.BitPosition > 0)
            {
                ++totalBytes;
            }

            return (int)totalBytes;
        }
        //decompress

        private const int MAX_TREE_NODES = 511;

        public class TreeNode
        {
            public TreeNode ChildA;
            public TreeNode ChildB;
            public int Symbol;
        }

        private static uint readBit(ref BitStream stream)
        {
            byte[] buffer = stream.BytePointer;
            uint bit = stream.BitPosition;
            uint x = (uint)(Convert.ToBoolean(buffer[stream.Index] & (1 << (int)(7 - bit))) ? 1 : 0);
            bit = (bit + 1) & 7;

            if (!Convert.ToBoolean(bit))
            {
                ++stream.Index;
            }

            stream.BitPosition = bit;

            return x;
        }
        private static uint read8Bits(ref BitStream stream)
        {
            byte[] buffer = stream.BytePointer;
            uint bit = stream.BitPosition;
            uint x = (uint)((buffer[stream.Index] << (int)bit) | (buffer[stream.Index + 1] >> (int)(8 - bit)));
            ++stream.Index;

            return x;
        }
        private static TreeNode recoverTree(TreeNode[] nodes, ref BitStream stream, ref uint nodeNumber)
        {
            TreeNode thisNode;

            thisNode = nodes[nodeNumber];
            nodeNumber = nodeNumber + 1;

            thisNode.Symbol = -1;
            thisNode.ChildA = null;
            thisNode.ChildB = null;

            if (Convert.ToBoolean(readBit(ref stream)))
            {
                thisNode.Symbol = (int)read8Bits(ref stream);
                return thisNode;
            }

            if (Convert.ToBoolean(readBit(ref stream)))
            {
                thisNode.ChildA = recoverTree(nodes, ref stream, ref nodeNumber);
            }

            if (Convert.ToBoolean(readBit(ref stream)))
            {
                thisNode.ChildB = recoverTree(nodes, ref stream, ref nodeNumber);
            }

            return thisNode;
        }
       public static void Decompress(byte[] input, byte[] output, uint inputSize, uint outputSize)
        {
            TreeNode[] nodes = new TreeNode[MAX_TREE_NODES];

            for (int counter = 0; counter < nodes.Length; counter++)
            {
                nodes[counter] = new TreeNode();
            }

            TreeNode root, node;
            BitStream stream = new BitStream();
            uint i, nodeCount;
            byte[] buffer;

            if (inputSize < 1) return;

            initBitStream(ref stream, input);

            nodeCount = 0;
            root = recoverTree(nodes, ref stream, ref nodeCount);
            buffer = output;

            for (i = 0; i < outputSize; ++i)
            {
                node = root;

                while (node.Symbol < 0)
                {
                    if (Convert.ToBoolean(readBit(ref stream)))
                        node = node.ChildB;
                    else
                        node = node.ChildA;
                }

                buffer[i] = (byte)node.Symbol;
            }
        }
        #endregion
        public Bitmap saveImageToCustomFormat(Bitmap bmap)
        {
            Bitmap lclBMap = new Bitmap(bmap);

            int iHeight = bmap.Height;
            int iWidth = bmap.Width;

            imgHeight = iHeight;
            imgWitdth = iWidth;

            yCh = new double[imgWitdth * imgHeight];
            uCh = new double[(imgWitdth * imgHeight)];
            vCh = new double[(imgWitdth * imgHeight)];

            yChPos = uChPos = vChPos = 0;


            Color cl;
            //int cc=0;
            int kk = 0;
            int mm = 0;
            bool cc = true;

            byte R, G, B;
            double Y, U, V;
            Y = U = V = 0;

            int left, right, up, down;
            int count;
            left = right = up = down = count = 0;
            int rr, gg, bb;
            rr = gg = bb = 0;

            for (int x = 0; x < iWidth; x++) //iz bitmape u niz
            {
                if (x % 2 == 0 && x > 0)
                {
                    cc = !cc;
                }

                if (cc)
                    kk = 1;
                if (!cc)
                    mm = 1;
                for (int y = 0; y < iHeight; y++)
                {
                    if (kk > 4 && cc) { kk = 1; }
                    if (mm > 4 && !cc) { mm = 1; }

                    cl = lclBMap.GetPixel(x, y);

                    R = lclBMap.GetPixel(x, y).R;
                    G = lclBMap.GetPixel(x, y).G;
                    B = lclBMap.GetPixel(x, y).B;


                    YUV valueYUV = RGBToYUV(new RGB(R, G, B));

                    //to do with YUV value

                    yCh[yChPos] = valueYUV.Y;
                    yChPos++;

                    if ((kk == 1 || kk == 2) && kk <= 2 && cc)
                    {
                        uCh[uChPos] = valueYUV.U;
                        uChPos++;
                        // kk++;

                        vCh[vChPos] = valueYUV.V;
                        vChPos++;


                    }



                    if ((mm == 3 || mm == 4) && !cc)
                    {
                        uCh[uChPos] = valueYUV.U;
                        uChPos++;

                        //

                        vCh[vChPos] = valueYUV.V;
                        vChPos++;
                    }



                    mm++; kk++;

                }
            }

            this.toByteArray();

            //newnewnewnew

            Bitmap newBitMap = new Bitmap(lclBMap.Width, lclBMap.Height);

            cc = true;
            int indexChY = yChPos;//nova pozicija spremna za upis

            int indexChU = uChPos;//

            int indexChV = vChPos;//

            int iY, iU, iV;
            iY = iU = iV = 0;

            for (int x = 0; x < iWidth; x++) //iz niza u bitmapu
            {
                if (x % 2 == 0 && x > 0)
                {
                    cc = !cc;
                }

                if (cc)
                    kk = 1;
                if (!cc)
                    mm = 1;

                for (int y = 0; y < iHeight; y++)
                {
                    if (kk > 4 && cc) { kk = 1; }
                    if (mm > 4 && !cc) { mm = 1; }



                    Y = yCh[iY];
                    iY++;

                    if ((kk == 1 || kk == 2) && kk <= 2 && cc)
                    {


                        U = uCh[iU];
                        iU++;

                        V = vCh[iV];
                        iV++;


                    }



                    if ((mm == 3 || mm == 4) && !cc)
                    {


                        U = uCh[iU];
                        iU++;

                        V = vCh[iV];
                        iV++;
                    }


                    //-----
                    if (mm == 3 || mm == 4 || kk == 1 || kk == 2)
                    {
                        RGB valueRGB = new RGB();

                        YUV valueYUV = new YUV(Y, U, V);

                        valueRGB = YUVToRGB(valueYUV);

                        newBitMap.SetPixel(x, y, Color.FromArgb(valueRGB.R, valueRGB.G, valueRGB.B));



                    }

                    mm++; kk++;
                }
            }

            ///reconstructing----------------
            ///
            cc = false;

            for (int x = 0; x < iWidth; x++)
            {
                if (x % 2 == 0 && x > 0)
                {
                    cc = !cc;
                }

                if (cc)
                    kk = 1;
                if (!cc)
                    mm = 1;

                for (int y = 0; y < iHeight; y++)
                {
                    if (kk > 4 && cc) { kk = 1; }
                    if (mm > 4 && !cc) { mm = 1; }



                    //   Y = yCh[iY];
                    //    iY++;

                    if ((kk == 1 || kk == 2) && kk <= 2 && cc)
                    {
                        count = 0;
                        rr = gg = bb = 0;

                        if (y - 1 >= 0) // za UP
                        {
                            cl = newBitMap.GetPixel(x, y - 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;

                        }

                        if (y + 1 <= (iHeight - 1))// za DOWN
                        {
                            cl = newBitMap.GetPixel(x, y + 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x - 1 >= 0) //za LEFT
                        {
                            cl = newBitMap.GetPixel(x - 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x + 1 <= (iWidth - 1))//za RIGHT
                        {
                            cl = newBitMap.GetPixel(x + 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }
                    }


                    //za UP: bitmap(x, y-1)
                    //za DOWN: bitmap(x, y+1)
                    //za LEFT: bitmap(x-1, y)
                    //za RIGHT: bitmap(x+1,y)

                    if ((mm == 3 || mm == 4) && !cc)
                    {
                        count = 0;
                        rr = gg = bb = 0;

                        if (y - 1 >= 0) // za UP
                        {
                            cl = newBitMap.GetPixel(x, y - 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;

                        }

                        if (y + 1 <= (iHeight - 1))// za DOWN
                        {
                            cl = newBitMap.GetPixel(x, y + 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x - 1 >= 0) //za LEFT
                        {
                            cl = newBitMap.GetPixel(x - 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x + 1 <= (iWidth - 1))//za RIGHT
                        {
                            cl = newBitMap.GetPixel(x + 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                    }



                    if (mm == 3 || mm == 4 || kk == 1 || kk == 2)
                    {

                        if (count == 0) count = 1;

                        rr /= count;
                        gg /= count;
                        bb /= count;

                        newBitMap.SetPixel(x, y, Color.FromArgb(rr, gg, bb));


                    }

                    mm++; kk++;
                }
            }

            //kompresija bez gubitaka SHANNON-FANO ALGORITAM

            //string str = "This is an example for Shannon–Fano coding"; //compress proccess
            //byte[] originalData = Encoding.Default.GetBytes(str);
            //uint originalDataSize = (uint)str.Length;
            //byte[] compressedData = new byte[originalDataSize * (101 / 100) + 384];
            //int compressedDataSize = Compress(originalData, compressedData, originalDataSize);



            byte[] orgChY = yChByte;
            uint orgChYSize = (uint)yChByte.Length;
            byte[] compChY = new byte[orgChYSize * (101 / 100) + 384];
            int compChYSize = Compress(orgChY, compChY, orgChYSize);


            byte[] orgChU = uChByte;
            uint orgChUSize = (uint)uChByte.Length;
            byte[] compChU = new byte[orgChUSize * (101 / 100) + 384];
            int compChUSize = Compress(orgChU, compChU, orgChUSize);

            byte[] orgChV = vChByte;
            uint orgChVSize = (uint)vChByte.Length;
            byte[] compChV = new byte[orgChVSize * (101 / 100) + 384];
            int compChVSize = Compress(orgChV, compChV, orgChVSize);





            //byte[] decompressedData = new byte[originalDataSize];//decompress
            //Decompress(compressedData, decompressedData, (uint)compressedDataSize, originalDataSize);



            //end kompresija



            //file manipulation

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "All Files|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {



                FileStream output = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWtr = new BinaryWriter(output);
                // BinaryReader binRdr = new BinaryReader(output);

                binWtr.Write(Int16.Parse(iWidth.ToString()));
                binWtr.Write(Int16.Parse(iHeight.ToString()));
                binWtr.Write(compChYSize);
                binWtr.Write(compChUSize);
                binWtr.Write(compChVSize);

                //for (int i = 0; i < indexChY; i++)
                //{

                //    binWtr.Write(yChByte[i]);
                //}

                //for (int i = 0; i < indexChU; i++)
                //{

                //    binWtr.Write(uChByte[i]);
                //}

                //for (int i = 0; i < indexChV; i++)
                //{

                //    binWtr.Write(vChByte[i]);
                //}

                // binWtr.Write(compWidth);
                //  binWtr.Write(compHeight);

                for (int i = 0; i < compChYSize; i++)
                {
                    binWtr.Write(compChY[i]);
                }

                for (int i = 0; i < compChUSize; i++)
                {
                    binWtr.Write(compChU[i]);
                }

                for (int i = 0; i < compChVSize; i++)
                {
                    binWtr.Write(compChV[i]);
                }



                binWtr.Close();

            }



            //end file manipulation



            return newBitMap;
        }
        public Bitmap loadImageToCustomFormat()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            BinaryReader binRdr = null;

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                binRdr = new BinaryReader(new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read));
            }

            //   binRdr = new BinaryReader(new FileStream("test.bin", FileMode.Open, FileAccess.Read));


            //byte[] decompressedData = new byte[originalDataSize];//decompress
            //   Decompress(compressedData, decompressedData, (uint)compressedDataSize, originalDataSize);



            int sirina = binRdr.ReadInt16();
            int visina = binRdr.ReadInt16();
            int kompYChVel = binRdr.ReadInt32();
            int kompUChVel = binRdr.ReadInt32();
            int kompVChVel = binRdr.ReadInt32();

            byte[] nizY = new byte[sirina * visina];
            byte[] nizU = new byte[(sirina * visina) / 2 + 1];
            byte[] nizV = new byte[(sirina * visina) / 2 + 1];


            Bitmap bmapa = new Bitmap(sirina, visina);

            byte[] rawYCh = binRdr.ReadBytes(kompYChVel);
            byte[] decompYCh = new byte[sirina * visina];
            Decompress(rawYCh, decompYCh, (uint)kompYChVel, (uint)(sirina * visina));
            nizY = decompYCh;

            byte[] rawUCh = binRdr.ReadBytes(kompUChVel);
            byte[] decompUCh = new byte[sirina * visina / 2 + 1];
            Decompress(rawUCh, decompUCh, (uint)kompUChVel, (uint)(sirina * visina / 2 + 1));
            nizU = decompUCh;

            byte[] rawVCh = binRdr.ReadBytes(kompVChVel);
            byte[] decompVCh = new byte[sirina * visina / 2 + 1];
            Decompress(rawVCh, decompVCh, (uint)kompVChVel, (uint)(sirina * visina / 2 + 1));
            nizV = decompVCh;

            // byte[] ostalo = binRdr.Read





            //for (int i = 0; i < sirina*visina; i++)
            //{
            //    nizY[i] = binRdr.ReadByte();
            //}

            //for (int i = 0; i < (sirina*visina)/2; i++)
            //{
            //    nizU[i] = binRdr.ReadByte();
            //}

            //for (int i = 0; i < (sirina * visina) / 2; i++)
            //{
            //    nizV[i] = binRdr.ReadByte();
            //}


            bool cc = true;
            int kk, mm;
            kk = mm = 0;
            int Y, U, V;

            int iY, iU, iV;
            iY = iU = iV = 0;

            Y = U = V = 0;

            for (int x = 0; x < sirina; x++) //iz niza u bitmapu
            {
                if (x % 2 == 0 && x > 0)
                {
                    cc = !cc;
                }

                if (cc)
                    kk = 1;
                if (!cc)
                    mm = 1;

                for (int y = 0; y < visina; y++)
                {
                    if (kk > 4 && cc) { kk = 1; }
                    if (mm > 4 && !cc) { mm = 1; }



                    Y = nizY[iY];
                    iY++;

                    if ((kk == 1 || kk == 2) && kk <= 2 && cc)
                    {


                        U = nizU[iU];
                        iU++;

                        V = nizV[iV];
                        iV++;


                    }



                    if ((mm == 3 || mm == 4) && !cc)
                    {


                        U = nizU[iU];
                        iU++;

                        V = nizV[iV];
                        iV++;
                    }


                    //-----
                    if (mm == 3 || mm == 4 || kk == 1 || kk == 2)
                    {
                        RGB valueRGB = new RGB();

                        YUV valueYUV = new YUV(Y, U, V);

                        valueRGB = YUVToRGB(valueYUV);

                        bmapa.SetPixel(x, y, Color.FromArgb(valueRGB.R, valueRGB.G, valueRGB.B));



                    }

                    mm++; kk++;
                }
            }


            int count, rr, gg, bb;
            Color cl;
            count = rr = gg = bb = 0;
            ///reconstruction
            ///
            cc = false;

            for (int x = 0; x < sirina; x++)
            {
                if (x % 2 == 0 && x > 0)
                {
                    cc = !cc;
                }

                if (cc)
                    kk = 1;
                if (!cc)
                    mm = 1;

                for (int y = 0; y < visina; y++)
                {
                    if (kk > 4 && cc) { kk = 1; }
                    if (mm > 4 && !cc) { mm = 1; }



                    //   Y = yCh[iY];
                    //    iY++;

                    if ((kk == 1 || kk == 2) && kk <= 2 && cc)
                    {
                        count = 0;
                        rr = gg = bb = 0;

                        if (y - 1 >= 0) // za UP
                        {
                            cl = bmapa.GetPixel(x, y - 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;

                        }

                        if (y + 1 <= (visina - 1))// za DOWN
                        {
                            cl = bmapa.GetPixel(x, y + 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x - 1 >= 0) //za LEFT
                        {
                            cl = bmapa.GetPixel(x - 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x + 1 <= (sirina - 1))//za RIGHT
                        {
                            cl = bmapa.GetPixel(x + 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }
                    }


                    //za UP: bitmap(x, y-1)
                    //za DOWN: bitmap(x, y+1)
                    //za LEFT: bitmap(x-1, y)
                    //za RIGHT: bitmap(x+1,y)

                    if ((mm == 3 || mm == 4) && !cc)
                    {
                        count = 0;
                        rr = gg = bb = 0;

                        if (y - 1 >= 0) // za UP
                        {
                            cl = bmapa.GetPixel(x, y - 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;

                        }

                        if (y + 1 <= (visina - 1))// za DOWN
                        {
                            cl = bmapa.GetPixel(x, y + 1);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x - 1 >= 0) //za LEFT
                        {
                            cl = bmapa.GetPixel(x - 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                        if (x + 1 <= (sirina - 1))//za RIGHT
                        {
                            cl = bmapa.GetPixel(x + 1, y);

                            rr += cl.R;
                            gg += cl.G;
                            bb += cl.B;

                            count++;
                        }

                    }



                    if (mm == 3 || mm == 4 || kk == 1 || kk == 2)
                    {

                        if (count == 0) count = 1;

                        rr /= count;
                        gg /= count;
                        bb /= count;

                        bmapa.SetPixel(x, y, Color.FromArgb(rr, gg, bb));


                    }

                    mm++; kk++;
                }
            }


            return bmapa;
        }
    }
}
