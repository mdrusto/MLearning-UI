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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void setInputShade(int y, int x, byte value)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(value, value, value);
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = brush;
            Grid.SetRow(rectangle, y);
            Grid.SetColumn(rectangle, x);
            rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rectangle.Width = 10;
            rectangle.Height = 10;
            Panel.SetZIndex(rectangle, 0);
            NetworkInputDisplay.Children.Add(rectangle);
        }

        public void setLabel(byte label)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(128, 128, 128);
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = brush;
            Grid.SetRow(ellipse, label);
            Grid.SetColumn(ellipse, 1);
            ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ellipse.Width = 28;
            ellipse.Height = 28;
            NetworkOutputDisplay.Children.Add(ellipse);
        }
    }
}
