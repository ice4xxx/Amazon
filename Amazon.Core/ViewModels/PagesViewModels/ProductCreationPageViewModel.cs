using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.ViewModels.Base;
using DI;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class ProductCreationPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Name of current product.
        /// </summary>
        private string _name;

        /// <summary>
        /// Iamge of current product.
        /// </summary>
        public IImage Image { get; set; }


        /// <summary>
        /// Name of current product.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(IsAddButtonEnabled));
            }
        }
        
        /// <summary>
        /// Cost of current product.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Is add button clickable and visible.
        /// </summary>
        public bool IsAddButtonEnabled => TextsValidations.TextValidation.AddressValidation(_name);


        /// <summary>
        /// Adds new product.
        /// </summary>
        public ICommand AddProductCommand { get; set; }

        /// <summary>
        /// Loads image of new product.
        /// </summary>
        public ICommand LoadImageCommand { get; set; }

        public ProductCreationPageViewModel()
        {
            Image = ApplicationViewModel.Container.GetInstance<IImage>();
            
        }

        protected override void SetupCommands()
        {
            AddProductCommand = new RelayCommand(() =>
            {
                IProduct product = ApplicationViewModel.Container.GetInstance<IProduct>();
                product.Base64Image = Image.Base64Image;
                product.Name = Name;
                product.Cost = Cost;

                try
                {
                    ApplicationViewModel.Container.GetInstance<IData<IProduct>>().AddItem(product);
                }
                catch (DbUpdateException)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>()
                        .Show("Возникла непредвиденная ошибка! Повторите позже!");
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }
            });

            LoadImageCommand = new RelayCommand(() =>
            {
                Image.Base64Image = ApplicationViewModel.Container.GetInstance<IInputFileService>()
                    .GetFile("image files (*.png)|*.png");
                OnPropertyChanged(nameof(Image));
            });
        }
    }
}
