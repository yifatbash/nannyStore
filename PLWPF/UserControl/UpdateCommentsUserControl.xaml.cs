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
    /// Interaction logic for UpdateCommentsUserControl.xaml
    /// </summary>
    public partial class UpdateCommentsUserControl : UserControl
    {
        public UpdateCommentsUserControl()
        {
            try
            {
                InitializeComponent();
                this.CommentsTextBox.DataContext = this;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(UpdateCommentsUserControl), new PropertyMetadata(""));

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommentTextBox.Text != "")
            {
                if (CommentsTextBox.Text != "")
                    this.CommentsTextBox.Text += "\n";
                this.CommentsTextBox.Text += CommentTextBox.Text;
                this.CommentTextBox.Text = "";
                //Text = CommentsTextBox.Text;
            }
        }

    }
}
