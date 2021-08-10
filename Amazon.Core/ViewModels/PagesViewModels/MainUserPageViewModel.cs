using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.Controls;
using DI;
using DI.Enums;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class MainUserPageViewModel : BaseViewModel
    {

        #region Public memners

        /// <summary>
        /// Current cart image.
        /// </summary>
        public string CartImage => ApplicationViewModel.Cart.Products.Count > 0 ? "\uf218" : "\uf07a";

        /// <summary>
        /// True if we ned to open cart.
        /// </summary>
        public bool IsCartOpened { get; set; }

        /// <summary>
        /// True if we need to open user bubble.
        /// </summary>
        public bool IsUserOpened { get; set; }

        /// <summary>
        /// True if we need to change page.
        /// </summary>
        public bool IsPageChanging { get; set; }

        /// <summary>
        /// True if back button  clickable and visible. 
        /// </summary>
        public bool IsBackButtonVisible => CurrentHelperPage != PageType.StorePage;

        /// <summary>
        /// Current helper page.
        /// </summary>
        public PageType CurrentHelperPage => ApplicationViewModel.CurrentHelperPage;

        /// <summary>
        /// List of user's orders.
        /// </summary>
        public ObservableCollection<OrderViewModel> Orders
        {
            get
            {
                try
                {
                    return new ObservableCollection<OrderViewModel>(ApplicationViewModel.Container.GetInstance<IOrderMemory>()
                        .GetOrdersByUserEmail(ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser.Email).Select(i => new OrderViewModel(i)));
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }

                return null;
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Shows cart.
        /// </summary>
        public ICommand ShowCartCommand { get; set; }

        /// <summary>
        /// Shows user bubble.
        /// </summary>
        public ICommand ShowUserBubbleCommand { get; set; }

        /// <summary>
        /// Directs to store page.
        /// </summary>
        public ICommand BackCommand { get; set; }

        #endregion

        public MainUserPageViewModel()
        {
            //updates values.

            ApplicationViewModel.Cart.CartChanged += () => { OnPropertyChanged(nameof(CartImage)); };

            var orderMemory = ApplicationViewModel.Container.GetInstance<IOrderMemory>();

            orderMemory.CollectionChanged +=
                () => { OnPropertyChanged(nameof(Orders)); };
            ApplicationViewModel.HelperPageChanged +=async () =>
            {
                OnPropertyChanged(nameof(IsBackButtonVisible));
                IsPageChanging = true;
                await Task.Delay(TimeSpan.FromSeconds(0.3));
                IsPageChanging = false;
                OnPropertyChanged(nameof(CurrentHelperPage));
            };

            orderMemory.OrderStatusChanged += order => OnPropertyChanged(nameof(Orders));

            ApplicationViewModel.SetCurrentHelperPage(PageType.StorePage, new StorePageViewModel());
        }

        protected override void SetupCommands()
        {
            ShowCartCommand = new RelayCommand(async () =>
            {
                if (IsUserOpened)
                {
                    IsUserOpened = false;
                    await Task.Delay(TimeSpan.FromSeconds(0.3));
                }

                IsCartOpened ^= true;
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            });

            ShowUserBubbleCommand = new RelayCommand(async () =>
            {
                if (IsCartOpened)
                {
                    IsCartOpened = false;
                    await Task.Delay(TimeSpan.FromSeconds(0.3));
                }

                IsUserOpened ^= true;
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            });

            BackCommand = new RelayCommand(() =>
            {
                ApplicationViewModel.SetCurrentHelperPage(PageType.StorePage, new StorePageViewModel());
            });
        }
    }
}
