using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspPiPlay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static SPIclass spi = new SPIclass();
        public List<NamedColor> ColorsNamed = new List<NamedColor>();
        public Apa102 LedStrip = new Apa102();

        public MainPage()
        {
            this.InitializeComponent();

            spi.StartSPI();

            int i = 0;
            foreach (var color in typeof(Colors).GetRuntimeProperties())
            {
                ColorsNamed.Add(new NamedColor() { Name = color.Name, Color = (Color)color.GetValue(null) });
                colorlistbox.Items.Add(ColorsNamed[i]);
                i++;
            }

           
        }

        private void btnColorSender_Click(object sender, RoutedEventArgs e)
        {
            Color getcolor = ColorsNamed[colorlistbox.SelectedIndex].Color;

            LedStrip.AllOneColor(getcolor);
        }

        private void btnFader_Click(object sender, RoutedEventArgs e)
        {
            Color getcolor = ColorsNamed[colorlistbox.SelectedIndex].Color;
            LedStrip.FadeLedString(getcolor);
        }

        private void btnRainbow_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.Rainbow_Click();
        }

        private void btnCFader_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.ColorsFade();
        }

        private void btnTwinkle_Click(object sender, RoutedEventArgs e)
        {
            Color getcolor = ColorsNamed[colorlistbox.SelectedIndex].Color;
            LedStrip.Twinkle(getcolor);
        }

        private void btnCylon_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.cyclon();
        }

        private void btnColorShift_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.ColorShift();
        }

        private void btnStopAnimation_Click(object sender, RoutedEventArgs e)
        {
            Apa102.ColorFadeRun = false;
            Apa102.CyclonRun = false;
            Apa102.fadeRun = false;
            Apa102.TwinkleRun = false;
        }
    }

    public class NamedColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
