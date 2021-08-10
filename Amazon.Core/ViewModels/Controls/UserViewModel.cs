using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.PagesViewModels;
using DI;

namespace Amazon.Core.ViewModels.Controls
{
    public class UserViewModel : BaseViewModel
    {
        /// <summary>
        /// True if user page of current user is not opened.
        /// </summary>
        private bool _isClickable = true;

        /// <summary>
        /// Current user.
        /// </summary>
        public IUser User { get; set; }

        /// <summary>
        /// Shows all user's orders.
        /// </summary>
        public ICommand ShowUsersOrdersProductsCommand { get; set; }

        public UserViewModel(IUser user)
        {
            User = user;
            ApplicationViewModel.HelperPageChanged += () => _isClickable = true;
        }

        protected override void SetupCommands()
        {
            ShowUsersOrdersProductsCommand = new RelayCommand(() =>
            {
                if (_isClickable)
                {
                    ApplicationViewModel.SetCurrentHelperPage(PageType.UsersOrdersPage, new UsersOrdersPageViewModel(User));
                    _isClickable = false;
                }
            });
        }
    }
}
