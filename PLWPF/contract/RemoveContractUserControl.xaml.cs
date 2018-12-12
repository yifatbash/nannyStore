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
    /// Interaction logic for RemoveContractUserControl.xaml
    /// </summary>
    public partial class RemoveContractUserControl : UserControl
    {
        BE.Contract contract { get; set; }
        BL.IBL bl;

        public RemoveContractUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();

            this.DataContext = this;

            this.nannyComboBox.ItemsSource = bl.GetNannyList();
            this.nannyComboBox.DisplayMemberPath = "FirstName";
            this.nannyComboBox.SelectedValuePath = "Id";

            this.allRadioButton.IsChecked = true;
            this.contractDataGrid.ItemsSource = bl.GetContractList();
        }

        /// <summary>
        /// when select nanny- bring the contract's of the selected nanny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nannyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.nannyComboBox.SelectedItem is BE.Nanny)
            {
                int nannyId = (int)this.nannyComboBox.SelectedValue;
                this.contractDataGrid.ItemsSource = bl.GetContractList(c => c.NannyId == nannyId);
            }
        }

        /// <summary>
        /// when checked inValidRadioButton- bring only the invalid contracts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inValidRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.contractDataGrid.ItemsSource = bl.GetContractList(c => bl.IsValidContract(c.ContractId) == false);
        }

        /// <summary>
        /// when checked inValidRadioButton- bring all the contracts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.contractDataGrid.ItemsSource = bl.GetContractList();
        }

        private void moreDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.contractDataGrid.SelectedItem is BE.Contract)
                {
                    contract = (BE.Contract)this.contractDataGrid.SelectedItem;
                    MessageBox.Show(
                        contract.ToString(),
                        $"Contract #{contract.ContractId} Details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void deleteDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.contractDataGrid.SelectedItem is BE.Contract)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "Are you sure?",
                            "Delete Contract",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        contract = (BE.Contract)this.contractDataGrid.SelectedItem;
                        bl.RemoveContract(contract.ContractId);
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
        /// reload the page data, to display the updated contract list
        /// </summary>
        private void refreshData()
        {
            try
            {
                this.contractDataGrid.ItemsSource = null;
                if (this.allRadioButton.IsChecked == true)
                    this.contractDataGrid.ItemsSource = bl.GetContractList();
                if (this.inValidRadioButton.IsChecked == true)
                {
                    this.contractDataGrid.ItemsSource = bl.GetContractList(c => bl.IsValidContract(c.ContractId) == false);
                }
                if (this.nannyRadioButton.IsChecked == true)
                {
                    if (this.nannyComboBox.SelectedItem is BE.Nanny)
                    {
                        int nannyId = (int)this.nannyComboBox.SelectedValue;
                        this.contractDataGrid.ItemsSource = bl.GetContractList(c => c.NannyId == nannyId);
                    }
                    else
                    {
                        this.allRadioButton.IsChecked = true;
                        this.contractDataGrid.ItemsSource = bl.GetContractList();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
