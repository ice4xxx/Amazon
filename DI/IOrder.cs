using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Enums;

namespace DI
{
    public interface IOrder
    {
        int Id { get; set; }

        string CustomerEmail { get; set; }

        string Date { get; set; }

        string Status { get; set; }
        
        string ProductsJson { get; set; }

        long Cost { get; set; }
    }
}
