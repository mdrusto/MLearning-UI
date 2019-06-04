using MLearning_UI.Network_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MLearning_UI.UI_Elements.AddNetworkWindow
{

    public partial class AddNetworkWindow : Window
    {
        public delegate void CreateDelegate(NetworkProperties props);
        private CreateDelegate CreateDele;

        private NetworkProperties CurrentProperties => new NetworkProperties
        {
            Size = new NetworkSize(
                    InputLayerDisplay.LayerLength,
                    (from x in internalLayerDisplays select x.LayerLength).ToArray(),
                    OutputLayerDisplay.LayerLength),
            IsInitialized = InitCheckbox.IsChecked.Value,
            InitializationBound = IsInitialized ? double.Parse(InitBoundTextbox.Text) : (double?) null,
            Name = NetworkName.Text
        };

        private List<string> usedNames;

        private List<InternalLayerDisplay> internalLayerDisplays = new List<InternalLayerDisplay>(0);
        private List<AddInternalLayerButton> addLayerButtons = new List<AddInternalLayerButton>();

        public AddNetworkWindow(CreateDelegate createDele, List<string> usedNames)
        {
            InitializeComponent();
            CreateDele = createDele;
            this.usedNames = usedNames;
            InputLayerDisplay.LayerLength = 784;
            OutputLayerDisplay.LayerLength = 10;
            AddAddInternalLayerButton(0);

            AddInternalLayer(0);

            NetworkName.Text = $"Network {usedNames.Count + 1}";
        }

        private void AddInternalLayer(int position)
        {
            AddInternalLayer(position, 1);
        }

        private void AddInternalLayer(int position, int length) // To the left of current layer at this position
        {
            internalLayerDisplays.GetRange(position, internalLayerDisplays.Count - position).ForEach((x) => x.Position++);
            
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
            addLayerButtons.GetRange(pos, internalLayerDisplays.Count - pos).ForEach((x) => x.Position++);

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
            AddInternalLayerButton button = addLayerButtons[position + 1];
            internalLayerDisplays.Remove(display);
            addLayerButtons.Remove(button);

            InternalLayersPanel.Children.Remove(display);
            InternalLayersPanel.Children.Remove(button);

            internalLayerDisplays.GetRange(position, internalLayerDisplays.Count - position).ForEach(x => x.Position--);
            if (addLayerButtons.Count > position + 1)
                addLayerButtons.GetRange(position + 1, addLayerButtons.Count - position).ForEach(x => x.Position--);
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

        private void NetworkName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (NetworkName == null || usedNames == null) return;
            bool isValidName = !usedNames.Contains(NetworkName.Text);
            NameUsedLabel.Visibility = isValidName ? Visibility.Hidden : Visibility.Visible;
            CreateButton.IsEnabled = isValidName;
        }

        private void InitBoundTextbox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(InitBoundTextbox.Text, out double x))
            {
                e.Handled = true;
            }
        }

        private void InitCheckbox_Checked(object sender, EventArgs e)
        {
            if (InitBoundTextbox != null)
            {
                InitBoundTextbox.IsEnabled = true;
                InitBoundLabel.IsEnabled = true;
            }
        }

        private void InitCheckbox_Unchecked(object sender, EventArgs e)
        {
            InitBoundTextbox.IsEnabled = false;
            InitBoundLabel.IsEnabled = false;
        }

        private void InitBoundTextbox_GotMouseCapture(object sender, EventArgs e)
        {
            InitBoundTextbox.SelectAll();
        }

        private void InitBoundTextbox_LostMouseCapture(object sender, EventArgs e)
        {
            InitBoundTextbox.Select(0, 0);
        }
    }
}
