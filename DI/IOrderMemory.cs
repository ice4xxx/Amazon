using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using DI.Enums;

namespace DI
{
    public interface IOrderMemory : IData<IOrder>
    {
        /// <summary>
        /// Fires when status of order has changed.
        /// </summary>
        event Action<IOrder> OrderStatusChanged;


        /// <summary>
        /// Returns orders by user email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IEnumerable<IOrder> GetOrdersByUserEmail(string email);

        /// <summary>
        /// Sets order status.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status"></param>
        void SetOrderStatusById(int Id, OrderStatus status);
    }
}
