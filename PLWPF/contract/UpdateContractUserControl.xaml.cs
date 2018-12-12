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
    /// Interaction logic for UpdateContractUserControl.xaml
    /// </summary>
    public partial class UpdateContractUserControl : UserControl
    {
        BE.Contract contract { get; set; }
        BE.Contract contractToUpdate { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateContractUserControl()
        {
            InitializeComponent();
            //contract = new BE.Contract();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            this.DataContext = this;

            this.selectNannyComboBox.ItemsSource = bl.GetNannyList();
            this.selectNannyComboBox.DisplayMemberPath = "FirstName";
            this.selectNannyComboBox.SelectedValuePath = "Id";

            this.selectChildComboBox.ItemsSource = bl.GetChildList();
            this.selectChildComboBox.DisplayMemberPath = "FirstName";
            this.selectChildComboBox.SelectedValuePath = "Id";

            this.payPerHourOrMonthComboBox.ItemsSource = Enum.GetValues(typeof(BE.PaymentPer));
            this.paidByComboBox.ItemsSource = Enum.GetValues(typeof(BE.WayOfPayment));
        }

        /// <summary>
        /// when change the nanny selection in the ComboBox reload the children list in the selectChildComboBox, bring the nanny's children
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectNannyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                int nannyId = (int)this.selectNannyComboBox.SelectedValue;
                this.selectChildComboBox.ItemsSource = bl.GetChildrenListByNannyId(nannyId);
            }
        }

        /// <summary>
        /// the wanted contract's details would automaticlly apper in the fields 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchContractButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int? nannyId = (int?)this.selectNannyComboBox.SelectedValue;
                int? childId = (int?)this.selectChildComboBox.SelectedValue;

                if (nannyId == null)
                    throw new Exception("must select nanny first");

                if (childId == null)
                    throw new Exception("must select child first");

                contract = bl.SearchContract((int)nannyId, (int)childId);
                contractToUpdate = contract.GetCopy();
                this.contractToUpdate.NetoRate = bl.CalculateContractRate(contractToUpdate);
                this.contractDetailsGrid.DataContext = contractToUpdate;
                this.contractDetailsGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  add to the errorMessagesList all the errors that have quit exceptions
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

        private void updateContractButton_Click(object sender, RoutedEventArgs e)
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
                        "Are you sure?",
                        "Update Contract",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        bl.UpdateContractDetails(contractToUpdate);
                        MessageBox.Show($"Contract #{ contractToUpdate.ContractId} was updated successfully!\n\n" + contractToUpdate.ToString());
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
        ///  reload the page data, to get empty fields
        /// </summary>
        private void refreshData()
        {
            try
            {
                contractToUpdate = new BE.Contract();
                this.contractDetailsGrid.DataContext = null;
                this.contractDetailsGrid.DataContext = contractToUpdate;

                this.selectNannyComboBox.SelectedItem = null;
                this.selectChildComboBox.SelectedItem = null;

                this.contractDetailsGrid.Visibility = Visibility.Collapsed;
            }
            catch
            {
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }
        }

        /// <summary>
        /// recalculate the neto rate when user change his choice (pay per month or hour)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void payPerHourOrMonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (contractToUpdate.PayPerHourOrMonth == BE.PaymentPer.Hour && contractToUpdate.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contractToUpdate.NetoRate = bl.CalculateContractRate(contractToUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        ///  recalculate the neto rate when weeklyHoursTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weeklyHoursTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contractToUpdate.PayPerHourOrMonth == BE.PaymentPer.Hour && contractToUpdate.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contractToUpdate.NetoRate = bl.CalculateContractRate(contractToUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        ///  recalculate the neto rate when the monthlyRateTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthlyRateTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contractToUpdate.PayPerHourOrMonth == BE.PaymentPer.Hour && contractToUpdate.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contractToUpdate.NetoRate = bl.CalculateContractRate(contractToUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// recalculate the neto rate when the hourlyRateTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hourlyRateTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contractToUpdate.PayPerHourOrMonth == BE.PaymentPer.Hour && contractToUpdate.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contractToUpdate.NetoRate = bl.CalculateContractRate(contractToUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
