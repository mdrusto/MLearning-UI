using System;

namespace MLearning_UI
{
    public class LayerSnapshot
    {
        public int Length { get; }
        public double[] ZValues { get; }
        public double[] Output { get; }

        public LayerSnapshot(double[] zValues, double[] output)
        {
            this.ZValues = zValues;
            this.Output = output;
            this.Length = zValues.Length;
        }
    }
}
