using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace Amazon.Core.Entities
{
    /// <summary>
    /// Entity for product to deserialize list of products.
    /// </summary>
    class ProductEntity : IOrderProduct
    {
        /// <summary>
        /// Id of product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of product.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Image of product in base 64.
        /// </summary>
        public string Base64Image { get; set; }

        /// <summary>
        /// Count of product in order.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Total cost of product in order.
        /// </summary>
        public long TotalCost => Cost * Count;
    }
}
