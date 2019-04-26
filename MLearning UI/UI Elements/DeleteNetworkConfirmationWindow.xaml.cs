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

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for DeleteNetworkConfirmationWindow.xaml
    /// </summary>
    public partial class DeleteNetworkConfirmationWindow : Window
    {
        private CloseConfirmationWindow closer;
        private ConfirmDeleteNetwork confirmer;

        public DeleteNetworkConfirmationWindow(CloseConfirmationWindow closer, ConfirmDeleteNetwork confirmer)
        {
            InitializeComponent();
            this.closer = closer;
            this.confirmer = confirmer;
        }

        public delegate void CloseConfirmationWindow();
        public delegate void ConfirmDeleteNetwork();

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            closer();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            confirmer();
        }
    }
}
