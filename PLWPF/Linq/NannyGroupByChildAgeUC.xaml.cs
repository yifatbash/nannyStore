using System;
using System.Collections;
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
    /// Interaction logic for NannyGroupByChildAgeUC.xaml
    /// </summary>
    public partial class NannyGroupByChildAgeUC : UserControl
    {
        BL.IBL bl;
        private IEnumerable source;
        public IEnumerable Source
        {
            get { return source; }
            set
            {
                source = value;
                this.listView.ItemsSource = null;
                this.listView.ItemsSource = source;
            }
        }

        public NannyGroupByChildAgeUC()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            this.DataContext = this;
        }

        private void orderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.maxCheckBox.IsChecked == true)
                Source = bl.GroupNannyByChildAge(true, true);
            else
                Source = bl.GroupNannyByChildAge(true, false);
        }

        private void orderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.maxCheckBox.IsChecked == true)
                Source = bl.GroupNannyByChildAge(false, true);
            else
                Source = bl.GroupNannyByChildAge(false, false);
        }

        private void maxCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.orderCheckBox.IsChecked == true)
                Source = bl.GroupNannyByChildAge(true, true);
            else
                Source = bl.GroupNannyByChildAge(false, true);
        }

        private void maxCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.orderCheckBox.IsChecked == true)
                Source = bl.GroupNannyByChildAge(true, false);
            else
                Source = bl.GroupNannyByChildAge(false, false);
        }
    }
}

