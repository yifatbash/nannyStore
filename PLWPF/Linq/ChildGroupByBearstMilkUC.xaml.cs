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
    /// Interaction logic for ChildGroupByBearstMilkUC.xaml
    /// </summary>
    public partial class ChildGroupByBearstMilkUC : UserControl
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

        public int NannyId
        {
            get { return (int)GetValue(NannyIdProperty); }
            set { SetValue(NannyIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MotherId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NannyIdProperty =
            DependencyProperty.Register("NannyId", typeof(int), typeof(ChildGroupByBearstMilkUC), new PropertyMetadata(0));



        public ChildGroupByBearstMilkUC()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();

            this.nannyComboBox.ItemsSource = bl.GetNannyList();
            this.nannyComboBox.DisplayMemberPath = "FirstName";
            this.nannyComboBox.SelectedValuePath = "Id";

        }

        private void nannyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.nannyComboBox.SelectedItem is BE.Nanny)
            {
                int id = (int)this.nannyComboBox.SelectedValue;
                Source = bl.GroupChildrenByBreastMilk(id);
            }
        }
    }
}
