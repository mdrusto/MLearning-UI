using MLearning_UI.Network_Elements;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MLearning_UI.UI_Elements
{
    public partial class NetworkRunDisplayPanel : UserControl
    {
        public delegate NetworkResult RunNetwork(double[] inputActivations);

        private DigitImage[] images;
        private DigitImage currentImage;
        public DigitImage CurrentImage
        {
            get => currentImage;
            set
            {
                InputDisplay.SetImage(value);
                currentImage = value;
            }
        }

        public double[] ImageAsDoubleArray { get; private set; }

        private RunNetwork runNetworkDelegate;
        private RunNetwork RunNetworkDelegate
        {
            get => runNetworkDelegate;
            set
            {
                RunButton.IsEnabled = (value != null);
                runNetworkDelegate = value;
            }
        }

        public NetworkRunDisplayPanel()
        {
            InitializeComponent();
            LoadImages();
        }

        public void WithDelegate(RunNetwork runNetworkDelegate)
        {
            this.RunNetworkDelegate = runNetworkDelegate;
        }

        public void LoadImages()
        {
            images = new DigitImage[60000];
            byte[] imageFileContents = File.ReadAllBytes("digits/train-images.idx3-ubyte");
            byte[] labelFileContents = File.ReadAllBytes("digits/train-labels.idx1-ubyte");
            for (int counter = 0; counter < 60000; counter++)
            {

                byte label = labelFileContents[counter + 8];

                byte[,] imageContents = new byte[28, 28];
                for (int pixelCounterX = 0; pixelCounterX < 28; pixelCounterX++)
                {
                    for (int pixelCounterY = 0; pixelCounterY < 28; pixelCounterY++)
                    {
                        imageContents[pixelCounterY, pixelCounterX] = imageFileContents[16 + 784 * counter + pixelCounterY * 28 + pixelCounterX];
                    }
                }
                images[counter] = new DigitImage(imageContents, label);
            }
        }

        private void RunButton_Clicked(object sender, RoutedEventArgs e)
        {
            NetworkResult result = RunNetworkDelegate(ImageAsDoubleArray);
            OutputDisplay.SetOutputShades(result.Output);
        }

        private void LoadNewImageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadRandomImage();
        }

        public void LoadRandomImage()
        {
            CurrentImage = images[new Random().Next(images.Length)];
        }
    }
}
