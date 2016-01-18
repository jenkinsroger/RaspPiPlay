using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
using Sensors.Dht;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RaspPiPlay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeScreen : Page
    {
        public static HomeScreen _main;
        private IDht _dht = null;
        private DispatcherTimer _timer = new DispatcherTimer();
        DhtReading reading;// = new DhtReading();


        public HomeScreen()
        {
            this.InitializeComponent();
            _main = this;

            var _pin = GpioController.GetDefault().OpenPin(4, GpioSharingMode.Exclusive);
            _dht = new Dht11(_pin, GpioPinDriveMode.Input);
            

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;


            GetNistTime();
            getinitreadingtemp();
        }
        public void GetNistTime()
        {

        }

        private async void getinitreadingtemp()
        {
            _timer.Start();
        }

        private async void _timer_Tick(object sender, object e)
        {

            reading = new DhtReading();
            reading = await _dht.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                txtTemp.Text = (reading.Temperature).ToString() + " °C" + "       " + (ConvertTemp.ConvertCelsiusToFahrenheit(reading.Temperature).ToString() + " °F");
            }
            else
            {

            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));

        }
    }


}
