using System;
using System.Collections.Generic;
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
    /// Interaction logic for TimePeriodView.xaml
    /// </summary>
    public partial class TimePeriodView : UserControl
    {
        public TimePeriodView()
        {
            InitializeComponent();
        }


        //Bools - Confirmation
        private bool confirmedPrimary = false;
        private bool confirmedOperational = false;

        //TimeValues
        private TimePeriod timePeriodPrimary = new TimePeriod();
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
                    timePeriodPrimary = new TimePeriod(txtPrimary.Text);
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
                        " Remember that these are max values for TimePeriod: --:59:59.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedPrimary = true;
                }

                catch (ArgumentException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                            " Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.", "ArgumentError", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedPrimary = true;
                }
                finally
                {
                    if (!conversionFailedPrimary)
                    {
                        timePeriodPrimary = new TimePeriod(txtPrimary.Text);
                        confirmedPrimary = true;
                    }
                    else
                    {
                        toggleButton.IsChecked = false;
                        timePeriodPrimary = new TimePeriod();
                        confirmedPrimary = false;
                    }
                }
            }
            else
            {
                timePeriodPrimary = new TimePeriod();
                confirmedPrimary = false;
            }

            if (confirmedPrimary)
            {
                if (!String.IsNullOrEmpty(MultiplyNumber.Text))
                {
                    bool parseMultiplicator = int.TryParse(MultiplyNumber.Text, out int multiplicator);

                    if (parseMultiplicator)
                    {
                        TimePeriod Multiplied = timePeriodPrimary * multiplicator;
                        txtMultiply.Text = Multiplied.ToString();
                    }
                    else
                    {
                        MessageBox.Show("TimePeriod can't be multiplied by given number. Make sure number you are trying to enter is correct.", "Multiplicator", MessageBoxButton.OK, MessageBoxImage.Warning);
                        MultiplyNumber.Text = "";
                    }
                }
            }
            else
            {
                txtMultiply.Text = "Empty";
            }


            if (confirmedPrimary && confirmedOperational)
            {
                if (timePeriodPrimary == timePeriodOperational)
                    txtEqual.Text = "True";
                else txtEqual.Text = "False";

                if (timePeriodPrimary != timePeriodOperational)
                    txtNotEqual.Text = "True";
                else txtNotEqual.Text = "False";

                if (timePeriodPrimary < timePeriodOperational)
                    txtSmaller.Text = "True";
                else txtSmaller.Text = "False";

                if (timePeriodPrimary > timePeriodOperational)
                    txtBigger.Text = "True";
                else txtBigger.Text = "False";

                if (timePeriodPrimary <= timePeriodOperational)
                    txtSmallerOrEqual.Text = "True";
                else txtSmallerOrEqual.Text = "False";

                if (timePeriodPrimary >= timePeriodOperational)
                    txtBiggerOrEqual.Text = "True";
                else txtBiggerOrEqual.Text = "False";

                TimePeriod Added = timePeriodPrimary + timePeriodOperational;
                txtAdd.Text = Added.ToString();

                //Flag
                bool subtractFailed = false;

                try
                {
                    TimePeriod Subtracted = timePeriodPrimary - timePeriodOperational;
                }
                catch (ArgumentOutOfRangeException)
                {
                    subtractFailed = true;
                }
                finally
                {
                    if (!subtractFailed)
                    {
                        TimePeriod Subtracted = timePeriodPrimary - timePeriodOperational;
                        txtSubtract.Text = Subtracted.ToString();
                    }
                    else
                    {
                        txtSubtract.Text = "Empty";
                    }
                }
            }
            else
            {
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
                    timePeriodOperational = new TimePeriod(txtOperational.Text);
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
                        " Remember that these are max values for TimePeriod: --:59:59.", "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedOperational = true;
                }

                catch (ArgumentException)
                {
                    MessageBox.Show("Converting input into 'Time' value failed." +
                            " Make sure value you are trying to convert is in format 00:00:00 and consists of natural numbers.", "ArgumentError", MessageBoxButton.OK, MessageBoxImage.Error);

                    conversionFailedOperational = true;
                }
                finally
                {
                    if (!conversionFailedOperational)
                    {
                            timePeriodOperational = new TimePeriod(txtOperational.Text);
                            confirmedOperational = true;
                    }
                    else
                    {
                        toggleButton.IsChecked = false;
                        timePeriodOperational = new TimePeriod();
                        confirmedOperational = false;
                    }
                }

            }
            else
            {
                    timePeriodOperational = new TimePeriod();
                    confirmedOperational = false;
            }

            if (confirmedPrimary && confirmedOperational)
            {

                if (timePeriodPrimary == timePeriodOperational)
                    txtEqual.Text = "True";
                else txtEqual.Text = "False";

                if (timePeriodPrimary != timePeriodOperational)
                    txtNotEqual.Text = "True";
                else txtNotEqual.Text = "False";

                if (timePeriodPrimary < timePeriodOperational)
                    txtSmaller.Text = "True";
                else txtSmaller.Text = "False";

                if (timePeriodPrimary > timePeriodOperational)
                    txtBigger.Text = "True";
                else txtBigger.Text = "False";

                if (timePeriodPrimary <= timePeriodOperational)
                    txtSmallerOrEqual.Text = "True";
                else txtSmallerOrEqual.Text = "False";

                if (timePeriodPrimary >= timePeriodOperational)
                    txtBiggerOrEqual.Text = "True";
                else txtBiggerOrEqual.Text = "False";

                TimePeriod Added = timePeriodPrimary + timePeriodOperational;
                txtAdd.Text = Added.ToString();


                //Flag
                bool subtractFailed = false;

                try
                {
                    TimePeriod Subtracted = timePeriodPrimary - timePeriodOperational;
                }
                catch (ArgumentOutOfRangeException)
                {
                    subtractFailed = true;
                }
                finally
                {
                    if (!subtractFailed)
                    {
                        TimePeriod Subtracted = timePeriodPrimary - timePeriodOperational;
                        txtSubtract.Text = Subtracted.ToString();
                    }
                    else
                    {
                        txtSubtract.Text = "Empty";
                    }
                }
            }
            else
            {
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
