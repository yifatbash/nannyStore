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
    /// Interaction logic for ShowAllUserControl.xaml
    /// </summary>
    public partial class ShowAllUserControl : UserControl
    {
        private IEnumerable source;

        public IEnumerable Source
        {
            get { return source; }
            set
            {
                source = value;
                this.listView.ItemsSource = source;
            }
        }

        public string MyTitle
        {
            get { return (string)GetValue(MyTitleProperty); }
            set { SetValue(MyTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTitleProperty =
            DependencyProperty.Register("MyTitle", typeof(string), typeof(GroupByUserControl), new PropertyMetadata(""));

        public ShowAllUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
