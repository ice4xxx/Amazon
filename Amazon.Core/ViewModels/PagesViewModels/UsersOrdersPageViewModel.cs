using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.Controls;
using DI;
using DI.Enums;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class UsersOrdersPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Current user.
        /// </summary>
        private readonly IUser _user;

        /// <summary>
        /// Order memory.
        /// </summary>
        private readonly IOrderMemory _orderMemory;

        /// <summary>
        /// List of user's  orders.
        /// </summary>
        public ObservableCollection<OrderViewModel> Orders
        {
            get
            {
                try
                {
                    return new ObservableCollection<OrderViewModel>(_orderMemory.GetOrdersByUserEmail(_user.Email).Select(i => new OrderViewModel(i)));
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }

                return null;
            }
        }

        /// <summary>
        /// Returns user name string.
        /// </summary>
        public string UserName => $"{_user.Surname} {_user.Name} {_user.Patronymic} {_user.Email}";

        /// <summary>
        /// Returns total paid cost of all orders.
        /// </summary>
        public string TotalCost =>
            $"Суммарная оплаченная стоимость: {Orders.Where(i => ((OrderStatus) Enum.Parse(typeof(OrderStatus), i.Order.Status) & OrderStatus.Paid) != 0).Sum(i => i.Order.Cost)}";

        public UsersOrdersPageViewModel(IUser user)
        {
            _user = user;
            _orderMemory = ApplicationViewModel.Container.GetInstance<IOrderMemory>();
            _orderMemory.OrderStatusChanged += order => OnPropertyChanged(nameof(Orders));
        }
    }
}
