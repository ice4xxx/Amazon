using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Entities;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.PagesViewModels;
using DI;
using Newtonsoft.Json;

namespace Amazon.Core.ViewModels.Controls
{
    public class OrderViewModel : BaseViewModel
    {

        /// <summary>
        /// False if order is now open.
        /// </summary>
        private bool _isClickable = true;

        /// <summary>
        /// Current order.
        /// </summary>
        public IOrder Order { get; set; }

        /// <summary>
        /// True if now application opened as seller.
        /// </summary>
        public bool IsSellerOwner => ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser.IsSeller;

        /// <summary>
        /// Shows products in order.
        /// </summary>
        public ICommand ShowOrderProductsCommand { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public OrderViewModel(IOrder order)
        {
            Order = order;
            ApplicationViewModel.HelperPageChanged += () => _isClickable = true;
        }

        protected override void SetupCommands()
        {
            ShowOrderProductsCommand = new RelayCommand(() =>
            {
                if (_isClickable)
                {
                    ApplicationViewModel.SetCurrentHelperPage(PageType.UserOrderPage,
                        new OrderPageViewModel(Order));
                    _isClickable = false;
                }
            });
        }
    }
}
