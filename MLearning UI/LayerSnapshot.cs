using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    public class LayerSnapshot
    {
        public readonly int length;
        public readonly double[] zValues, output;

        public LayerSnapshot(double[] zValues, double[] output)
        {
            this.zValues = zValues;
            this.output = output;
            this.length = zValues.Length;
        }
    }
}
