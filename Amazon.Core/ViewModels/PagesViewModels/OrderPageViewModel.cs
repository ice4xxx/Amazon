using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Entities;
using Amazon.Core.ViewModels.Base;
using DI;
using DI.Enums;
using Newtonsoft.Json;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class OrderPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Current order.
        /// </summary>
        private readonly IOrder _order;

        /// <summary>
        /// Return orders number string.
        /// </summary>
        public string Id => $"Номер заказа: {_order.Id}";

        /// <summary>
        /// True if page open in user mode.
        /// </summary>
        public bool IsUserOwner => !IsSellerOwner;

        /// <summary>
        /// True if page open in seller mode.
        /// </summary>
        public bool IsSellerOwner => ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser.IsSeller;

        /// <summary>
        /// Is pay button clickable and visible.
        /// </summary>
        public bool IsPayButtonVisible =>
            ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) & OrderStatus.Paid) == 0 &&
            ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) & OrderStatus.Processed) != 0;


        /// <summary>
        /// Is order button clickable and visible.
        /// </summary>
        public bool IsOrderPaid => !IsPayButtonVisible &&
                                   ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) &
                                    OrderStatus.Processed) != 0;

        /// <summary>
        /// Is process order button clickable and visible.
        /// </summary>
        public bool IsProcessOrderButtonVisible =>
            ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) & OrderStatus.Processed) == 0;

        /// <summary>
        /// Is complete button clickable and visible.
        /// </summary>
        public bool IsCompleteButtonVisible =>
            ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) & OrderStatus.Completed) == 0;

        /// <summary>
        /// Is ship button clickable and visible.
        /// </summary>
        public bool IsShipButtonVisible => ((OrderStatus)Enum.Parse(typeof(OrderStatus), _order.Status) & OrderStatus.Shipped) == 0;

        public bool IsOrderProcessed => !IsProcessOrderButtonVisible;

        public bool IsOrderShipped => !IsShipButtonVisible;

        public bool IsOrderCompleted => !IsCompleteButtonVisible;


        /// <summary>
        /// List of products.
        /// </summary>
        public List<IOrderProduct> Products
        {
            get
            {
                try
                {
                    return JsonConvert
                        .DeserializeObject<List<ProductEntity>>(_order.ProductsJson)?.Select(i => i as IOrderProduct)
                        .ToList();
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Данные повреждены!");
                }

                return null;
            }
        }

        /// <summary>
        /// Processes order.
        /// </summary>
        public ICommand ProcessOrderCommand { get; set; }

        /// <summary>
        /// Pays order.
        /// </summary>
        public ICommand PayCommand { get; set; }

        /// <summary>
        /// Ships order.
        /// </summary>
        public ICommand ShipCommand { get; set; }

        /// <summary>
        /// Completes order.
        /// </summary>
        public ICommand CompleteCommand { get; set; }

        public OrderPageViewModel(IOrder order)
        {
            _order = order;
        }

        protected override void SetupCommands()
        {
            ProcessOrderCommand = new RelayCommand(() =>
            {
                SetOrderStatus(OrderStatus.Processed);
                UpdateVisibility();
            });

            PayCommand = new RelayCommand(() =>
            {
                SetOrderStatus(OrderStatus.Processed | OrderStatus.Paid);
                UpdateVisibility();
            });

            ShipCommand = new RelayCommand(() =>
            {
                SetOrderStatus(OrderStatus.Processed | OrderStatus.Paid | OrderStatus.Shipped);
                UpdateVisibility();
            });

            CompleteCommand = new RelayCommand(() =>
            {
                SetOrderStatus(OrderStatus.Processed | OrderStatus.Paid | OrderStatus.Shipped | OrderStatus.Completed);
                UpdateVisibility();
            });
        }

        /// <summary>
        /// Sets new order status.
        /// </summary>
        /// <param name="status"></param>
        private void SetOrderStatus(OrderStatus status)
        {
            try
            {
                ApplicationViewModel.Container.GetInstance<IOrderMemory>().SetOrderStatusById(_order.Id, status);
            }
            catch (Exception e)
            {
                ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                return;
            }
            _order.Status = status.ToString();
        }

        /// <summary>
        /// Updates visibility values.
        /// </summary>
        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsProcessOrderButtonVisible));
            OnPropertyChanged(nameof(IsOrderProcessed));
            OnPropertyChanged(nameof(IsPayButtonVisible));
            OnPropertyChanged(nameof(IsOrderPaid));
            OnPropertyChanged(nameof(IsShipButtonVisible));
            OnPropertyChanged(nameof(IsOrderShipped));
            OnPropertyChanged(nameof(IsCompleteButtonVisible));
            OnPropertyChanged(nameof(IsOrderCompleted));
        }
    }
}
