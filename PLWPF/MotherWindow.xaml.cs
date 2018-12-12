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
    /// Interaction logic for MotherWindow.xaml
    /// </summary>
    public partial class MotherWindow : Window
    {
        public MotherWindow()
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
            RemoveMotherUserControl r = new RemoveMotherUserControl();
            this.RemoveTabItem.Content = r;

            this.UpdateTabItem.Content = null;
            UpdateMotherUserControl u = new UpdateMotherUserControl();
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
            AddMotherUserControl a = new AddMotherUserControl();
            this.AddTabItem.Content = a;

            this.RemoveTabItem.Content = null;
            RemoveMotherUserControl r = new RemoveMotherUserControl();
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
            AddMotherUserControl a = new AddMotherUserControl();
            this.AddTabItem.Content = a;

            this.UpdateTabItem.Content = null;
            UpdateMotherUserControl u = new UpdateMotherUserControl();
            this.UpdateTabItem.Content = u;
        }
    }
}
