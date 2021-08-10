using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace BLL
{
   public  class CartProduct : ICartProduct
   {
       private int _count = 1;

       /// <summary>
       /// Fires when product has removed from cart.
       /// </summary>
        public event Action<ICartProduct> ProductRemove;

       /// <summary>
       /// Fires when product count has changed.
       /// </summary>
        public event Action CountChanged;

       /// <summary>
       /// Product.
       /// </summary>
        public IProduct Product { get; set; }

       /// <summary>
       /// Count of product.
       /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                if (_count == 0)
                {
                    ProductRemove?.Invoke(this);
                }
                CountChanged?.Invoke();
            }
        }
    }
}
