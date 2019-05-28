using MLearning_UI.Network_Elements;
using System;
using System.Windows;

namespace MLearning_UI.UI_Elements
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool started = false;

        public static readonly double WINDOW_TITLE_HEIGHT = SystemParameters.WindowCaptionHeight + SystemParameters.ResizeFrameHorizontalBorderHeight;
        public static readonly double WINDOW_BORDER_WIDTH = SystemParameters.ResizeFrameVerticalBorderWidth;

        protected override void OnActivated(EventArgs e)
        {
            if (started)
                return;
            base.OnActivated(e);
            MainWindow window = (MainWindow)this.MainWindow;
            started = true;
        }
    }
}