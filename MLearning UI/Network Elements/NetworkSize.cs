using System;

namespace MLearning_UI.Network_Elements
{
    public class NetworkSize
    {
        public int InputLayerLength { get; }
        public int OutputLayerLength { get; }
        public int[] InternalLayerLengths { get; }
        public int InternalLayerCount { get; }
        public int TotalLayerCount
        {
            get
            {
                return InternalLayerCount + 2;
            }
        }

        public NetworkSize(int inputLayerLength, int[] internalLayerLengths, int outputLayerLength)
        {
            this.InputLayerLength = inputLayerLength;
            this.InternalLayerLengths = internalLayerLengths;
            this.OutputLayerLength = outputLayerLength;
            this.InternalLayerCount = internalLayerLengths.Length;
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is NetworkSize))
                return false;
            NetworkSize other = (NetworkSize)obj;
            if (InputLayerLength != other.InputLayerLength)
                return false;
            if (OutputLayerLength != other.OutputLayerLength)
                return false;
            for (int i = 0; i < InternalLayerCount; i++)
            {
                if (InternalLayerLengths[i] != other.InternalLayerLengths[i])
                    return false;
            }
            return true;
        }
    }
}
