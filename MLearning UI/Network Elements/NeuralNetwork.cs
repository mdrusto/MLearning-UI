using System;

namespace MLearning_UI.Network_Elements
{
    public class NeuralNetwork
    {
        public NetworkProperties Properties { get; }
        private readonly Layer[] layers;
        private readonly LayerLink[] links;
        public double Accuracy { get; set; }

        public NeuralNetwork(NetworkProperties props)
        {
            Properties = props;
            layers = new Layer[props.Size.TotalLayerCount];
            layers[0] = new Layer(props.Size.InputLayerLength);
            for (int index = 1; index < props.Size.InternalLayerCount + 1; index++)
            {
                layers[index] = new Layer(props.Size.InternalLayerLengths[index - 1]);
            }
            layers[props.Size.InternalLayerCount + 1] = new Layer(props.Size.OutputLayerLength);
            this.links = new LayerLink[props.Size.TotalLayerCount - 1];
            for (int index = 0; index < props.Size.InternalLayerCount + 1 ; index++)
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
            LayerSnapshot[] snapshots = new LayerSnapshot[Properties.Size.InternalLayerCount + 1];
            for (int layerIndex = 0; layerIndex < Properties.Size.InternalLayerCount + 1; layerIndex++)
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
