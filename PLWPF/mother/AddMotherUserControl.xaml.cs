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
    /// Interaction logic for AddMotherUserControl.xaml
    /// </summary>
    public partial class AddMotherUserControl : UserControl
    {
        BE.Mother mother { get; set; }
        BL.IBL bl;
        private List<string> errorMessages;

        public AddMotherUserControl()
        {
            InitializeComponent();
            mother = new BE.Mother();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {
                this.DataContext = mother;
                this.statusComboBox.ItemsSource = Enum.GetValues(typeof(BE.MotherStatus));
                this.motherScheduleDataGrid.ItemsSource = mother.WantedNannySchedule;
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

            //this.AddMotherButton.IsEnabled = !errorMessages.Any();
        }

        private void AddMotherButton_Click(object sender, RoutedEventArgs e)
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
                            "Are the details correct?\n\n" + mother.ToString(),
                            "Confirm Mother Details",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        bl.AddMother(mother);
                        MessageBox.Show(
                            $"{mother.FirstName} {mother.LastName} was added successfully!\n\n" + mother.ToString(),
                            "Add New Mother");
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
            try
            {
                mother = new BE.Mother();
                this.DataContext = null;
                this.DataContext = mother;
                this.statusComboBox.SelectedItem = null;
                this.CommentsExpander.IsEnabled = false;
            }
            catch
            {
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }
        }
    }
}
