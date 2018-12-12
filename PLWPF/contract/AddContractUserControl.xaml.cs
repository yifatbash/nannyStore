using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AddContractUserControl.xaml
    /// </summary>
    public partial class AddContractUserControl : UserControl
    {
        BE.Contract contract;
        public BE.Mother mother { get; set; }
        public BE.Nanny nanny { get; set; }
        public BE.Child child { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public AddContractUserControl()
        {
            InitializeComponent();
            contract = new BE.Contract();
            nanny = new BE.Nanny();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {

                this.DataContext = this;
                this.NannyDataGrid.DataContext = this;
                this.contractDetailsGrid.DataContext = contract;

                this.selectMotherComboBox.DataContext = this;
                this.selectMotherComboBox.ItemsSource = bl.GetMotherList();
                this.selectMotherComboBox.DisplayMemberPath = "FirstName";
                this.selectMotherComboBox.SelectedValuePath = "Id";

                this.selectChildComboBox.DataContext = this;
                this.selectChildComboBox.DisplayMemberPath = "FirstName";
                this.selectChildComboBox.SelectedValuePath = "Id";

                this.DistanceComboBox.ItemsSource = new int[] {1,2,5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
                this.DistanceComboBox.SelectedValue = 50;

                //this.nannyIdTextBox.DataContext = this;

                this.paidByComboBox.ItemsSource = Enum.GetValues(typeof(BE.WayOfPayment));
                this.payPerHourOrMonthComboBox.ItemsSource = Enum.GetValues(typeof(BE.PaymentPer));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// when select nanny all the nanny details would automaticlly appear in the contract fieds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NannyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (nanny != null)
                {
                    contract = new BE.Contract();
                    this.contract.ChildId = (int)this.selectChildComboBox.SelectedValue;
                    this.contract.NannyId = nanny.Id;
                    this.contract.MonthlyRate = nanny.MonthlyRate;
                    this.contract.WeeklyHours = bl.CalculateContractWeeklyHours(contract);
                    if (nanny.EnanblePayForHour == true)
                        this.contract.HourlyRate = nanny.HourlyRate;
                    this.contractDetailsGrid.DataContext = contract;
                    if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                        return;
                    this.contract.NetoRate = bl.CalculateContractRate(contract);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// when select mother- reload the child list in the comboBox, bring the mother's children that have no nanny(have no valid contract)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectMotherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (mother != null)
                {
                    this.selectChildComboBox.ItemsSource = null;
                    this.selectChildComboBox.ItemsSource = bl.GetChildList(c => c.MotherId == mother.Id && bl.GetNoNannyChildrenList().Any<BE.Child>(ch => ch.Id == c.Id));
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// when select child- his details whould automaticlly appear in the contract fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectChildComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (child != null)
            {
                contract.ChildId = (int)selectChildComboBox.SelectedValue;
                this.ageTextBox.Text = bl.GetChildAge(contract.ChildId).ToString();

                if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null)
                    return;
                this.contract.NetoRate = bl.CalculateContractRate(contract);
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

        private void addContractButton_Click(object sender, RoutedEventArgs e)
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
                            "Are the details correct?\n\n" + contract.ToString(),
                            "Confirm The Contrct Details",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Information);

                    if (result == MessageBoxResult.OK)
                    {
                        this.contract.NetoRate = bl.CalculateContractRate(contract);
                        bl.AddContract(contract);
                        MessageBox.Show(
        $"Contract #{ contract.ContractId} was added successfully!\n\n" + contract.ToString(),
        "Add New Contract");
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
        ///  reload the page data, to get clean fields
        /// </summary>
        private void refreshData()
        {
            try
            {
                contract = new BE.Contract();
                this.contractDetailsGrid.DataContext = null;
                this.contractDetailsGrid.DataContext = contract;

                this.selectMotherComboBox.SelectedItem = null;

                this.NannyDataGrid.ItemsSource = null;
                this.NannyDataGrid.ItemsSource = bl.GetNannyList();
                this.NannyDataStackPanel.Visibility = Visibility.Collapsed;


                this.DistanceComboBox.SelectedValue = 50;
                this.AlmostMatchHoursRadioButton.IsChecked = true;
                this.MonthlyRateRadioButton.IsChecked = true;
                this.MonthlyRateTextBox.Text = "3000";

                this.selectChildComboBox.SelectedItem = null;

                this.selectNannyExpander.IsExpanded = false;
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
                if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contract.NetoRate = bl.CalculateContractRate(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// recalculate the neto rate when monthlyRateTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthlyRateTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contract.NetoRate = bl.CalculateContractRate(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// recalculate the neto rate when hourlyRateTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hourlyRateTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contract.NetoRate = bl.CalculateContractRate(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// recalculate the neto rate when weeklyHoursTextBox lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weeklyHoursTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contract.PayPerHourOrMonth == BE.PaymentPer.Hour && contract.HourlyRate == null || selectChildComboBox.SelectedIndex < 0)
                    return;
                this.contract.NetoRate = bl.CalculateContractRate(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Bring all the nannies according to the mother's constraints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindnNanny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int motherId = (int)this.selectMotherComboBox.SelectedValue;
                int childAge = int.Parse(this.ageTextBox.Text);
                int maxDistance = (int)DistanceComboBox.SelectedValue;

                bool almostMatchNanny;
                if (this.AlmostMatchHoursRadioButton.IsChecked == true)
                    almostMatchNanny = true;
                else
                    almostMatchNanny = false;

                bool isMonthlyRate = true;
                int maxRate = 3000;
                
                if (this.MonthlyRateRadioButton.IsChecked == true)
                {
                    maxRate = int.Parse(this.MonthlyRateTextBox.Text);
                    isMonthlyRate = true;
                }
                else if (this.HourlyRateRadioButton.IsChecked == true)
                {
                    maxRate = int.Parse(this.HourlyRateTextBox.Text);
                    isMonthlyRate = false;
                }

                void FindNannyFunc()
                {

                    List<BE.Nanny> nannyList = bl.GetNannyByMotherConstraints(motherId, childAge, maxRate, isMonthlyRate, almostMatchNanny, maxDistance * 1000);
                    Action<List<BE.Nanny>> a = setDataGridSource;
                    Dispatcher.BeginInvoke(a, nannyList);
                }

                void setDataGridSource(List<BE.Nanny> lst)
                {
                    this.NannyDataGrid.ItemsSource = lst;
                }

                Thread t = new Thread(FindNannyFunc);
                t.Start();

                this.NannyDataStackPanel.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// show the details of the selected nanny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moreDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.NannyDataGrid.SelectedItem is BE.Nanny)
                {
                    nanny = (BE.Nanny)this.NannyDataGrid.SelectedItem;
                    MessageBox.Show(
                        nanny.ToString(),
                        $"{nanny.FirstName} {nanny.LastName} Details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
