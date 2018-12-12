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
    /// Interaction logic for UpdateMotherUserControl.xaml
    /// </summary>
    public partial class UpdateMotherUserControl : UserControl
    {
        BE.Mother motherToUpdate { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateMotherUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            this.motherComboBox.ItemsSource = bl.GetMotherList();
            this.motherComboBox.DisplayMemberPath = "FirstName";
            this.motherComboBox.SelectedValuePath = "Id";

            this.statusComboBox.ItemsSource = Enum.GetValues(typeof(BE.MotherStatus));
        }

        /// <summary>
        /// when select mother in the comboBox- here details automaticlly would appear in the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void motherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.motherComboBox.SelectedItem is BE.Mother)
            {
                try
                {
                    this.motherToUpdate = ((BE.Mother)this.motherComboBox.SelectedItem).GetCopy();
                    this.DataContext = motherToUpdate;
                    this.motherDetailsGrid.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

            //this.UpdateMotherButton.IsEnabled = !errorMessages.Any();
        }

        private void UpdateMotherButton_Click(object sender, RoutedEventArgs e)
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
                            "Are you sure?\n\n",
                            "Update Mother",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        bl.UpdateMotherDetails(motherToUpdate);
                        MessageBox.Show($"{motherToUpdate.FirstName} {motherToUpdate.LastName} was updated successfully!\n\n" + motherToUpdate.ToString());
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
            this.DataContext = motherToUpdate = null;

            this.motherComboBox.SelectedIndex = -1;
            this.motherComboBox.ItemsSource = null;
            this.motherComboBox.ItemsSource = bl.GetMotherList();

            this.motherSchedule.IsExpanded = false;
            this.commentsExpander.IsExpanded = false;

            this.motherDetailsGrid.IsEnabled = false;
        }
    }
}

