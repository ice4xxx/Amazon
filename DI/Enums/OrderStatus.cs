using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Enums
{
    [Flags]
    public enum OrderStatus
    {
        OnProcessing = 1,
        Processed = 2,
        Paid = 4,
        Shipped = 8,
        Completed = 16,
    }
}
