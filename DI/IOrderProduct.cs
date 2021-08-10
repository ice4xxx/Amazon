using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IOrderProduct : IProduct
    {
        /// <summary>
        /// Count of product.
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Total cost of product.
        /// </summary>
        long TotalCost { get; }
    }
}
