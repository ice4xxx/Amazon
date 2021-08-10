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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Amazon.Core.ViewModels;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.Controls;
using DI;

namespace Amazon.Controls
{
    /// <summary>
    /// Логика взаимодействия для CartBubble.xaml
    /// </summary>
    public partial class CartBubble : UserControl
    {

        public CartBubble()
        {
            DataContext = new CartViewModel();
            InitializeComponent();
        }
    }
}
