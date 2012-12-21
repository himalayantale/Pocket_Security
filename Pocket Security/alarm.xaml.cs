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

namespace Pocket_Security
{
    public partial class alarm : PhoneApplicationPage
    {
        public alarm()
        {
            InitializeComponent();
            mediaElement1.Stop();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }
    }
}