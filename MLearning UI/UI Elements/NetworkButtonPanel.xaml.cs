using MLearning_UI.Network_Elements;
using System.Windows.Controls;
using System.Windows.Media;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for NetworkInfoPanel.xaml
    /// </summary>
    public partial class NetworkButtonPanel : Button
    {
        public NetworkButtonPanel(NetworkProperties props)
        {
            InitializeComponent();
            Height = 80;
            Background = new SolidColorBrush()
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            NetworkName.Content = props.Name;
            AccuracyLabel.Content = props.IsInitialized ? "Initialized" : "Uninitialized";
            AccuracyLabel.Foreground = new SolidColorBrush()
            {
               Color = props.IsInitialized ? Color.FromRgb(200, 255, 200) : Color.FromRgb(100, 100, 100)
            };
            string networkSizeString = props.Size.InputLayerLength + "-";
            foreach (int layerLength in props.Size.InternalLayerLengths)
            {
                networkSizeString += layerLength + "-";
            }
            networkSizeString += props.Size.OutputLayerLength;
            NetworkSizeLabel.Content = networkSizeString;
            VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }
    }
}
