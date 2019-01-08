using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    class BiasVector
    {
        private readonly double[] biases;

        public BiasVector(int rowCount)
        {
            biases = new double[rowCount];
        }

        public double GetBias(int row)
        {
            return biases[row];
        }

        public void SetBias(int row, double value)
        {
            biases[row] = value;
        }
    }
}
