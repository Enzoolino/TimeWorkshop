using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeLibrary;

namespace TimeWorkshopDesktopApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for ClockView.xaml
    /// </summary>
    public partial class ClockView : UserControl
    {
        private Time currentTime;
        private DateTime localTime;
        private DispatcherTimer timer;


        public ClockView()
        {
            InitializeComponent();

            //Get the current local time
            localTime = DateTime.Now;

            //Create new instance of Time
            currentTime = new Time(localTime.Hour, localTime.Minute, localTime.Second);

            // Set up the DispatcherTimer to update the clock every second
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            
            //Subscribe to the Loaded event of the ClockView
            Loaded += ClockView_Loaded;

            //Stop the DispatcherTime when the view is unloaded
            Unloaded += ClockView_Unloaded;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Add one second
            currentTime = currentTime + new TimePeriod(0, 0, 1);

            //Set TextBlock to current Time
            txtClock.Text = currentTime.ToString();

        }

        
        
        private void ClockView_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the DispatcherTimer when the ClockView is loaded
            //timer.Start();
            // Calculate the time until the next second
            var nextSecond = localTime.AddSeconds(1);
            var delay = nextSecond - DateTime.Now;

            // Start the DispatcherTimer with the initial delay
            timer.Interval = delay;
            timer.IsEnabled = true;
        }

        private void ClockView_Unloaded(object sender, RoutedEventArgs e)
        {
            //Stop the DispatcherTimer when the view is unloaded
            timer.IsEnabled = false;
        }







    }
}
