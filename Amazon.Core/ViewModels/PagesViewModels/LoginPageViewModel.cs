using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.Enums;
using Amazon.Core.TextsValidations;
using Amazon.Core.ViewModels.Base;
using DI;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Private members

        /// <summary>
        /// Password.
        /// </summary>
        private SecureString _securePassword;

        /// <summary>
        /// Email.
        /// </summary>
        private string _email;

        #endregion


        #region Public properties


        /// <summary>
        /// Is login button clickable and visible.
        /// </summary>
        public bool IsLoginButtonEnabled => _securePassword?.Length > 0 && TextValidation.EmailValidation(_email);

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(IsLoginButtonEnabled));
            }
        }

        /// <summary>
        /// Password of the user.
        /// </summary>
        public SecureString SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(IsLoginButtonEnabled));
            }
        }

        #endregion

        #region Commands
        
        /// <summary>
        /// Directs to account creation page.
        /// </summary>
        public ICommand CreateAccountCommand { get; set; }

        /// <summary>
        /// Signs in user.
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        public LoginPageViewModel()
        {

        }


        #region Helpers

        protected override void SetupCommands()
        {
            CreateAccountCommand = new RelayCommand(() =>
            {
                ApplicationViewModel.CurrentPage = PageType.AccountCreationPage;
            });

            LoginCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(Email) || SecurePassword == null)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Введите email и пароль");
                    return;
                }

                var dataBase = ApplicationViewModel.Container.GetInstance<IUserMemory>();

                try
                {
                    dataBase.LoginUser(Email,
                        Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(SecurePassword)));

                    if (dataBase.LoggedInUser.IsSeller)
                    {
                        ApplicationViewModel.CurrentPage = PageType.MainSellerPage;
                        ApplicationViewModel.SetCurrentHelperPage(PageType.ProductPage,
                            new ProductCreationPageViewModel());
                    }
                    else
                    {
                        ApplicationViewModel.CurrentPage = PageType.MainUserPage;
                    }
                }
                catch (ArgumentException)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Неверный email или пароль");
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }
            });
        }

        #endregion
    }
}
