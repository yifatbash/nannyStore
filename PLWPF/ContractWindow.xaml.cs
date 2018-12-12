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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        public ContractWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// when change tab item reload the others- to get "clean" data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            this.RemoveTabItem.Content = null;
            RemoveContractUserControl r = new RemoveContractUserControl();
            this.RemoveTabItem.Content = r;

            this.UpdateTabItem.Content = null;
            UpdateContractUserControl u = new UpdateContractUserControl();
            this.UpdateTabItem.Content = u;
        }

        /// <summary>
        /// when change tab item reload the others- to get "clean" data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            this.AddTabItem.Content = null;
            AddContractUserControl a = new AddContractUserControl();
            this.AddTabItem.Content = a;

            this.RemoveTabItem.Content = null;
            RemoveContractUserControl r = new RemoveContractUserControl();
            this.RemoveTabItem.Content = r;
        }

        /// <summary>
        /// when change tab item reload the others- to get "clean" data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            this.AddTabItem.Content = null;
            AddContractUserControl a = new AddContractUserControl();
            this.AddTabItem.Content = a;

            this.UpdateTabItem.Content = null;
            UpdateContractUserControl u = new UpdateContractUserControl();
            this.UpdateTabItem.Content = u;
        }
    }
}
