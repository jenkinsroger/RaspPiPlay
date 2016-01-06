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

        public void FadeLedString(Color color)
        {
            Task.Run(() =>
            {
                int Brightness = 1;
                int CurrentCycle = 0;
                int NumberOfCycles = 10;

                while (CurrentCycle < NumberOfCycles)
                {

                    while (Brightness < 126)
                    {
                        pixelColors = new List<Color>();

                        for (int i = 0; i < MainPage.spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, color.R, color.G, color.B));
                        }

                        SPIclass.SendPixels(pixelColors);
                        Brightness += 5;
                        Task.Delay(30).Wait();
                    }

                    while (Brightness > 1)
                    {
                        pixelColors = new List<Color>();

                        for (int i = 0; i < MainPage.spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, color.R, color.G, color.B));
                        }

                        SPIclass.SendPixels(pixelColors);
                        Brightness -= 5;
                        Task.Delay(30).Wait();
                    }

                    CurrentCycle++;
                }
            });
        }

        public void Rainbow_Click()
        {
            List<Color> pixelColors = new List<Color>();

            for (int i = 0; i < MainPage.spi.PixelCount / 6; i++)
            {
                pixelColors.Add(Color.FromArgb(60, 255, 0, 0));     // Red
                pixelColors.Add(Color.FromArgb(60, 0, 255, 0));     // Green
                pixelColors.Add(Color.FromArgb(60, 0, 0, 255));     // Blue
                pixelColors.Add(Color.FromArgb(60, 255, 255, 0));     // Yellow
                pixelColors.Add(Color.FromArgb(60, 255, 0, 255));     // pink
                pixelColors.Add(Color.FromArgb(60, 0, 255, 255));     // cyan

            }

            SPIclass.SendPixels(pixelColors);
        }

        public void Twinkle(Color color)
        {
            Task.Run(() =>
            {
                while(true)
                {
                    Random Brightness = new Random();
                    pixelColors = new List<Color>();

                    for (int i = 0; i < MainPage.spi.PixelCount; i++)
                    {
                        pixelColors.Add(Color.FromArgb((byte)Brightness.Next(0,255), color.R, color.G, color.B));
                    }

                    SPIclass.SendPixels(pixelColors);
                    Task.Delay(50).Wait();
                }

            });
        }

        public void ColorsFade()
        {
            int Brightness = 60;
            int CurrentCycle = 0;
            int NumberOfCycles = 10;

            int red = 255;
            int green = 0;
            int blue = 0;

            int changeamount = 5;

            Color StartColor = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);

            Task.Run(() =>
            {
                while (true)
                {
                    pixelColors = new List<Color>();

                    for (int i = 0; i < MainPage.spi.PixelCount; i++)
                    {
                        pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue));
                    }
                    SPIclass.SendPixels(pixelColors);

                    if (red == 255 && green < 255)
                    {
                        if (green == 255) break;
                        green = green + changeamount;
                    }
                    else if(red >0 && green ==255)
                    {
                        if (red == 0) break;
                        red = red - changeamount;
                    }
                    else if (green == 255 && blue <255)
                    {
                        if (blue == 255) break;
                        blue = blue + changeamount;
                    }
                    else if(green > 0 && blue == 255)
                    {
                        if (green == 0) break;
                        green = green - changeamount;
                    }
                    else if (red < 255 && blue == 255)
                    {
                        if (red == 255) break;
                        red = red + changeamount;
                    }
                    else if (red == 255 && blue > 0)
                    {
                        if (blue == 0) break;
                        blue = blue - changeamount;
                    }

                    Task.Delay(30).Wait();
                }
            });


        }

    }
}
