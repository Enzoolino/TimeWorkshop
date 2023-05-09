using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for TimeView.xaml
    /// </summary>
    public partial class TimeView : UserControl
    {
        public TimeView()
        {
            InitializeComponent();
        }



        
        private bool confirmedPrimary = false;

        private bool confirmedOperationalTime = false;
        private bool confirmedOperationalTimePeriod = false;

        private Time timePrimary = new Time();
        private Time timeOperational = new Time();
        private TimePeriod timePeriodOperational = new TimePeriod();



        private void PrimaryConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timePrimary = new Time(txtPrimary.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Converting input into 'Time' value failed." +
                    "Make sure value you are trying to convert is in format 00:00:00.");
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Entered Time values are incorrect!");
            }

            catch (ArgumentException)
            {
                MessageBox.Show("Converting input into 'Time' value failed." +
                        "Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.");
            }
            finally
            {
                timePrimary = new Time(txtPrimary.Text);
                confirmedPrimary = true;
            }

            if (confirmedPrimary && confirmedOperationalTime)
            {
                if (timePrimary == timeOperational)
                    txtEqual.Text = "True";
                else txtEqual.Text = "False";

                if (timePrimary != timeOperational)
                    txtNotEqual.Text = "True";
                else txtNotEqual.Text = "False";

                if (timePrimary < timeOperational)
                    txtSmaller.Text = "True";
                else txtSmaller.Text = "False";

                if (timePrimary > timeOperational)
                    txtBigger.Text = "True";
                else txtBigger.Text = "False";

                if (timePrimary <= timeOperational)
                    txtSmallerOrEqual.Text = "True";
                else txtSmallerOrEqual.Text = "False";

                if (timePrimary >= timeOperational)
                    txtBiggerOrEqual.Text = "True";
                else txtBiggerOrEqual.Text = "False";
            }
            
        }

        private void OperationalConfirmButton_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (TimePeriodCheckBox.IsChecked == true)
                    timePeriodOperational = new TimePeriod(txtPrimary.Text);
                else if (TimePeriodCheckBox.IsChecked == false)
                    timeOperational = new Time(txtPrimary.Text);

            }
            catch (FormatException)
            {
                MessageBox.Show("Converting input into 'Time' value failed." +
                    "Make sure value you are trying to convert is in format 00:00:00.");
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Entered Time values are incorrect!");
            }

            catch (ArgumentException)
            {
                MessageBox.Show("Converting input into 'Time' value failed." +
                        "Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.");
            }
            finally
            {
                if (TimePeriodCheckBox.IsChecked == true)
                {
                    timePeriodOperational = new TimePeriod(txtPrimary.Text);
                    confirmedOperationalTimePeriod = true;
                }
                else if (TimePeriodCheckBox.IsChecked == false)
                {
                    timeOperational = new Time(txtPrimary.Text);
                    confirmedOperationalTime = true;
                } 
            }


            if (confirmedPrimary && confirmedOperationalTime)
            {
                if (timePrimary == timeOperational)
                    txtEqual.Text = "True";
                else txtEqual.Text = "False";

                if (timePrimary != timeOperational)
                    txtNotEqual.Text = "True";
                else txtNotEqual.Text = "False";

                if (timePrimary < timeOperational)
                    txtSmaller.Text = "True";
                else txtSmaller.Text = "False";

                if (timePrimary > timeOperational)
                    txtBigger.Text = "True";
                else txtBigger.Text = "False";

                if (timePrimary <= timeOperational)
                    txtSmallerOrEqual.Text = "True";
                else txtSmallerOrEqual.Text = "False";

                if (timePrimary >= timeOperational)
                    txtBiggerOrEqual.Text = "True";
                else txtBiggerOrEqual.Text = "False";
            }
            else if (confirmedPrimary && confirmedOperationalTimePeriod)
            {
                Time Added = timePrimary + timePeriodOperational;
                Time Subtracted = timePrimary - timePeriodOperational;

                txtAdd.Text = Added.ToString();
                txtSubtract.Text = Subtracted.ToString();
            
            }

        }

    }
}
