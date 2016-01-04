using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
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

            //var dialog = new MessageDialog(ColorsNamed[colorlistbox.SelectedIndex].Color.ToString());
            //await dialog.ShowAsync();

            LedStrip.AllOneColor(getcolor);
        }
    }

    public class NamedColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
