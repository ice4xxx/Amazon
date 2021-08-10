using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface ICartProduct
    {
        /// <summary>
        /// Fires when product has removed from cart.
        /// </summary>
        event Action<ICartProduct> ProductRemove;

        /// <summary>
        /// Fires when count of product has changed.
        /// </summary>
        event Action CountChanged;

        /// <summary>
        /// Product.
        /// </summary>
        IProduct Product { get; set; }

        /// <summary>
        /// Count of product.
        /// </summary>
        int Count { get; set; }
    }
}
