using MLearning_UI.Network_Elements;
using System.Windows;
using System.Windows.Controls;

namespace MLearning_UI.UI_Elements
{
    public partial class NetworkInformationPanel : UserControl
    {
        public delegate void TrainNetworkDelegate();
        private TrainNetworkDelegate trainNetworkDelegate;
        private NeuralNetwork network;

        public NetworkInformationPanel(NeuralNetwork network, TrainNetworkDelegate trainNetworkDelegate)
        {
            InitializeComponent();
            this.network = network;
            this.trainNetworkDelegate = trainNetworkDelegate;
            NetworkName.Content = network.Properties.Name;
        }

        private void TrainButton_Clicked(object sender, RoutedEventArgs e)
        {
            trainNetworkDelegate();
        }
    }
}
