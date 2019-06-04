using MLearning_UI.Network_Elements;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for NetworkInputDisplay.xaml
    /// </summary>
    public partial class NetworkInputDisplay : UserControl
    {
        public NetworkInputDisplay()
        {
            InitializeComponent();
        }

        private void SetInputShade(int y, int x, byte value)
        {
            SolidColorBrush brush = new SolidColorBrush
            {
                Color = Color.FromRgb((byte)(255 - value), (byte)(255 - value), (byte)(255 - value))
            };
            Rectangle rectangle = new Rectangle
            {
                Fill = brush,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Width = 10,
                Height = 10
            };
            Grid.SetRow(rectangle, y);
            Grid.SetColumn(rectangle, x);
            MainGrid.Children.Add(rectangle);
        }

        public void SetImage(DigitImage image)
        {
            MainGrid.Children.Clear();
            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    SetInputShade(y, x, image.GetValueAt(y, x));
                }
            }
        }
    }
}
