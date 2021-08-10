using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Enums;
using Amazon.Pages;
using Amazon.Pages.Helper;
using Amazon.Pages.Main;

namespace Amazon.ValueConverters
{
    class PageTypeValueConverter : BaseValueConverter<PageTypeValueConverter>
    {
        /// <summary>
        /// Converts from PageType to BasePage
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pageType = (PageType)value;
            switch (pageType)
            {
                case PageType.AccountCreationPage:
                    return new AccountCreationPage();
                case PageType.LoginPage:
                    return new LoginPage();
                case PageType.MainSellerPage:
                    return new MainSellerPage();
                case PageType.ProductPage:
                    return new ProductPage();
                case PageType.MainUserPage:
                    return new MainUserPage();
                case PageType.UserOrderPage:
                    return new OrderPage();
                case PageType.StorePage:
                    return new StorePage();
                case PageType.UsersOrdersPage:
                    return new UsersOrdersPage();
                default:
                    return null;
            }
        }

        /// <summary>
        /// We dont need it.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
