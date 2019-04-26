using System;

namespace MLearning_UI.Network_Elements
{
    public class NetworkResult
    {
        public readonly LayerSnapshot[] snapshots;

        public double[] Output
        {
            get
            {
                return snapshots[snapshots.Length - 1].Output;
            }
        }

        public NetworkResult(LayerSnapshot[] snapshots)
        {
            this.snapshots = snapshots;
        }

        public NetworkGradient GetGradient()
        {
            return null;
        }
    }
}
