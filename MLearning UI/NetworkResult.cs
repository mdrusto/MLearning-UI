using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLearning_UI
{
    public class NetworkResult
    {
        public readonly LayerSnapshot[] snapshots;

        public double[] Output
        {
            get
            {
                return snapshots[snapshots.Length - 1].output;
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

        public bool[] equal(NetworkResult result2)
        {
            bool[] returnarray = new bool[2];
            returnarray[0] = true;
            returnarray[1] = true;
            LayerSnapshot[] snapshots2 = result2.snapshots;
            for (int index = 0; index < snapshots2.Length; index++)
            {
                for (int i = 0; i < snapshots2[index].output.Length; i++)
                {
                    if (snapshots[index].output[i] != snapshots2[index].output[i])
                        returnarray[index] = false;
                }

            }
            return returnarray;
        }
    }
}
