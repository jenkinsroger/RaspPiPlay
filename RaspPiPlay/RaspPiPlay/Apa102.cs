using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace RaspPiPlay
{
    public class Apa102
    {
        List<Color> pixelColors = new List<Color>();

        public void AllOneColor(Color Color)
        {
            pixelColors = new List<Color>();

            for (int i = 0; i < MainPage.spi.PixelCount; i++) 
            {
                pixelColors.Add(Color.FromArgb(60, Color.R,Color.G,Color.B));
            }

            SPIclass.SendPixels(pixelColors);
        }
    }
}
