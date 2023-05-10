using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        //Bools - Confirmation
        private bool confirmedPrimary = false;
        private bool confirmedOperationalTime = false;
        private bool confirmedOperationalTimePeriod = false;

        //TimeValues
        private Time timePrimary = new Time();
        private Time timeOperational = new Time();
        private TimePeriod timePeriodOperational = new TimePeriod();

        //Bools - Flags
        bool conversionFailedPrimary = false; // initialize a flag to indicate whether a conversion error occurred.
        bool conversionFailedOperational = false; //// initialize a flag to indicate whether a conversion error occurred.


        private void PrimaryConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            
            ToggleButton toggleButton = sender as ToggleButton;

            if (toggleButton.IsChecked == true)
            {
                try
                {
                    timePrimary = new Time(txtPrimary.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                        " Make sure value you are trying to convert is in format 00:00:00.", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedPrimary = true;
                }

                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Entered Time values are incorrect!" +
                        " Remember that these are max values for Time: 23:59:59.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedPrimary= true;
                }

                catch (ArgumentException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                            " Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedPrimary = true;
                }
                finally
                {
                    if (!conversionFailedPrimary)
                    {
                        timePrimary = new Time(txtPrimary.Text);
                        confirmedPrimary = true;
                    }
                    else
                    {
                        toggleButton.IsChecked = false;
                        timePrimary = new Time();
                        confirmedPrimary = false;
                    }
                }
            }
            else
            {
                timePrimary = new Time();
                confirmedPrimary = false;
            }


            if (confirmedPrimary && confirmedOperationalTime)
            {
                TimePeriodCheckBox.IsHitTestVisible = false;

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
                TimePeriodCheckBox.IsHitTestVisible = false;

                Time Added = timePrimary + timePeriodOperational;
                Time Subtracted = timePrimary - timePeriodOperational;

                txtAdd.Text = Added.ToString();
                txtSubtract.Text = Subtracted.ToString();

            }
            else
            {
                TimePeriodCheckBox.IsHitTestVisible = true;

                txtEqual.Text = "Empty";
                txtNotEqual.Text = "Empty";
                txtSmaller.Text = "Empty";
                txtBigger.Text = "Empty";
                txtSmallerOrEqual.Text = "Empty";
                txtBiggerOrEqual.Text = "Empty";
                txtAdd.Text = "Empty";
                txtSubtract.Text = "Empty";
            }    
        }

        private void OperationalConfirmButton_Click(Object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;

            if (toggleButton.IsChecked == true)
            {
                try
                {
                    if (TimePeriodCheckBox.IsChecked == true)
                        timePeriodOperational = new TimePeriod(txtOperational.Text);
                    else if (TimePeriodCheckBox.IsChecked == false)
                        timeOperational = new Time(txtOperational.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                        " Make sure value you are trying to convert is in format 00:00:00.", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedOperational = true;
                }

                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Entered Time values are incorrect!" +
                        " Remember that these are max values for Time: 23:59:59.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedOperational = true;
                }

                catch (ArgumentException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                            " Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedOperational = true;
                }
                finally
                {
                    if (!conversionFailedOperational)
                    {
                        if (TimePeriodCheckBox.IsChecked == true)
                        {
                            timePeriodOperational = new TimePeriod(txtOperational.Text);
                            confirmedOperationalTimePeriod = true;
                        }
                        else if (TimePeriodCheckBox.IsChecked == false)
                        {
                            timeOperational = new Time(txtOperational.Text);
                            confirmedOperationalTime = true;
                        }
                    }
                    else
                    {
                        toggleButton.IsChecked = false;
                        timePeriodOperational = new TimePeriod();
                        timeOperational = new Time();
                        confirmedOperationalTimePeriod = false;
                        confirmedOperationalTime = false;
                    }
                }

            }
            else
            {
                if (TimePeriodCheckBox.IsChecked == true)
                {
                    timePeriodOperational = new TimePeriod();
                    confirmedOperationalTimePeriod = false;
                }
                else if (TimePeriodCheckBox.IsChecked == false)
                {
                    timeOperational = new Time();
                    confirmedOperationalTime = false;
                }

            }

            if (confirmedPrimary && confirmedOperationalTime)
            {
                TimePeriodCheckBox.IsHitTestVisible = false;

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
                TimePeriodCheckBox.IsHitTestVisible = false;

                Time Added = timePrimary + timePeriodOperational;
                Time Subtracted = timePrimary - timePeriodOperational;

                txtAdd.Text = Added.ToString();
                txtSubtract.Text = Subtracted.ToString();

            }
            else
            {
                TimePeriodCheckBox.IsHitTestVisible = true;

                txtEqual.Text = "Empty";
                txtNotEqual.Text = "Empty";
                txtSmaller.Text = "Empty";
                txtBigger.Text = "Empty";
                txtSmallerOrEqual.Text = "Empty";
                txtBiggerOrEqual.Text = "Empty";
                txtAdd.Text = "Empty";
                txtSubtract.Text = "Empty";
            }
        }

    }
}
