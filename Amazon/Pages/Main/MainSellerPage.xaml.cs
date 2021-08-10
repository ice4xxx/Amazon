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
using Amazon.Core.ViewModels.PagesViewModels;

namespace Amazon.Pages.Main
{
    /// <summary>
    /// Логика взаимодействия для MainSellerPage.xaml
    /// </summary>
    public partial class MainSellerPage : BasePage
    {
        public MainSellerPage()
        {
            InitializeComponent();

            DataContext = new MainSellerPageViewModel();
        }
    }
}
