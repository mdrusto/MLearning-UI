using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI.Network_Elements
{
    public class DigitImage
    {
        public byte[,] contents;
        public double[] AsDoubleArray { get; internal set; }
        public byte Label { get; }

        public DigitImage(byte[,] contents, byte label)
        {
            this.contents = contents;
            int length = contents.GetLength(0);
            int width = contents.GetLength(1);
            AsDoubleArray = new double[contents.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                AsDoubleArray[i] = (double)contents[i / length, i % length] / 256.0;
            }
        }

        public byte GetValueAt(int y, int x)
        {
            return contents[y, x];
        }
    }
}
