using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface ICart
    {
        /// <summary>
        /// Fires when cart has changed.
        /// </summary>
        event Action CartChanged;

        /// <summary>
        /// Fires when count of items in cart has changed.
        /// </summary>
        event Action CountChanged;

        /// <summary>
        /// List of products in cart.
        /// </summary>
        List<ICartProduct> Products { get; }

        /// <summary>
        /// Count of items in cart.
        /// </summary>
        int Count { get; }


        /// <summary>
        /// Total cost.
        /// </summary>
        long Cost { get; }

        /// <summary>
        /// Adds product in cart.
        /// </summary>
        /// <param name="cartProduct"></param>
        void AddProduct(ICartProduct cartProduct);


        /// <summary>
        /// Removes product from cart.
        /// </summary>
        /// <param name="cartProduct"></param>
        void RemoveProduct(ICartProduct cartProduct);

        /// <summary>
        /// Clears cart.
        /// </summary>
        void ClearCart();
    }
}
