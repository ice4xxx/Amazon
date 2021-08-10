using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Comparators;
using DI;

namespace BLL
{
    public class Cart : ICart
    {
        /// <summary>
        /// Fires when cart has changed.
        /// </summary>
        public event Action CartChanged;

        /// <summary>
        /// Fires when count of items in cart has changed.
        /// </summary>
        public event Action CountChanged;

        /// <summary>
        /// Product list in cart.
        /// </summary>
        public List<ICartProduct> Products { get; set; } = new List<ICartProduct>();


        /// <summary>
        /// Count  of items in cart.
        /// </summary>
        public int Count => Products.Sum(i => i.Count);

        /// <summary>
        /// Total cost.
        /// </summary>
        public long Cost => Products.Sum(i => i.Product.Cost * i.Count);


        /// <summary>
        /// Adds products in cart.
        /// </summary>
        /// <param name="cartProduct"></param>
        public void AddProduct(ICartProduct cartProduct)
        {
            if (Products.Contains(cartProduct, new CartProductComparer()))
            {
                Products.Find(product => product.Product.Id == cartProduct.Product.Id).Count++;
                return;
            }

            cartProduct.ProductRemove += product => RemoveProduct(product);
            cartProduct.CountChanged += () => CountChanged?.Invoke();
            Products.Add(cartProduct);

            CartChanged?.Invoke();
        }

        /// <summary>
        /// Removes product from cart.
        /// </summary>
        /// <param name="cartProduct"></param>
        public void RemoveProduct(ICartProduct cartProduct)
        {
            Products.Remove(cartProduct);

            CartChanged?.Invoke();
        }

        /// <summary>
        /// Clears cart.
        /// </summary>
        public void ClearCart()
        {
            Products.Clear();

            CartChanged?.Invoke();
        }
    }
}
