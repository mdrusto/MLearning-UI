using MLearning_UI.Network_Elements;
using System.Collections.Generic;
using System.Windows;

namespace MLearning_UI.UI_Elements
{

    public partial class AddNetworkWindow : Window
    {
        public delegate void CreateDelegate(NetworkProperties props);

        private CreateDelegate CreateDele;

        private List<AddNetworkWindowLayerDisplay> internalLayerDisplays = new List<AddNetworkWindowLayerDisplay>(0);

        public AddNetworkWindow(CreateDelegate createDele)
        {
            InitializeComponent();
            CreateDele = createDele;
            void buttonDele()
            {
                AddNetworkWindowLayerDisplay display = new AddNetworkWindowLayerDisplay
                {
                    LayerLength = 1
                };
                InternalLayersPanel.Children.Add(display);
                internalLayerDisplays.Add(display);
                InternalLayersPanel.Children.Add(new AddNetworkWindowAddInternalLayerButton(buttonDele));
            }
            InputLayerDisplay.LayerLength = 784;
            OutputLayerDisplay.LayerLength = 10;
            InternalLayersPanel.Children.Add(new AddNetworkWindowAddInternalLayerButton(buttonDele));
            InternalLayersPanel.Children.Add(new AddNetworkWindowLayerDisplay { LayerLength = 16 });
            InternalLayersPanel.Children.Add(new AddNetworkWindowAddInternalLayerButton(buttonDele));
        }

        private void AddInternalLayer(int position)
        {

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            int[] internalLayerLengths = new int[internalLayerDisplays.Count];
            for (int i = 0; i < internalLayerLengths.Length; i++)
            {
                internalLayerLengths[i] = internalLayerDisplays[i].LayerLength;
            }

            NetworkProperties props = new NetworkProperties
            {
                Name = NetworkName.Text,
                IsInitialized = InitializeCheckbox.IsChecked.Value,
                Size = new NetworkSize(InputLayerDisplay.LayerLength, internalLayerLengths, OutputLayerDisplay.LayerLength)
            };
            CreateDele(props);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
