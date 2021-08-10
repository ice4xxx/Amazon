using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.ViewModels.Base;
using DI;

namespace Amazon.Core.ViewModels.Controls
{
    public class CartListViewModel : BaseViewModel
    {
        /// <summary>
        /// Cart of this application.
        /// </summary>
        public ObservableCollection<ICartProduct> Cart { get; set; } = new ObservableCollection<ICartProduct>(ApplicationViewModel.Cart.Products);

        /// <summary>
        /// ctor.
        /// </summary>
        public CartListViewModel()
        {
            //Updates cart.
            ApplicationViewModel.Cart.CartChanged += () =>
            {
                Cart = new ObservableCollection<ICartProduct>(ApplicationViewModel.Cart.Products);
                OnPropertyChanged(nameof(Cart));
            };
        }
    }
}
