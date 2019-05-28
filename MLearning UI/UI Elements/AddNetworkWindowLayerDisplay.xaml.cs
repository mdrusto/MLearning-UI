using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for AddNetworkWindowLayerDisplay.xaml
    /// </summary>
    public partial class AddNetworkWindowLayerDisplay : DockPanel
    {
        public int LayerLength
        {
            get => int.Parse(LayerLengthTextBox.Text);
            set
            {
                if (value > 0)
                    LayerLengthTextBox.Text = value.ToString();
            }
        }

        public AddNetworkWindowLayerDisplay()
        {
            InitializeComponent();
        }

        private void UpdateCircles(int newLength)
        {
            int numberToAdd = newLength - EllipseContainer.Children.Count;
            if (numberToAdd > 0)
            {
                for (int i = 0; i < numberToAdd; i++)
                {
                    EllipseContainer.Children.Add(new Ellipse
                    {
                        Width = 20,
                        Height = 20,
                        Fill = new SolidColorBrush
                        {
                            Color = Color.FromRgb(0, 0, 0)
                        }
                    });
                }
            }
            else if (numberToAdd < 0)
            {
                for (int i = 0; i < -numberToAdd; i++)
                {
                    EllipseContainer.Children.RemoveAt(EllipseContainer.Children.Count - 1);
                }
            }
            DecreaseLengthButton.IsEnabled = (newLength > 1);
        }

        private void IncreaseLengthButton_Click(object sender, RoutedEventArgs e)
        {
            LayerLength++;
        }

        private void DecreaseLengthButton_Click(object sender, RoutedEventArgs e)
        {
            LayerLength--;
        }

        private void LayerLengthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(LayerLengthTextBox.Text, out int newLength) && newLength > 0)
                UpdateCircles(int.Parse(LayerLengthTextBox.Text));
        }
    }
}
