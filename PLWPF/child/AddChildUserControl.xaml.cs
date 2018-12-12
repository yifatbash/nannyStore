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
    /// Interaction logic for AddChildUserControl.xaml
    /// </summary>
    public partial class AddChildUserControl : UserControl
    {
        BE.Child child;
        BL.IBL bl;
        private List<string> errorMessages;

        public AddChildUserControl()
        {
            InitializeComponent();
            child = new BE.Child();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {
                this.DataContext = child;
                child.BirthDate = DateTime.Parse("1.9.2017");

                this.motherIdComboBox.ItemsSource = bl.GetMotherList();
                this.motherIdComboBox.DisplayMemberPath = "FirstName";
                this.motherIdComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void isSpecialNeedsChildCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           SpecialNeedsText.Text = "";
        }

        private void isFoodAllergyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FoodAllergyText.Text = "";
        }

        private void isMedicinesAllergyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MedicinesAllergyText.Text = "";
        }

        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            //this.AddNannyButton.IsEnabled = !errorMessages.Any();
        }

        private void AddChildButton_Click(object sender, RoutedEventArgs e)
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
                    "Are the details correct?\n\n" + child.ToString(),
                    "Confirm Child Details",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information);

                    if (result == MessageBoxResult.OK)
                    {
                        bl.AddChild(child);
                        MessageBox.Show(
                            child.FirstName + " was added successfully!\n\n" + child.ToString(),
                            "Add New Child");
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
                child = new BE.Child();
                child.BirthDate = DateTime.Parse("1.9.2017");
                //this.DataContext = null;
                this.DataContext = child;
                this.motherIdComboBox.SelectedItem = null;
            }
            catch
            {
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }

        }
    }
}
