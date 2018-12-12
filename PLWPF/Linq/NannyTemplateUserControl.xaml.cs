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
    /// Interaction logic for NannyTemplateUserControl.xaml
    /// </summary>
    public partial class NannyTemplateUserControl : UserControl
    {
        public NannyTemplateUserControl()
        {
            InitializeComponent();
        }

        private void moreDetails_Click(object sender, RoutedEventArgs e)
        {
            object OBJ = this.DataContext;
            MessageBox.Show(
                OBJ.ToString(),
                "Nanny Details");
        }
    }
}
