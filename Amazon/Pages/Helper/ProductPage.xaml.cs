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
using Amazon.Core.TextsValidations;

namespace Amazon.Pages.Helper
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : BaseHelperPage
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (TextValidation.AddressValidation(textBox.Text))
            {
                textBox.Foreground = Application.Current.FindResource("WhiteBrush") as SolidColorBrush;
            }
            else
            {
                textBox.Foreground = Application.Current.FindResource("ErrorBrush") as SolidColorBrush;
            }
        }
    }
}
