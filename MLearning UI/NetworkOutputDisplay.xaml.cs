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
    /// Interaction logic for NetworkOutputDisplay.xaml
    /// </summary>
    public partial class NetworkOutputDisplay : UserControl
    {
        private Ellipse[] circles = new Ellipse[10];

        private readonly static SolidColorBrush DefaultBrush = new SolidColorBrush()
        {
            Color = Color.FromRgb(200, 200, 200)
        };

        public NetworkOutputDisplay()
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
                System.Windows.Controls.Grid.SetRow(ellipse, index);
                System.Windows.Controls.Grid.SetColumn(ellipse, 1);
                ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                ellipse.Width = 28;
                ellipse.Height = 28;
                Grid.SetRow(this, index);
                circles[index] = ellipse;
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
    }
}
