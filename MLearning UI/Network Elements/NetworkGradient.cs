﻿using System;

namespace MLearning_UI.Network_Elements
{
    public class NetworkGradient
    {
        private readonly WeightMatrix[] weightAdditives;
        private readonly BiasVector[] biasAdditives;

        public NetworkGradient(NetworkSize size)
        {
            this.weightAdditives = new WeightMatrix[size.InternalLayerCount - 1];
            this.biasAdditives = new BiasVector[size.InternalLayerCount - 1];
            for (int index = 0; index < weightAdditives.Length; index++)
            {
                weightAdditives[index] = new WeightMatrix(size.InternalLayerLengths[index + 1], size.InternalLayerLengths[index]);
                biasAdditives[index] = new BiasVector(index + 1);
            }
        }

        public void SetWeight(int index, int row, int column, double value)
        {
            weightAdditives[index].SetWeight(row, column, value);
        }

        public void SetBias(int index, int row, double value)
        {
            biasAdditives[index].SetBias(row, value);
        }
    }
}
