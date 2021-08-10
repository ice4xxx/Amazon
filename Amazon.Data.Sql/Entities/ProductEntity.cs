using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace Amazon.Data.Sql.Entities
{
    [Table("Products")]
    class ProductEntity : IProduct
    {
        /// <summary>
        /// Product's id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Product's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product's cost.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Product's image in base64.
        /// </summary>
        public string Base64Image { get; set; }

        public ProductEntity()
        {

        }

        /// <summary>
        /// Ctor from interface.
        /// </summary>
        /// <param name="product"></param>
        public ProductEntity(IProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Cost = product.Cost;
            Base64Image = product.Base64Image;
        }
    }
}
