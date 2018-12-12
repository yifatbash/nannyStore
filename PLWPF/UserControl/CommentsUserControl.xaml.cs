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
    /// Interaction logic for CommentsUserControl.xaml
    /// </summary>
    public partial class CommentsUserControl : UserControl
    {
        public CommentsUserControl()
        {
            InitializeComponent();
            this.CommentsTextBlock.DataContext = this;
        }
        
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CommentsUserControl), new PropertyMetadata(""));

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommentTextBox.Text != "")
            {
                if (CommentsTextBlock.Text != "")
                    this.CommentsTextBlock.Text += "\n";
                this.CommentsTextBlock.Text += CommentTextBox.Text;
                this.CommentTextBox.Text = "";
                Text = CommentsTextBlock.Text;
            }
        }

    }
}
