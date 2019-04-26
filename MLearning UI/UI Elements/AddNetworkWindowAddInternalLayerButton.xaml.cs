using System.Windows;
using System.Windows.Controls;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for AddNetworkWindowInternalLayerDisplay.xaml
    /// </summary>
    public partial class AddNetworkWindowAddInternalLayerButton : DockPanel
    {
        public delegate void AddInternalLayerDelegate();

        private AddInternalLayerDelegate dele;

        public AddNetworkWindowAddInternalLayerButton(AddInternalLayerDelegate dele)
        {
            InitializeComponent();
            this.dele = dele;
        }

        private void AddInternalLayerButton_Click(object sender, RoutedEventArgs e)
        {
            dele();
        }

        
    }
}
