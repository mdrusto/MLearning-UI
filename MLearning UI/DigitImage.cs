using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    public class DigitImage
    {
        public byte[,] contents;
        public byte label;

        public DigitImage(byte[,] contents, byte label)
        {
            this.contents = contents;
            this.label = label;
        }

        public byte GetValueAt(int y, int x)
        {
            return contents[y, x];
        }

        public byte GetLabel()
        {
            return label;
        }
    }
}
