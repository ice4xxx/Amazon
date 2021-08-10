using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.ViewModels;

namespace Amazon.Pages
{
    public class BaseHelperPage : BasePage
    {
        public BaseHelperPage()
        {
            DataContext = ApplicationViewModel.CurrentHelperViewModel;
        }
    }
}
