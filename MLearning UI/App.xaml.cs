using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace MLearning_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DigitImage[] images;
        private NeuralNetwork network = new NeuralNetwork(new NetworkSize(784, new int[] {16}, 10));
        private bool started = false;

        protected override void OnActivated(EventArgs e)
        {
            if (started)
                return;
            base.OnActivated(e);
            LoadImages();
            int index = 50963;
            MainWindow window = (MainWindow)this.MainWindow;
            window.SetImage(images[index]);
            window.SetLabel(images[index].label);
            window.Network = network;
            window.CurrentImage = images[index];
            window.Images = images;
            network.Initialize(10, new Random());
            started = true;
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

        private void TrainNetwork(NeuralNetwork network)
        {

        }
    }
}