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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddNannyUserControl.xaml
    /// </summary>
    public partial class AddNannyUserControl : UserControl
    {
        BE.Nanny nanny { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public AddNannyUserControl()
        {
            InitializeComponent();
            nanny = new BE.Nanny();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {
                this.DataContext = nanny;
                nanny.MinAge = 3;
                nanny.MaxAge = 3;
                nanny.MaxNumOfChildren = 1;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// add to the errorMessagesList all the errors that have quit exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            //this.AddNannyButton.IsEnabled = !errorMessages.Any();
        }

        private void AddNannyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.enanblePayForHourCheckBox.IsChecked == true && this.hourlyRateTextBox.Text == "")
                    throw new Exception("Must enter hourly rate");

                if (errorMessages.Any()) //errorMessages.Count > 0 
                {
                    string err = "Exception:";
                    foreach (var item in errorMessages)
                        err += "\n" + item;

                    MessageBox.Show(err);
                    return;
                }
                else
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "Are the details correct?\n\n" + nanny.ToString(),
                            "Confirm Nanny Details",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Information);

                    if (result == MessageBoxResult.OK)
                    {
                        bl.AddNanny(nanny);
                        MessageBox.Show(
                            nanny.FirstName + " " + nanny.LastName + " was added successfully!\n" + nanny.ToString(),
                            "Add New Nanny");
                        refreshData();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// reload the page data, to get clean fields
        /// </summary>
        private void refreshData()
        {
            nanny = new BE.Nanny();
            this.DataContext = nanny;
            this.CommentsExpander.IsExpanded = false;
            this.RecommendationsExpander.IsExpanded = false;
            this.SpecialActivitiesExpander.IsExpanded = false;
        }

        /// <summary>
        /// if nanny doesn't enanble pay for hour- earse the hourly rate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enanblePayForHourCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.hourlyRateTextBox.Text = null;
            this.nanny.HourlyRate = null;
        }
    }
}

