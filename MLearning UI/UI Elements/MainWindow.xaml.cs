using MLearning_UI.Network_Elements;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<NetworkButtonPanel, NeuralNetwork> networks = new Dictionary<NetworkButtonPanel, NeuralNetwork>();
        private Dictionary<NeuralNetwork, NetworkInformationPanel> networkInfoPanels = new Dictionary<NeuralNetwork, NetworkInformationPanel>();
        public NeuralNetwork SelectedNetwork = null;
        public NetworkButtonPanel SelectedButton = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        public DigitImage CurrentImage { get; set; }
        public DigitImage[] Images { get; set; }

        private void RunButtonClicked(object sender, RoutedEventArgs e)
        {
            double[] inputActivations = new double[SelectedNetwork.Properties.Size.InputLayerLength];
            for (int index = 0; index < inputActivations.Length; index++)
            {
                inputActivations[index] = CurrentImage.GetValueAt(index / 28, index % 28) / 256.0;
            }
            NetworkResult result = SelectedNetwork.Run(inputActivations);
            double[] output = result.Output;
            //SetOutputShades(output);
        }

        private void NetworkAddButton_Click(object sender, RoutedEventArgs e)
        {
            void propsDelegate(NetworkProperties props)
            {
                AddNetwork(props);
            }
            AddNetworkWindow addWindow = null;
            addWindow = new AddNetworkWindow(propsDelegate);
            addWindow.ShowDialog();
        }

        private void NetworkDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteNetworkConfirmationWindow window = null;
            void closer() => window.Close();
            void confirmer()
            {
                DeleteNetwork();
                window.Close();
            }
            window = new DeleteNetworkConfirmationWindow(closer, confirmer);
            window.ShowDialog();
        }

        private void DeleteNetwork()
        {
            NetworksListPanel.Children.Remove(SelectedButton);
            networks.Remove(SelectedButton);
            NetworkUnselected();
        }

        private void TrainNetwork()
        {
            SelectedNetwork.Train();
        }

        private void NetworkSelected(NetworkButtonPanel networkPanel)
        {
            NeuralNetwork network = networks[networkPanel];
            NetworkUnselected();
            NetworkInformationPlaceholder.Children.Add(networkInfoPanels[network]);
            NetworkDeleteButton.IsEnabled = true;
            networkPanel.Background = new SolidColorBrush()
            {
                Color = Color.FromRgb(150, 150, 150)
            };
        }

        private void NetworkUnselected()
        {
            SelectedNetwork = null;
            NetworkDeleteButton.IsEnabled = false;
            NetworkInformationPlaceholder.Children.Clear();
            if (SelectedButton != null)
                SelectedButton.Background = new SolidColorBrush()
                {
                    Color = Color.FromRgb(255, 255, 255)
                };
            SelectedButton = null;
        }

        private void AddNetwork(NetworkProperties props)
        {
            NeuralNetwork network = new NeuralNetwork(props);
            NetworkButtonPanel networkButtonPanel = new NetworkButtonPanel(props);
            networks.Add(networkButtonPanel, network);
            networkButtonPanel.Click += delegate (object sender2, RoutedEventArgs e)
            {
                NetworkUnselected();
                SelectedNetwork = network;
                NetworkSelected(networkButtonPanel);
                SelectedButton = networkButtonPanel;
            };
            void trainDelegate() => TrainNetwork();
            NetworkInformationPanel infoPanel = new NetworkInformationPanel(network, trainDelegate);
            networkInfoPanels.Add(network, infoPanel);
            NetworksListPanel.Children.Add(networkButtonPanel);
            DockPanel.SetDock(networkButtonPanel, Dock.Top);
        }
    }
}
