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
        public NeuralNetwork SelectedNetwork = null;
        public NetworkButtonPanel SelectedButton = null;

        public DigitImage CurrentImage
        {
            get => RunDisplayPanel.CurrentImage;
        }

        public MainWindow()
        {
            InitializeComponent();
            ClearNetworkInfoPanels();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            RunDisplayPanel.LoadRandomImage();

        }
        
        private NetworkResult RunSelectedNetwork()
        {
            // This is not in RunDisplay class because RunDisplay should know nothing about the selected network
            double[] inputActivations = CurrentImage.AsDoubleArray;
            return SelectedNetwork.Run(inputActivations);
        }

        private void SetOutputShades(double[] values)
        {
            RunDisplayPanel.OutputDisplay.SetOutputShades(values);
        }

        private void NetworkAddButton_Click(object sender, RoutedEventArgs e)
        {
            void propsDelegate(NetworkProperties props)
            {
                AddNetwork(props);
            }
            AddNetworkWindow.AddNetworkWindow addWindow = null;
            addWindow = new AddNetworkWindow.AddNetworkWindow(propsDelegate);
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

        private void ClearNetworkInfoPanels()
        {
            // Clear Accuracy Panel
            AccuracyPanel.AccuracyPercentage.Visibility = Visibility.Hidden;
            AccuracyPanel.UntestedLabel.Visibility = Visibility.Hidden;
            AccuracyPanel.NoNetworkSelectedLabel.Visibility = Visibility.Visible;

            // Clear Training Panel
            TrainingPanel.NoNetworkSelectedLabel.Visibility = Visibility.Visible;
            TrainingPanel.TrainingOptionsBox.Visibility = Visibility.Hidden;

            // Clear Info Panel
            InfoPanel.NoNetworkSelectedLabel.Visibility = Visibility.Visible;
            InfoPanel.NetworkName.Visibility = Visibility.Hidden;
        }

        private void NetworkSelected(NetworkButtonPanel networkPanel)
        {
            NeuralNetwork network = networks[networkPanel];
            NetworkUnselected();
            SelectedButton = networkPanel;
            SelectedNetwork = network;
            NetworkDeleteButton.IsEnabled = true;
            networkPanel.Background = new SolidColorBrush()
            {
                Color = Color.FromRgb(150, 150, 150)
            };

            // Format Info Panel
            InfoPanel.NetworkName.Content = network.Properties.Name;

            // Format Accuracy Panel
            AccuracyPanel.NoNetworkSelectedLabel.Visibility = Visibility.Hidden;
            double? acc = network.Accuracy;
            AccuracyPanel.UntestedLabel.Visibility = (acc == null) ? Visibility.Hidden : Visibility.Visible;
            AccuracyPanel.AccuracyPercentage.Visibility = (acc != null) ? Visibility.Hidden : Visibility.Visible;
            if (network.Accuracy != null)
            {
                AccuracyPanel.AccuracyPercentage.Content = (int) acc + "." + (int) ((acc % 1) * 10) + "%";
            }

            // Format Training Panel
            TrainingPanel.IsTrainedLabel.Content = "Untrained";
            TrainingPanel.NoNetworkSelectedLabel.Visibility = Visibility.Hidden;
            TrainingPanel.TrainingOptionsBox.Visibility = Visibility.Visible;
            void trainDelegate() => TrainNetwork();
            TrainingPanel.NetworkSelected(trainDelegate);

            //Format Run Display Panel
            NetworkResult runNetwork(double[] inputActivations)
            {
                return RunSelectedNetwork();
            }
            RunDisplayPanel.WithDelegate(runNetwork);
            if (!network.Properties.Size.Equals(new NetworkSize(784, new int[] { 16 }, 10)))
                RunDisplayPanel.RunButton.IsEnabled = false;

            // Format Info Panel
            InfoPanel.NoNetworkSelectedLabel.Visibility = Visibility.Hidden;
            InfoPanel.NetworkName.Visibility = Visibility.Visible;
        }

        private void NetworkUnselected()
        {
            SelectedNetwork = null;
            NetworkDeleteButton.IsEnabled = false;
            if (SelectedButton != null)
                SelectedButton.Background = new SolidColorBrush()
                {
                    Color = Color.FromRgb(255, 255, 255)
                };
            SelectedButton = null;

            ClearNetworkInfoPanels();
        }

        private void AddNetwork(NetworkProperties props)
        {
            NeuralNetwork network = new NeuralNetwork(props);
            NetworkButtonPanel networkButtonPanel = new NetworkButtonPanel(props);
            networks.Add(networkButtonPanel, network);
            networkButtonPanel.Click += delegate (object sender2, RoutedEventArgs e)
            {
                if (network != SelectedNetwork)
                {
                    NetworkUnselected();
                    SelectedNetwork = network;
                    NetworkSelected(networkButtonPanel);
                    SelectedButton = networkButtonPanel;
                }
            };
            NetworksListPanel.Children.Add(networkButtonPanel);
            DockPanel.SetDock(networkButtonPanel, Dock.Top);

            if (props.IsInitialized)
                network.Initialize(10, new Random());

            NetworkSelected(networkButtonPanel);
        }
    }
}
