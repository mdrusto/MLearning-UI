using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    class Layer
    {
        public int Length { get; }

        public Layer(int length)
        {
            this.Length = length;
        }

        public LayerSnapshot getOutput(double[] zValues)
        {
            double[] output = new double[zValues.Length];

            for (int index = 0; index < output.Length; index++)
            {
                output[index] = 1 / (Math.Exp(-zValues[index]) + 1);
            }

            return new LayerSnapshot(zValues, output);
        }
    }
}
