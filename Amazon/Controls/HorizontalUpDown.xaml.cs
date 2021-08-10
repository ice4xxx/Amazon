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

namespace Amazon.Controls
{
    /// <summary>
    /// Логика взаимодействия для HorizontalUpDown.xaml
    /// </summary>
    public partial class HorizontalUpDown : UserControl
    {

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(HorizontalUpDown), new PropertyMetadata(1));

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public HorizontalUpDown()
        {
            InitializeComponent();
        }


        private void Minus(object sender, RoutedEventArgs e)
        {
            if (Count > 0)
            {
                Count--;
            }
        }

        private void Plus(object sender, RoutedEventArgs e)
        {
            if (Count < int.MaxValue)
            {
                Count++;
            }
        }
    }
}
