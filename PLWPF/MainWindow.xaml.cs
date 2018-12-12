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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            bl.Init();
        }

        private void NannyButton_Click(object sender, RoutedEventArgs e)
        {
            new NannyWindow().Show();
        }

        private void MotherButton_Click(object sender, RoutedEventArgs e)
        {
            new MotherWindow().Show();
        }

        private void ChildButton_Click(object sender, RoutedEventArgs e)
        {
            new ChildWindow().Show();
        }

        private void ContractButton_Click(object sender, RoutedEventArgs e)
        {
            new ContractWindow().Show();
        }
        
        private void LinqButton_Click(object sender, RoutedEventArgs e)
        {
            new LinqWindow().Show();
        }

    }
}
