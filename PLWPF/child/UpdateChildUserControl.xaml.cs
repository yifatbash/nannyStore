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
    /// Interaction logic for UpdateChildUserControl.xaml
    /// </summary>
    public partial class UpdateChildUserControl : UserControl
    {
        BE.Child childToBeUpdated;
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateChildUserControl()
        {
            InitializeComponent();
            childToBeUpdated = new BE.Child();
            bl = BL.FactoryBL.GetBL();
            errorMessages = new List<string>();

            try
            {
                this.idComboBox.DataContext = this;
                this.idComboBox.ItemsSource = bl.GetChildList();
                this.idComboBox.DisplayMemberPath = "FirstName";
                this.idComboBox.SelectedValuePath = "Id";
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

        private void idComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.idComboBox.SelectedItem is BE.Child)
            {
                try
                {
                    this.childToBeUpdated = ((BE.Child)this.idComboBox.SelectedItem).GetCopy();
                    this.DataContext = childToBeUpdated;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            //this.AddNannyButton.IsEnabled = !errorMessages.Any();
        }


        private void UpdateChildButton_Click(object sender, RoutedEventArgs e)
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
                    "Are you sure?\n\n" + childToBeUpdated.ToString(),
                    "Update Child",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        bl.UpdateChildDetails(childToBeUpdated);
                        MessageBox.Show($"{childToBeUpdated.FirstName} was updated successfully!\n");
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
                childToBeUpdated = new BE.Child();
                this.DataContext = childToBeUpdated;
                this.idComboBox.SelectedItem = null;
                this.idComboBox.ItemsSource = bl.GetChildList();

                this.childDetailsGrid.IsEnabled = false;
                this.isBreastMilkCheckBox.IsEnabled = false;
                this.isMedicinesAllergyCheckBox.IsEnabled = false;
                this.isFoodAllergyCheckBox.IsEnabled = false;
                this.isSpecialNeedsChildCheckBox.IsEnabled = false;
                this.commentsExpander.IsEnabled = false;

                this.UpdateChildButton.IsEnabled = false;

                this.commentsExpander.IsEnabled = false;
            }
            catch
            {
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }
        }
        }
    }
