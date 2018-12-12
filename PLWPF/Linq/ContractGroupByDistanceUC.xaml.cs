using System;
using System.Collections;
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
    /// Interaction logic for ContractGroupByDistanceUC.xaml
    /// </summary>
    public partial class ContractGroupByDistanceUC : UserControl
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

        public ContractGroupByDistanceUC()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            this.DataContext = this;
        }

        private void orderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            void setListViewSource(IEnumerable<IGrouping<string, BE.Contract>> groups)
            {
                Source = groups.ToList();
            }

            Thread t = new Thread(
                () =>
                {
                    var myContractGroups = bl.GroupContractByDistance(true).ToList();
                    Action<IEnumerable<IGrouping<string, BE.Contract>>> a = setListViewSource;
                    Dispatcher.BeginInvoke(a, myContractGroups);
                });
            t.Start();
            
        }

        private void orderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            void setListViewSource(IEnumerable<IGrouping<string, BE.Contract>> groups)
            {
                Source = groups.ToList();
            }

            Thread t = new Thread(
                () =>
                {
                    var myContractGroups = bl.GroupContractByDistance(false).ToList();
                    Action<IEnumerable<IGrouping<string, BE.Contract>>> a = setListViewSource;
                    Dispatcher.BeginInvoke(a, myContractGroups);
                });
            t.Start();
        }

        }
}
