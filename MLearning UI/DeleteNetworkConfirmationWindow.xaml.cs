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
using System.Windows.Shapes;

namespace MLearning_UI
{
    /// <summary>
    /// Interaction logic for DeleteNetworkConfirmationWindow.xaml
    /// </summary>
    public partial class DeleteNetworkConfirmationWindow : Window
    {
        private CloseConfirmationWindow closer;

        public DeleteNetworkConfirmationWindow(CloseConfirmationWindow closer)
        {
            InitializeComponent();
            this.closer = closer;
        }

        public delegate void CloseConfirmationWindow();

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            closer();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
