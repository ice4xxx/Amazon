using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Entities;
using Amazon.Core.ViewModels.Base;
using DI;
using DI.Enums;
using Newtonsoft.Json;

namespace Amazon.Core.ViewModels.Controls
{
    public class CartViewModel : BaseViewModel
    {
        /// <summary>
        /// Cart of the application.
        /// </summary>
        public ICart Cart => ApplicationViewModel.Cart;


        /// <summary>
        /// Count of items in the cart.
        /// </summary>
        public int Count => Cart.Count;

        /// <summary>
        /// Cost of all items in the cart.
        /// </summary>
        public long Cost => Cart.Cost;

        /// <summary>
        /// True if we can process order.
        /// </summary>
        public bool IsProcessOrderButtonVisible => Cart.Products.Count > 0;

        /// <summary>
        /// Command to process order.
        /// </summary>
        public ICommand ProcessOrderCommand { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public CartViewModel()
        {
            //update values.
            Cart.CartChanged += () =>
            {
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Cost));
                OnPropertyChanged(nameof(IsProcessOrderButtonVisible));
            };
            Cart.CountChanged += () =>
            {
                OnPropertyChanged(nameof(Cost));
            };

            ProcessOrderCommand = new RelayCommand(() =>
            {
                var order = ApplicationViewModel.Container.GetInstance<IOrder>();

                order.CustomerEmail = ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser.Email;
                order.Date = DateTime.Now.ToString();
                order.Status = OrderStatus.OnProcessing.ToString();
                order.Cost = Cost;

                order.ProductsJson = JsonConvert.SerializeObject(Cart.Products.Select(i => new ProductEntity
                {
                    Count = i.Count,
                    Base64Image = i.Product.Base64Image,
                    Name = i.Product.Name,
                    Cost = i.Product.Cost,
                    Id = i.Product.Id
                }));

                try
                {
                    ApplicationViewModel.Container.GetInstance<IOrderMemory>().AddItem(order);
                    Cart.ClearCart();
                }
                catch (DbUpdateException)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>()
                        .Show("Возникла непредвиденная ошибка. Повторите позже!");
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }
            });
        }
    }
}
