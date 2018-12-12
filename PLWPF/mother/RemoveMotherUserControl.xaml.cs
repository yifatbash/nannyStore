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
    /// Interaction logic for RemoveMotherUserControl.xaml
    /// </summary>
    public partial class RemoveMotherUserControl : UserControl
    {
        BE.Mother mother { get; set; }
        BL.IBL bl;
       
        public RemoveMotherUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();

            this.DataContext = this;
            this.MotherDataGrid.ItemsSource = bl.GetMotherList();
            this.MotherDataGrid.SelectedValuePath = "Id";
        }

        /// <summary>
        /// delete the selected mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                        "Are you sure?",
                        "Delete Mother",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int motherId = (int)this.MotherDataGrid.SelectedValue;
                    bl.RemoveMother(motherId);
                    refreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  reload the page data, to display the updated mother list
        /// </summary>
        private void refreshData()
        {
            this.MotherDataGrid.ItemsSource = null;
            this.MotherDataGrid.ItemsSource = bl.GetMotherList();
        }

        /// <summary>
        /// show the details of the selected mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moreDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MotherDataGrid.SelectedItem is BE.Mother)
                {
                    mother = (BE.Mother)this.MotherDataGrid.SelectedItem;
                    MessageBox.Show(
                        mother.ToString(),
                        $"{mother.FirstName} {mother.LastName} Details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
