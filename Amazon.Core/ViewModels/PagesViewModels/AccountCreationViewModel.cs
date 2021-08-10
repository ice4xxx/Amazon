using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
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
    public class AccountCreationViewModel : BaseViewModel
    {
        #region PrivateMembers

        /// <summary>
        /// Service that provides file input.
        /// </summary>
        private IInputFileService _inputFileService;

        /// <summary>
        /// Name of the new user.
        /// </summary>
        private string _name;

        /// <summary>
        /// Surname of the new use.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Patronymic of the new user. 
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Phone number of the new user.
        /// </summary>
        private string _phone;


        /// <summary>
        /// Address of the new user.
        /// </summary>
        private string _address;


        /// <summary>
        /// Email of the new user.
        /// </summary>
        private string _email;
        
        /// <summary>
        /// Password of the new user.
        /// </summary>
        private SecureString _password;
        #endregion

        #region Public Properties

        /// <summary>
        /// User image.
        /// </summary>
        public IImage Image { get; set; }

        /// <summary>
        /// Name of the new user.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }


        /// <summary>
        /// Surname of the new user.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        /// <summary>
        /// Patronymic of the new user.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        /// <summary>
        /// Phone number of the new user.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        /// <summary>
        /// Address of the new user.
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }
        
        /// <summary>
        /// Email of the new user.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        /// <summary>
        /// Password of the new user.
        /// </summary>
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        /// <summary>
        /// Is create button clickable and visible.
        /// </summary>
        public bool IsCreateButtonEnabled => TextValidation.NameValidation(_name) && TextValidation.NameValidation(_surname) &&
                                             TextValidation.NameValidation(_patronymic) && TextValidation.AddressValidation(_address) &&
                                             TextValidation.PhoneValidation(_phone) &&
                                            TextValidation.EmailValidation(_email) && _password?.Length > 0;

        #endregion

        #region Commands

        /// <summary>
        /// Gets image.
        /// </summary>
        public ICommand GetImageCommand { get; set; }

        /// <summary>
        /// Adds user.
        /// </summary>
        public ICommand AddUserCommand { get; set; }
        
        /// <summary>
        /// Returns back to login page.
        /// </summary>
        public ICommand BackCommand { get; set; }
        #endregion


        /// <summary>
        /// ctor.
        /// </summary>
        public AccountCreationViewModel()
        {
            _inputFileService = ApplicationViewModel.Container.GetInstance<IInputFileService>();
            Image = ApplicationViewModel.Container.GetInstance<IImage>();
        }

        #region Helpers

        protected override void SetupCommands()
        {
            BackCommand = new RelayCommand(() =>
            {
                ApplicationViewModel.CurrentPage = PageType.LoginPage;
            });

            GetImageCommand = new RelayCommand(() =>
            {
                Image.Base64Image = _inputFileService.GetFile("image files(*png)|*.png");
                OnPropertyChanged(nameof(Image));
            });

            AddUserCommand = new RelayCommand(() =>
            {
                var memory = ApplicationViewModel.Container.GetInstance<IUserMemory>();
                var user = ApplicationViewModel.Container.GetInstance<IUser>();

                user.Name = Name;
                user.Surname = Surname;
                user.Patronymic = Patronymic;
                user.PhoneNumber = Phone;
                user.Address = Address;
                user.Email = Email;
                user.Base64Image = Image.Base64Image;
                user.Password = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(Password));
                user.IsSeller = false;

                try
                {
                    memory.AddItem(user);
                    ApplicationViewModel.CurrentPage = PageType.LoginPage;
                }
                catch (DbUpdateException)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>()
                        .Show("Пользователь с таким email уже зарегистрирован");
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
