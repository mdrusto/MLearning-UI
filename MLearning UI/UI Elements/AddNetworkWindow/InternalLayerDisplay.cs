using System.Windows;
using System.Windows.Controls;

namespace MLearning_UI.UI_Elements.AddNetworkWindow
{
    /// <summary>
    /// Interaction logic for InternalLayerDisplay.xaml
    /// </summary>
    public partial class InternalLayerDisplay : LayerDisplay
    {
        public delegate void DeleteLayer(int position);
        private readonly DeleteLayer deleteLayer;

        public int Position { get; internal set; }

        public InternalLayerDisplay(DeleteLayer deleteLayer, int position, int length) : base()
        {
            InitializeComponent();
            this.deleteLayer = deleteLayer;
            this.Position = position;
            this.LayerLength = length;
            Button deleteLayerButton = new Button()
            {
                Content = "X",
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            deleteLayerButton.Click += DeleteLayerButton_Click;
            DockPanel.SetDock(deleteLayerButton, Dock.Bottom);
            Children.Remove(EllipseContainer);
            Children.Add(deleteLayerButton);
            Children.Add(EllipseContainer);
        }

        private void DeleteLayerButton_Click(object sender, RoutedEventArgs e)
        {
            deleteLayer(Position);
        }
    }
}
