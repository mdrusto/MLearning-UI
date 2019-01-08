using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MLearning_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ellipse[] circles = new Ellipse[10];

        private readonly static SolidColorBrush DefaultBrush = new SolidColorBrush()
        {
            Color = Color.FromRgb(200, 200, 200)
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            for (int index = 0; index < 10; index++)
            {
                Ellipse ellipse = new Ellipse
                {
                    Fill = DefaultBrush.Clone()
                };
                Grid.SetRow(ellipse, index);
                Grid.SetColumn(ellipse, 1);
                ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                ellipse.Width = 28;
                ellipse.Height = 28;
                NetworkOutputDisplay.Children.Add(ellipse);
                circles[index] = ellipse;
            }
        }

        public void SetInputShade(int y, int x, byte value)
        {
            SolidColorBrush brush = new SolidColorBrush
            {
                Color = Color.FromRgb((byte)(255 - value), (byte)(255 - value), (byte)(255 - value))
            };
            Rectangle rectangle = new Rectangle
            {
                Fill = brush
            };
            Grid.SetRow(rectangle, y);
            Grid.SetColumn(rectangle, x);
            rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rectangle.Width = 10;
            rectangle.Height = 10;
            Panel.SetZIndex(rectangle, 0);
            NetworkInputDisplay.Children.Add(rectangle);
        }

        public void SetImage(DigitImage image)
        {
            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    SetInputShade(y, x, image.GetValueAt(y, x));
                }
            }
        }

        public void SetLabel(byte label)
        {
            for (int index = 0; index < circles.Length; index++)
            {
                circles[index].Fill = DefaultBrush.Clone();
            }
            ((SolidColorBrush)circles[label].Fill).Color = Color.FromRgb(255, 0, 0);
        }

        public void SetOutputShades(double[] output)
        {
            for (int index = 0; index < output.Length; index++)
            {
                ((SolidColorBrush)circles[index].Fill).Color = Color.FromRgb((byte)(256 * output[index]), 0, 0);
            }
        }
    
        public NeuralNetwork Network { get; set; }
        public DigitImage CurrentImage { get; set; }
        public DigitImage[] Images { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double[] inputActivations = new double[Network.Size.InputLayerLength];
            for (int index = 0; index < inputActivations.Length; index++)
            {
                inputActivations[index] = CurrentImage.GetValueAt(index / 28, index % 28) / 256.0;
            }
            NetworkResult result = Network.Run(inputActivations);
            double[] output = result.Output;
            SetOutputShades(output);
            
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SetImage(Images[Int32.Parse(IndexBox.Text)]);
            SetLabel(Images[Int32.Parse(IndexBox.Text)].label);
            CurrentImage = Images[Int32.Parse(IndexBox.Text)];
        }
    }
}
