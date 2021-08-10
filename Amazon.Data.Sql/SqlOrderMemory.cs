using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Data.Sql.Entities;
using Amazon.Data.Sql.SQLite;
using DI;
using DI.Enums;

namespace Amazon.Data.Sql
{
    public class SqlOrderMemory : IOrderMemory
    {
        /// <summary>
        /// Fires when collection has changed.
        /// </summary>
        public event Action CollectionChanged;
        
        /// <summary>
        /// Adds item to memory data.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IOrder item)
        {
            using (AmazonContext db = new AmazonContext())
            {
                db.Orders.Add(new OrderEntity(item));
                db.SaveChanges();
                CollectionChanged?.Invoke();
            }
        }

        /// <summary>
        /// Removes item from memory data.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(IOrder item)
        {
            using (AmazonContext db = new AmazonContext())
            {
                db.Orders.Remove(new OrderEntity(item));
                db.SaveChanges();
                CollectionChanged?.Invoke();
            }
        }

        /// <summary>
        /// Returns all items in memory.
        /// </summary>
        /// <returns></returns>
        public List<IOrder> GetAll()
        {
            using (AmazonContext db = new AmazonContext())
            {
                OrderStatus tmp;
                return db.Orders.ToArray().Where(i => Enum.TryParse(i.Status, out tmp)).Select(i => i as IOrder).ToList();
            }
        }

        /// <summary>
        /// Fires when one of order's status has changed.
        /// </summary>
        public event Action<IOrder> OrderStatusChanged;

        /// <summary>
        /// Returns all orders of user by user email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<IOrder> GetOrdersByUserEmail(string email)
        {
            return GetAll().Where(entity => entity.CustomerEmail == email).ToList();
        }

        /// <summary>
        /// Sets order status to specific order.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void SetOrderStatusById(int id, OrderStatus status)
        {
            using (AmazonContext db = new AmazonContext())
            {
                foreach (var orderEntity in db.Orders.Where(i => i.Id == id))
                {
                    orderEntity.Status = status.ToString();
                    db.SaveChanges();
                    OrderStatusChanged?.Invoke(orderEntity);
                }
            }
        }
    }
}
