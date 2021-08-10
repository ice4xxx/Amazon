using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace BLL
{
    public class Product : IProduct
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
        /// Image ins base64 of product.
        /// </summary>
        public string Base64Image { get; set; }
    }
}
