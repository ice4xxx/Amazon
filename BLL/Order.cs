using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;
using DI.Enums;
using Newtonsoft.Json;

namespace BLL
{
    public class Order : IOrder
    {
        /// <summary>
        /// Id of order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email of customer.
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Status of order <see cref="OrderStatus"/>.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Products of order in json format.
        /// </summary>
        public string ProductsJson { get; set; }

        /// <summary>
        /// Total cost.
        /// </summary>
        public long Cost { get; set; }
    }
}
