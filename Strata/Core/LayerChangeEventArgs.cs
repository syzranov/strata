using System;

namespace Strata
{
    public class LayerChangeEventArgs : EventArgs
    {
        public LayerChangeEventArgs(Layer layer)
        {
            GetState = layer;
        }

        public Layer GetState { get; private set; }
    }
}