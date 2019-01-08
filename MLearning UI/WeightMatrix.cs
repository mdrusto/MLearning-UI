using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    class WeightMatrix
    {
        private readonly WeightMatrixRow[] rows;

        public WeightMatrix(int rowCount, int columnCount)
        {
            rows = new WeightMatrixRow[rowCount];
            for (int index = 0; index < rowCount; index++)
            {
                rows[index] = new WeightMatrixRow(columnCount);
            }
        }

        public void SetWeight(int row, int column, double value)
        {
            rows[row].SetWeight(column, value);
        }

        public double GetWeight(int row, int column)
        {
            return rows[row].GetWeight(column);
        }
    }

    class WeightMatrixRow
    {
        private readonly double[] weights;

        public WeightMatrixRow(int columnCount)
        {
            weights = new double[columnCount];
        }

        public double GetWeight(int column)
        {
            return weights[column];
        }

        public void SetWeight(int column, double value)
        {
            weights[column] = value;
        }
    }
}
