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
    /// Interaction logic for NumericUpDownControrl.xaml
    /// </summary>
    public partial class NumericUpDownControl : UserControl
    {
        public NumericUpDownControl()
        {
            InitializeComponent();
            MaxValue = 30;
        }

        public int MaxValue { get; set; }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUpDownControl), new PropertyMetadata(0));

        public float? Value
        {
            get { return (float?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(float?), typeof(NumericUpDownControl), new PropertyMetadata(null,PropertyChangedCallBack, ValueCoerceValueCallBack));

        public static object ValueCoerceValueCallBack(DependencyObject d, object baseValue)
        {
            float? value = baseValue as float?;
            NumericUpDownControl THIS = d as NumericUpDownControl;

            if (value > THIS.MaxValue)
                return (float?) THIS.MaxValue;
            else if (value < THIS.MinValue)
                return (float?) THIS.MinValue;
            else
                return value;
        }

        public static void PropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownControl THIS = d as NumericUpDownControl;
            THIS.textNumber.Text = THIS.Value == null ? "" : THIS.Value.ToString();
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textNumber == null || textNumber.Text == "-")
                return;

            float val;
            if (!float.TryParse(textNumber.Text, out val))
                textNumber.Text = Value.ToString();
            else
                Value = val;
        }
    }
}

