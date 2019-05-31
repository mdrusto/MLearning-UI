using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MLearning_UI.UI_Elements.AddNetworkWindow
{
    /// <summary>
    /// Interaction logic for AddNetworkWindowInternalLayerDisplay.xaml
    /// </summary>
    public partial class AddInternalLayerButton : DockPanel
    {
        public delegate void AddInternalLayerDelegate(int position);
        private AddInternalLayerDelegate dele;

        public int Position { get; internal set; }

        public AddInternalLayerButton(AddInternalLayerDelegate dele, int pos)
        {
            InitializeComponent();
            this.dele = dele;
            this.Position = pos;
        }

        private void AddInternalLayerButton_Click(object sender, RoutedEventArgs e)
        {
            dele(Position);
        }

        private void MouseEntered(object sender, MouseEventArgs e)
        {
            Opacity = 1.0;
        }

        private void MouseLeft(object sender, MouseEventArgs e)
        {
            Opacity = 0.0;
        }
    }
}
