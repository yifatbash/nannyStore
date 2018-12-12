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
    /// Interaction logic for UpdateNannyUserControl.xaml
    /// </summary>
    public partial class UpdateNannyUserControl : UserControl
    {
        BE.Nanny nannyToUpdate { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateNannyUserControl()
        {
            InitializeComponent();
            nannyToUpdate = new BE.Nanny();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {
                this.nannyComboBox.ItemsSource = bl.GetNannyList();
                this.nannyComboBox.DisplayMemberPath = "FirstName";
                this.nannyComboBox.SelectedValuePath = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// when select nanny in the comboBox- here details automaticlly would appear in the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nannyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.nannyComboBox.SelectedItem is BE.Nanny)
            {
                try
                {
                    this.nannyToUpdate = ((BE.Nanny)this.nannyComboBox.SelectedItem).GetCopy();
                    this.DataContext = nannyToUpdate;
                    this.allNannyDetailsGrid.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// if nanny doesn't enanble pay for hour- earse the hourly rate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enanblePayForHourCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.hourlyRateTextBox.Text = "";
            this.nannyToUpdate.HourlyRate = null;
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

            //this.UpdateNannyButton.IsEnabled = !errorMessages.Any();
        }

        
        private void UpdateNannyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                            "Are you sure?\n",
                            "Update Nanny",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        bl.UpdateNannyDetails(nannyToUpdate);
                        MessageBox.Show($"{nannyToUpdate.FirstName} {nannyToUpdate.LastName} was updated successfully!\n\n" + nannyToUpdate.ToString());
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
        /// reload the page data, to get empty fields
        /// </summary>
        private void refreshData()
        {
            try
            {
                nannyToUpdate = new BE.Nanny();
                this.DataContext = nannyToUpdate;
                this.nannyComboBox.SelectedItem = null;
                this.nannyComboBox.ItemsSource = null;
                this.nannyComboBox.ItemsSource = bl.GetNannyList();
                this.allNannyDetailsGrid.IsEnabled = false;
                this.SpecialActivitiesExpander.IsExpanded = false;
                this.RecommendationsExpander.IsExpanded = false;
                this.CommentsExpender.IsExpanded = false;
            }
            catch
            {
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }
        }

       /// <summary>
       ///reload the comboBox itemsSource in order to get the updated nannyList after we update a nanny
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void nannyComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.nannyComboBox.ItemsSource = null;
            this.nannyComboBox.ItemsSource = bl.GetNannyList();
        }
    }
}
