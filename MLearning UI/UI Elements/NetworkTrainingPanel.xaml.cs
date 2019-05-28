using System.Windows;
using System.Windows.Controls;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for NetworkTrainingPanel.xaml
    /// </summary>
    public partial class NetworkTrainingPanel : UserControl
    {
        public delegate void TrainNetworkDelegate();

        private TrainNetworkDelegate trainNetworkDelegate;

        public NetworkTrainingPanel()
        {
            InitializeComponent();
        }

        public void NetworkSelected(TrainNetworkDelegate trainNetworkDelegate)
        {
            this.trainNetworkDelegate = trainNetworkDelegate;
        }

        private void TrainButton_Clicked(object sender, RoutedEventArgs e)
        {
            trainNetworkDelegate();
        }
    }
}
