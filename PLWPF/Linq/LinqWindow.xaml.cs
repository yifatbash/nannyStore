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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LinqWindow.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        BL.IBL bl;
        public LinqWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
        }

        private void groupByChildAge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NannyGroupByChildAgeUC uc = new NannyGroupByChildAgeUC();
                uc.Source = bl.GroupNannyByChildAge(false,false);
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllMother_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllUserControl uc = new ShowAllUserControl();
                uc.MyTitle = "Show All Mothers";
                uc.Source = bl.GetMotherList();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupByMotherStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupByUserControl uc = new GroupByUserControl();
                uc.MyTitle = "Group All Mother By Status";
                uc.Source = bl.GroupMotherByStatus();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllNanny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllUserControl uc = new ShowAllUserControl();
                uc.MyTitle = "Show All Nannies";
                uc.Source = bl.GetNannyList();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void childGroupByBearstMilk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChildGroupByBearstMilkUC uc = new ChildGroupByBearstMilkUC();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contractGroupByRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupByUserControl uc = new GroupByUserControl();
                uc.MyTitle = "Group All Contract By Rate";
                uc.Source = bl.GroupContractByRate();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contractGroupByDistance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContractGroupByDistanceUC uc = new ContractGroupByDistanceUC();

                void setListViewSource(IEnumerable<IGrouping<string, BE.Contract>> groups)
                {
                    uc.Source = groups.ToList();
                }

                Thread t = new Thread(
                    ()=>
                    {
                        var myContractGroups = bl.GroupContractByDistance().ToList();
                        Action<IEnumerable<IGrouping<string, BE.Contract>>> a = setListViewSource;
                        Dispatcher.BeginInvoke(a, myContractGroups);
                    });
                t.Start();

                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllChild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllUserControl uc = new ShowAllUserControl();
                uc.MyTitle = "Show All Children";
                uc.Source = bl.GetChildList();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllContracts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllUserControl uc = new ShowAllUserControl();
                uc.MyTitle = "Show All Contracts";
                uc.Source = bl.GetContractList();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showTMTNanny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAllUserControl uc = new ShowAllUserControl();
                uc.MyTitle = "Show All Nanny With TMT Vacations";
                uc.Source = bl.GetNannyWithTMTList();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
