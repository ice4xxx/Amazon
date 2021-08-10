
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Entities;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.Controls;
using CsvHelper;
using DI;
using DI.Enums;
using Newtonsoft.Json;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class MainSellerPageViewModel : BaseViewModel
    {
        #region Private members

        /// <summary>
        /// Product memory.
        /// </summary>
        private readonly IData<IProduct> _productData = ApplicationViewModel.Container.GetInstance<IData<IProduct>>();


        /// <summary>
        /// Order memory.
        /// </summary>
        private readonly IOrderMemory _orderData = ApplicationViewModel.Container.GetInstance<IOrderMemory>();

        /// <summary>
        /// User memory.
        /// </summary>
        private readonly IUserMemory _userData = ApplicationViewModel.Container.GetInstance<IUserMemory>();

        /// <summary>
        /// Service to show error message.
        /// </summary>
        private readonly IErrorMessage _errorMessage = ApplicationViewModel.Container.GetInstance<IErrorMessage>();

        /// <summary>
        /// Service to chose folder.
        /// </summary>
        private readonly IFolderService _folderService = ApplicationViewModel.Container.GetInstance<IFolderService>();

        /// <summary>
        /// List of orders.
        /// </summary>
        private ObservableCollection<OrderViewModel> _orders = new ObservableCollection<OrderViewModel>();

        #endregion

        #region Public Members

        /// <summary>
        /// List of products.
        /// </summary>
        public ObservableCollection<IProduct> Products
        {
            get
            {
                try
                {
                    return new ObservableCollection<IProduct>(_productData.GetAll());
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }

                return null;
            }
        }

        /// <summary>
        /// List of orders.
        /// </summary>
        public ObservableCollection<OrderViewModel> Orders
        {
            get => new ObservableCollection<OrderViewModel>(_orders);
            private set => _orders = value;
        }

        /// <summary>
        /// List of users.
        /// </summary>
        public ObservableCollection<UserViewModel> Users =>
            new ObservableCollection<UserViewModel>(ApplicationViewModel.Container.GetInstance<IUserMemory>().GetAll()
                .Where(i => !i.IsSeller)
                .Select(i => new UserViewModel(i)));

        /// <summary>
        /// Indicator to animate page changing.
        /// </summary>
        public bool IsPageChanging { get; set; }

        /// <summary>
        /// Current helper page.
        /// </summary>
        public PageType CurrentHelperPage { get; set; }

        /// <summary>
        /// True if now we need to show product list.
        /// </summary>
        public bool IsProductListVisible { get; set; } = true;

        /// <summary>
        /// True if now we need to show order list.
        /// </summary>
        public bool IsOrderListVisible { get; set; } = false;

        /// <summary>
        /// True if now we need to show users list.
        /// </summary>
        public bool IsUserListVisible { get; set; } = false;

        /// <summary>
        /// True if we need to open report bubble.
        /// </summary>
        public bool IsReportBubbleOpen { get; set; } = false;

        /// <summary>
        /// Total sum to make report.
        /// </summary>
        public string ReportSum { get; set; }

        /// <summary>
        /// Id of product to make report.
        /// </summary>
        public string ReportId { get; set; }
        #endregion


        #region Public Commands

        /// <summary>
        /// Opens report bubble
        /// </summary>
        public ICommand OpenReportBubbleCommand { get; set; }

        /// <summary>
        /// Shows product list.
        /// </summary>
        public ICommand ShowProductListCommand { get; set; }

        /// <summary>
        /// Shows order list.
        /// </summary>
        public ICommand ShowOrderListCommand { get; set; }

        /// <summary>
        /// Shows user list.
        /// </summary>
        public ICommand ShowUserListCommand { get; set; }

        /// <summary>
        /// Shows only active orders in order list.
        /// </summary>
        public ICommand ShowOnlyActiveOrderCommand { get; set; }

        /// <summary>
        /// Makes sum report.
        /// </summary>
        public ICommand MakeSumReportCommand { get; set; }

        /// <summary>
        /// Makes id report.
        /// </summary>
        public ICommand MakeIdReportCommand { get; set; }

        #endregion

        public MainSellerPageViewModel()
        {
            //updates value if collections has changed.

            _productData.CollectionChanged += () => OnPropertyChanged(nameof(Products));

            ApplicationViewModel.HelperPageChanged += () =>
            {
                CurrentHelperPage = ApplicationViewModel.CurrentHelperPage;
                OnPropertyChanged(nameof(CurrentHelperPage));
            };

            _orderData.OrderStatusChanged += order => OnPropertyChanged(nameof(Orders));

            CurrentHelperPage = ApplicationViewModel.CurrentHelperPage;

        }

        protected override void SetupCommands()
        {
            MakeSumReportCommand = new RelayCommand(() =>
            {
                if (!TextsValidations.TextValidation.DigitsValidation(ReportSum))
                {
                    _errorMessage.Show("Введите корректную сумму");
                    return;
                }

                if (!long.TryParse(ReportSum, out long sum) || sum < 0)
                {
                    _errorMessage.Show($"Введите неотрицательную сумму меньше {long.MaxValue}");
                    return;
                }

                string path = _folderService.ChoseFolder();

                if (path == null)
                {
                    return;
                }

                try
                {
                    var users = _userData.GetAll()
                        .Where(i => _orderData.GetOrdersByUserEmail(i.Email).Sum(j => j.Cost) > sum);
                    SaveReport(GetFileNameForCsvReport(path, "SumReport"), users);
                }
                catch (DbUpdateException)
                {
                    _errorMessage.Show("Возникла непредвиденная ошибка. Повторите позже!");
                }
                catch (Exception e)
                {
                    _errorMessage.Show("Сервер спит!");
                }
            });

            MakeIdReportCommand = new RelayCommand(() =>
            {
                if (!TextsValidations.TextValidation.DigitsValidation(ReportId))
                {
                    _errorMessage.Show("Введите корректный Id");
                    return;
                }

                if (!long.TryParse(ReportId, out long id) || id < 0)
                {
                    _errorMessage.Show($"Введите неотрицательный Id меньше {long.MaxValue}");
                    return;
                }

                string path = _folderService.ChoseFolder();

                if (path == null)
                {
                    return;
                }

                try
                {
                    var users = _orderData.GetAll().Where(i => IsContainsProductById(id, i.ProductsJson))
                        .Select(i => _userData.GetUserByEmail(i.CustomerEmail));
                    SaveReport(GetFileNameForCsvReport(path, "IdReport"), UniqueUsers(users));
                }
                catch (DbUpdateException)
                {
                    _errorMessage.Show("Возникла непредвиденная ошибка. Повторите позже!");
                }
                catch (Exception e)
                {
                    _errorMessage.Show("Сервер спит!");
                }
            });

            ShowProductListCommand = new RelayCommand(async () =>
            {
                IsUserListVisible = false;
                IsOrderListVisible = false;
                IsProductListVisible = true;
                IsPageChanging = true;
                await Task.Delay(TimeSpan.FromSeconds(0.2));
                IsPageChanging = false;
                ApplicationViewModel.SetCurrentHelperPage(PageType.ProductPage, new ProductCreationPageViewModel());
            });

            ShowOrderListCommand = new RelayCommand(() =>
            {
                try
                {
                    SetOrdersList(new ObservableCollection<OrderViewModel>(_orderData.GetAll().Select(i => new OrderViewModel(i))));
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                    return;
                }
            });

            ShowOnlyActiveOrderCommand = new RelayCommand(() =>
            {
                SetOrdersList(new ObservableCollection<OrderViewModel>(_orderData.GetAll().Where(i => ((OrderStatus)Enum.Parse(typeof(OrderStatus), i.Status) & OrderStatus.Completed) == 0).Select(i => new OrderViewModel(i))));
            });

            ShowUserListCommand = new RelayCommand(async () =>
            {
                IsOrderListVisible = false;
                IsProductListVisible = false;
                IsUserListVisible = true;
                await Task.Delay(TimeSpan.FromSeconds(0.2));
                IsPageChanging = false;
                if (Users.Count > 0)
                {
                    ApplicationViewModel.SetCurrentHelperPage(PageType.UsersOrdersPage, new UsersOrdersPageViewModel(Users[0].User));
                }
            });

            OpenReportBubbleCommand = new RelayCommand(() =>
            {
                IsReportBubbleOpen ^= true;
            });
        }


        /// <summary>
        /// Returns list with unique users.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private List<IUser> UniqueUsers(IEnumerable<IUser> users)
        {
            List<IUser> usersList = new List<IUser>();

            foreach (var user in users)
            {
                if (!usersList.Contains(user, ApplicationViewModel.Container.GetInstance<IEqualityComparer<IUser>>()))
                {
                    usersList.Add(user);
                }
            }

            return usersList;
        }

        /// <summary>
        /// Saves report.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="users"></param>
        private void SaveReport(string path, IEnumerable<IUser> users)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (CsvWriter csvWriter = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(users);
                }
            }
            catch (Exception e)
            {
                _errorMessage.Show("Не удалось сохранить отчет.");
            }
        }

        /// <summary>
        /// Returns free file name to csv file.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetFileNameForCsvReport(string folderPath, string name)
        {
            for (int i = 0; ; i++)
            {
                string tmpPath = folderPath + Path.DirectorySeparatorChar + $"{i}-{name}.csv";
                if (!File.Exists(tmpPath))
                {
                    return tmpPath;
                }
            }
        }

        /// <summary>
        /// Returns true if order contains product.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="jsonProducts">Products in json format.</param>
        /// <returns></returns>
        private bool IsContainsProductById(long id, string jsonProducts)
        {
            List<IProduct> products = null;

            try
            {
                products = JsonConvert.DeserializeObject<List<ProductEntity>>(jsonProducts)?.Select(i => i as IProduct).ToList();
            }
            catch (Exception e)
            {
                return false;
            }

            if (products == null)
            {
                return false;
            }

            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets order list.
        /// </summary>
        /// <param name="list"></param>
        private async void SetOrdersList(ObservableCollection<OrderViewModel> list)
        {
            Orders = list;

            IsUserListVisible = false;
            IsProductListVisible = false;
            IsOrderListVisible = true;
            IsPageChanging = true;
            await Task.Delay(TimeSpan.FromSeconds(0.3));
            IsPageChanging = false;

            if (Orders.Count > 0)
            {
                ApplicationViewModel.SetCurrentHelperPage(PageType.UserOrderPage,
                    new OrderPageViewModel(Orders[0]?.Order));
            }
            else
            {

                ApplicationViewModel.SetCurrentHelperPage(PageType.None, null);
            }
        }
    }
}
