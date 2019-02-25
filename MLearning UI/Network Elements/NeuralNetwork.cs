using System;

namespace MLearning_UI
{
    public class NeuralNetwork
    {
        public NetworkSize Size { get; }
        private readonly Layer[] layers;

        private readonly LayerLink[] links;

        public double Accuracy { get; set; }
        public string Name { get; set; }

        public NeuralNetwork(NetworkSize size)
        {
            this.Size = size;
            layers = new Layer[size.TotalLayerCount];
            layers[0] = new Layer(size.InputLayerLength);
            for (int index = 1; index < size.InternalLayerCount + 1; index++)
            {
                layers[index] = new Layer(size.InternalLayerLengths[index - 1]);
            }
            layers[size.InternalLayerCount + 1] = new Layer(size.OutputLayerLength);
            this.links = new LayerLink[size.TotalLayerCount - 1];
            for (int index = 0; index < size.InternalLayerCount + 1 ; index++)
            {
                links[index] = new LayerLink(layers[index].Length, layers[index + 1].Length);
            }
        }

        public void Initialize(double bound, Random rand)
        {
            for (int index = 0; index < links.Length; index++)
            {
                for (int index1 = 0; index1 < layers[index + 1].Length; index1++)
                {
                    for (int index2 = 0; index2 < layers[index].Length; index2++)
                    {
                        double value = rand.NextDouble() * bound * 2 - bound;
                        links[index].InitializeWeightAt(index1, index2, value);
                    }
                    double value2 = rand.NextDouble() * bound * 2 - bound;
                    links[index].InitializeBiasAt(index1, value2);
                }
            }
        }

        public NetworkResult Run(double[] input)
        {
            double[] currentActivations = input;
            LayerSnapshot[] snapshots = new LayerSnapshot[Size.InternalLayerCount + 1];
            for (int layerIndex = 0; layerIndex < Size.InternalLayerCount + 1; layerIndex++)
            {
                double[] zValues = links[layerIndex].GetZValues(currentActivations);
                snapshots[layerIndex] = layers[layerIndex].getOutput(zValues);
                currentActivations = snapshots[layerIndex].Output;
            }
            return new NetworkResult(snapshots);
        }

        public void Train()
        {

        }
    }
}
