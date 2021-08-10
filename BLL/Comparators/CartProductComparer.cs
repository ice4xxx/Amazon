using System.Collections.Generic;
using DI;

namespace BLL.Comparators
{
    public class CartProductComparer : IEqualityComparer<ICartProduct>
    {
        /// <summary>
        /// Compares two cart product by product id.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(ICartProduct x, ICartProduct y)
        {
            return x?.Product.Id == y?.Product.Id;
        }

        public int GetHashCode(ICartProduct obj)
        {
            unchecked
            {
                return ((obj.Product != null ? obj.Product.GetHashCode() : 0) * 397) ^ obj.Count;
            }
        }
    }
}