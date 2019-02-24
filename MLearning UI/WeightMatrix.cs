using System;

namespace MLearning_UI
{
    class WeightMatrix
    {
        private readonly WeightMatrixRow[] rows;

        public WeightMatrix(int rowCount, int columnCount)
        {
            if (rowCount <= 0 || columnCount <= 0)
                throw new IndexOutOfRangeException($"Matrix row, column lengths must be positive, got {rowCount}, {columnCount}");
            rows = new WeightMatrixRow[rowCount];
            for (int index = 0; index < rowCount; index++)
            {
                rows[index] = new WeightMatrixRow(columnCount);
            }
        }

        public void SetWeight(int row, int column, double value)
        {
            if (row < 0 || row >= rows.Length)
                throw new IndexOutOfRangeException($"Row index {row} was not within bounds 0-{rows.Length}");
            if (column < 0 || column >= rows[0].Length)
                throw new IndexOutOfRangeException($"Column index {column} was not within bounds 0-{rows[0].Length}");

            rows[row].SetWeight(column, value);
        }

        public double GetWeight(int row, int column)
        {
            if (row < 0 || row >= rows.Length)
                throw new IndexOutOfRangeException($"Row index {row} was not within bounds 0-{rows.Length}");
            if (column < 0 || column >= rows[0].Length)
                throw new IndexOutOfRangeException($"Column index {column} was not within bounds 0-{rows[0].Length}");
            return rows[row].GetWeight(column);
        }
    }

    class WeightMatrixRow
    {
        private readonly double[] weights;
        public int Length { get; }

        public WeightMatrixRow(int columnCount)
        {
            weights = new double[columnCount];
            Length = weights.Length;
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
