using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    class LayerLink
    {
        private readonly int inputLength, outputLength;
        private readonly WeightMatrix weights;
        private readonly BiasVector biases;

        public LayerLink(int inputLength, int outputLength)
        {
            this.inputLength = inputLength;
            this.outputLength = outputLength;
            weights = new WeightMatrix(outputLength, inputLength);
            biases = new BiasVector(outputLength);
        }

        public void InitializeWeightAt(int row, int column, double value)
        {
            weights.SetWeight(row, column, value);
        }

        public void InitializeBiasAt(int row, double value)
        {
            biases.SetBias(row, value);
        }

        public double[] GetZValues(double[] input)
        {
            double[] zValues = new double[outputLength];
            for (int outputCounter = 0; outputCounter < outputLength; outputCounter++)
            {
                for (int inputCounter = 0; inputCounter < inputLength; inputCounter++)
                {
                    zValues[outputCounter] += input[inputCounter] * weights.GetWeight(outputCounter, inputCounter);
                }
                zValues[outputCounter] += biases.GetBias(outputCounter);
            }
            return zValues;
        }
    }
}
