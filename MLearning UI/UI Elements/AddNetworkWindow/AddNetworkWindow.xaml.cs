using MLearning_UI.Network_Elements;
using System.Collections.Generic;
using System.Windows;

namespace MLearning_UI.UI_Elements.AddNetworkWindow
{

    public partial class AddNetworkWindow : Window
    {
        public delegate void CreateDelegate(NetworkProperties props);
        private CreateDelegate CreateDele;


        private List<int> internalLayerLengths = new List<int>();

        private NetworkProperties CurrentProperties
        {
            get => new NetworkProperties
            {
                Size = new NetworkSize(InputLayerDisplay.LayerLength, internalLayerLengths.ToArray(), OutputLayerDisplay.LayerLength),
                IsInitialized = InitializeCheckbox.IsChecked.Value,
                Name = NetworkName.Text
            };
        }



        private List<InternalLayerDisplay> internalLayerDisplays = new List<InternalLayerDisplay>(0);

        private List<AddInternalLayerButton> addLayerButtons = new List<AddInternalLayerButton>();

        public AddNetworkWindow(CreateDelegate createDele)
        {
            InitializeComponent();
            CreateDele = createDele;
            InputLayerDisplay.LayerLength = 784;
            OutputLayerDisplay.LayerLength = 10;
            AddAddInternalLayerButton(0);

            AddInternalLayer(0);
        }

        private void AddInternalLayer(int position)
        {
            AddInternalLayer(position, 1);
        }

        private void AddInternalLayer(int position, int length) // To the left of current layer at this position
        {
            internalLayerDisplays.GetRange(position, internalLayerLengths.Count - position).ForEach((x) => x.Position++);

            internalLayerLengths.Insert(position, 0);

            InternalLayerDisplay display = new InternalLayerDisplay(DeleteInternalLayer, position, length);

            // Every layer has an AddLayerButton after it, so multiply by 2
            // Also add 1 to account for the initial Add button
            int positionToPlaceLayerDisplay = position * 2 + 1;

            if (positionToPlaceLayerDisplay == InternalLayersPanel.Children.Count)
            {
                InternalLayersPanel.Children.Add(display);
                internalLayerDisplays.Add(display);
            }
            else
            {
                InternalLayersPanel.Children.Insert(positionToPlaceLayerDisplay, display);
                internalLayerDisplays.Insert(position, display);
            }

            AddAddInternalLayerButton(position + 1); // Button to create a layer at the NEXT index
        }

        private void AddAddInternalLayerButton(int pos)
        {
            addLayerButtons.GetRange(pos, internalLayerLengths.Count - pos).ForEach((x) => x.Position++);

            int positionToPlaceAddButton = pos * 2;

            AddInternalLayerButton button = new AddInternalLayerButton(AddInternalLayer, pos);
            if (positionToPlaceAddButton == InternalLayersPanel.Children.Count)
            {
                InternalLayersPanel.Children.Add(button);
                addLayerButtons.Add(button);
            }
            else
            {
                InternalLayersPanel.Children.Insert(positionToPlaceAddButton, button);
                addLayerButtons.Insert(pos, button);
            }
        }

        private void DeleteInternalLayer(int position)
        {
            InternalLayerDisplay display = internalLayerDisplays[position];
            AddInternalLayerButton button = addLayerButtons[position];
            internalLayerDisplays.Remove(display);
            addLayerButtons.Remove(button);

            InternalLayersPanel.Children.Remove(display);
            InternalLayersPanel.Children.Remove(button);

            internalLayerDisplays.GetRange(position + 1, internalLayerLengths.Count - position - 1).ForEach((x) => x.Position--);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {   
            CreateDele(CurrentProperties);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
