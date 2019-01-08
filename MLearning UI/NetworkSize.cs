using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
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
    }
}
