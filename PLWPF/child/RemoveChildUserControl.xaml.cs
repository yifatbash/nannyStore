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
    /// Interaction logic for RemoveChildUserControl.xaml
    /// </summary>
    public partial class RemoveChildUserControl : UserControl
    {
        BE.Child child { get; set; }
        BL.IBL bl;

        public RemoveChildUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();

            this.DataContext = this;

            this.nannyComboBox.ItemsSource = bl.GetNannyList();
            this.nannyComboBox.DisplayMemberPath = "FirstName";
            this.nannyComboBox.SelectedValuePath = "Id";

            this.motherComboBox.ItemsSource = bl.GetMotherList();
            this.motherComboBox.DisplayMemberPath = "FirstName";
            this.motherComboBox.SelectedValuePath = "Id";

            this.allRadioButton.IsChecked = true;
            this.ChildDataGrid.ItemsSource = bl.GetChildList();
        }

        private void allRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.ChildDataGrid.ItemsSource = bl.GetChildList();
        }

        private void nannyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.nannyComboBox.SelectedItem is BE.Nanny)
            {
                int nannyId = (int)this.nannyComboBox.SelectedValue;
                this.ChildDataGrid.ItemsSource = bl.GetChildrenListByNannyId(nannyId);
            }
        }

        private void motherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.motherComboBox.SelectedItem is BE.Mother)
            {
                int motherId = (int)this.motherComboBox.SelectedValue;
                this.ChildDataGrid.ItemsSource = bl.GetChildList(c => c.MotherId == motherId);
            }
        }

        private void deleteDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.ChildDataGrid.SelectedItem is BE.Child)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                        "Are you sure?",
                        "Delete Child",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);


                    if (result == MessageBoxResult.Yes)
                    {
                        child = (BE.Child)this.ChildDataGrid.SelectedItem;
                        bl.RemoveChild(child.Id);
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
                this.ChildDataGrid.ItemsSource = null;
                if (this.allRadioButton.IsChecked == true)
                    this.ChildDataGrid.ItemsSource = bl.GetChildList();
                if (this.motherRadioButton.IsChecked == true)
                {
                    if (this.motherComboBox.SelectedItem is BE.Mother)
                    {
                        int motherId = (int)this.motherComboBox.SelectedValue;
                        this.ChildDataGrid.ItemsSource = bl.GetChildList(c => c.MotherId == motherId);
                    }
                    else
                    {
                        this.allRadioButton.IsChecked = true;
                        this.ChildDataGrid.ItemsSource = bl.GetChildList();
                    }
                }
                if (this.nannyRadioButton.IsChecked == true)
                {
                    if (this.nannyComboBox.SelectedItem is BE.Nanny)
                    {
                        int nannyId = (int)this.nannyComboBox.SelectedValue;
                        this.ChildDataGrid.ItemsSource = bl.GetChildrenListByNannyId(nannyId);
                    }
                    else
                    {
                        this.allRadioButton.IsChecked = true;
                        this.ChildDataGrid.ItemsSource = bl.GetChildList();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void moreDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.ChildDataGrid.SelectedItem is BE.Child)
                {
                    child = (BE.Child)this.ChildDataGrid.SelectedItem;
                    MessageBox.Show(
                        child.ToString(),
                       $"{child.FirstName} Details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
