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
        public DigitImage[] images;

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadImages();
            int index = 301;
            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    ((MainWindow)this.MainWindow).setInputShade(y, x, images[index].GetValueAt(y, x));
                }
            }
            ((MainWindow)this.MainWindow).setLabel(images[index].label);
        }

        public class DigitImage
        {
            public byte[,] contents;
            public byte label;

            public DigitImage(byte[,] contents, byte label)
            {
                this.contents = contents;
                this.label = label;
            }

            public byte GetValueAt(int y, int x)
            {
                return contents[y,x];
            }

            public byte GetLabel()
            {
                return label;
            }
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
    }
}