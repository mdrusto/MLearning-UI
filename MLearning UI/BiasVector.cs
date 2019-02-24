using System;

namespace MLearning_UI
{
    class BiasVector
    {
        private readonly double[] biases;

        public BiasVector(int rowCount)
        {
            if (rowCount <= 0)
                throw new IndexOutOfRangeException($"Bias vector length must be greater than 0, got {rowCount}");
            biases = new double[rowCount];
        }

        public double GetBias(int row)
        {
            if (row < 0 || row >= biases.Length)
                throw new IndexOutOfRangeException($"Row index {row} was not within bounds 0-{biases.Length}");
            return biases[row];
        }

        public void SetBias(int row, double value)
        {
            if (row < 0 || row >= biases.Length)
                throw new IndexOutOfRangeException($"Row index {row} was not within bounds 0-{biases.Length}");
            biases[row] = value;
        }
    }
}
