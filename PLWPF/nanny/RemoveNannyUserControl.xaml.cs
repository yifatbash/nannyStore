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
    /// Interaction logic for RemoveNannyUserControl.xaml
    /// </summary>
    public partial class RemoveNannyUserControl : UserControl
    {
        BE.Nanny nanny { get; set; }
        BL.IBL bl;

        public RemoveNannyUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();

            this.DataContext = this;
            this.NannyDataGrid.ItemsSource = bl.GetNannyList();
            this.NannyDataGrid.SelectedValuePath = "Id";
        }

        /// <summary>
        /// delete the selected nanny
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
                        "Delete Nanny",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int nannyId = (int)this.NannyDataGrid.SelectedValue;
                    bl.RemoveNanny(nannyId);
                    refreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// reload the page data, to display the updated nanny list
        /// </summary>
        private void refreshData()
        {
            this.NannyDataGrid.ItemsSource = null;
            this.NannyDataGrid.ItemsSource = bl.GetNannyList();
        }

        /// <summary>
        /// display the selected nanny details
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

