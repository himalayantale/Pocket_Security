using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone;
using Microsoft.Phone.Tasks;
using System.Device.Location;
using System.Diagnostics;

namespace Pocket_Security
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        string msg;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (watcher == null)
            {
                //Get the highest accuracy.
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High)
                {
                    //The minimum distance (in meters) to travel before the next location update.
                    MovementThreshold = 10
                };
                //Event to fire when a new position is obtained.
                watcher.PositionChanged += new
                EventHandler<GeoPositionChangedEventArgs
                <GeoCoordinate>>(watcher_PositionChanged);

                //Event to fire when there is a status change in the location service API.
                watcher.StatusChanged += new
                EventHandler<GeoPositionStatusChangedEventArgs>
                (watcher_StatusChanged);
                watcher.Start();
                
            }
            // position change code start
            watcher.PositionChanged += this.watcher_PositionChanged;
            watcher.StatusChanged += this.watcher_StatusChanged;
            watcher.Start();
            // position chane code stop
        }

        public void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // location is unsupported on this device
                    Debug.WriteLine("disabled");
                    break;
                case GeoPositionStatus.NoData:
                    // data unavailable
                    Debug.WriteLine("nodata");
                    break;


                case GeoPositionStatus.Initializing:
                    Debug.WriteLine("initializing");
                    break;

                case GeoPositionStatus.Ready:
                    Debug.WriteLine("ready");
                    break;
            }
        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var epl = e.Position.Location;
            string lat =  epl.Latitude.ToString();
            msg = "Latitude:  N " + lat + "° \n";
            string lon=  epl.Longitude.ToString();
            msg += "Longitude:  E " + lon + "° \n";
            msg += "Altitude: " + epl.Altitude.ToString() + " Meters \n"; 
            msg += "Horizontal Accuracy: " + epl.Longitude.ToString() + " Meters \n";
            msg += "Vertical Accuracy: " + epl.Longitude.ToString() + " Meters \n";
                                  
            string heading =  epl.Course.ToString();
            msg += "Heading towards: " + heading + "° N \n";
            string speed = epl.Speed.ToString();
            msg += "Speed: " + speed +"meters per second \n";
            msg += "Time: " + e.Position.Timestamp.LocalDateTime.ToString();
            textBlock1.Text = msg;
          
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string addtomsg = "I am in danger. My current location is: ";
            SmsComposeTask smsComposeTask = new SmsComposeTask();
            smsComposeTask.To = "";
            smsComposeTask.Body = addtomsg +"\n"+ msg;
            smsComposeTask.Show(); 
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var phoneCallTask = new PhoneCallTask
            {
                DisplayName = "Police",
                PhoneNumber = "100"
            };
            phoneCallTask.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/alarm.xaml"), UriKind.Relative));
        }
    }
}
