using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Core.ViewModels.Base;
using DI;

namespace Amazon.Core.ViewModels.Controls
{
    public class StoreItemViewModel : BaseViewModel
    {
        /// <summary>
        /// Current product.
        /// </summary>
        public IProduct Product { get; set; }

        /// <summary>
        /// Adds product in Cart.
        /// </summary>
        public ICommand AddToCartCommand { get; set; }

        protected override void SetupCommands()
        {
            AddToCartCommand = new RelayCommand(() =>
            {
                var product = ApplicationViewModel.Container.GetInstance<ICartProduct>();
                product.Product = Product;
                ApplicationViewModel.Cart.AddProduct(product);
            });
        }
    }
}
