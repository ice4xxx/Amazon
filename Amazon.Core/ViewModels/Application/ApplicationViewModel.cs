using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels.Application;
using Amazon.Core.ViewModels.Base;
using Amzon.Settings;
using DI;
using SimpleInjector;

namespace Amazon.Core.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region CurrentPage
        /// <summary>
        /// Fires when PageType has changed.
        /// </summary>
        public static event EventHandler<PageTypeEventArgs> PageTypeChanged;

        /// <summary>
        /// Current page of window.
        /// </summary>
        private static PageType _currentPage;

        /// <summary>
        /// Sets dependency of container there.
        /// </summary>
        public static Container Container { get; } = new Configuration().Container;

        /// <summary>
        /// Current page of window.
        /// </summary>
        public static PageType CurrentPage
        {
            get => _currentPage;
            set
            {
                PageTypeChanged?.Invoke(null, new PageTypeEventArgs(_currentPage, value));
                _currentPage = value;
            }
        }
        #endregion

        #region Helper Page

        /// <summary>
        /// Fires when helper page has changed.
        /// </summary>
        public static event Action HelperPageChanged;

        /// <summary>
        /// Current helper page.
        /// </summary>
        public static PageType CurrentHelperPage { get; private set; }

        /// <summary>
        /// Current helper page view model.
        /// </summary>
        public static BaseViewModel CurrentHelperViewModel { get; private set; }

        /// <summary>
        /// Sets current page with view model.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        public static void SetCurrentHelperPage(PageType page, BaseViewModel viewModel)
        {
            CurrentHelperPage = page;

            CurrentHelperViewModel = viewModel;

            HelperPageChanged?.Invoke();
        }
        #endregion

        #region Cart

        /// <summary>
        /// Cart of the application.
        /// </summary>
        public static ICart Cart { get; set; }

        #endregion


        /// <summary>
        /// Static ctor.
        /// </summary>
        static ApplicationViewModel()
        {
            _currentPage = PageType.LoginPage;
            Cart = Container.GetInstance<ICart>();
            PageTypeChanged?.Invoke(null,new PageTypeEventArgs(_currentPage,_currentPage));
        }
    }
}
