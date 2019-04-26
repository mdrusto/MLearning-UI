using System;

namespace MLearning_UI.Network_Elements
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
