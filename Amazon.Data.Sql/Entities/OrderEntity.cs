using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;
using DI.Enums;

namespace Amazon.Data.Sql.Entities
{
    [Table("Orders")]
    class OrderEntity : IOrder
    {

        /// <summary>
        /// Order's id.
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// Customer email.
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Creation date of product.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Orders status <see cref="OrderStatus"/>
        /// </summary>
        [TypeConverter(typeof(string))]
        public string Status { get; set; }

        /// <summary>
        /// Order's products in json format.
        /// </summary>
        public string ProductsJson { get; set; }

        /// <summary>
        /// Order's cost.
        /// </summary>
        public long Cost { get; set; }

        public OrderEntity()
        {

        }

        /// <summary>
        /// Ctor form interface.
        /// </summary>
        /// <param name="order"></param>
        public OrderEntity(IOrder order)
        {
            Id = order.Id;
            CustomerEmail = order.CustomerEmail;
            Date = order.Date;
            Status = order.Status;
            ProductsJson = order.ProductsJson;
            Cost = order.Cost;
        }
    }
}
