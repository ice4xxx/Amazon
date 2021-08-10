using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Data.Sql.Entities;
using Amazon.Data.Sql.SQLite;
using DI;

namespace Amazon.Data.Sql
{
    public class SqlProductMemory : IData<IProduct>
    {
        /// <summary>
        /// Fires when application changed.
        /// </summary>
        public event Action CollectionChanged;

        /// <summary>
        /// Adds item to memory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IProduct item)
        {
            using (AmazonContext db = new AmazonContext())
            {
                db.Products.Add(new ProductEntity(item));
                db.SaveChanges();

                CollectionChanged?.Invoke();
            }
        }

        /// <summary>
        /// Removes item from memory.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(IProduct item)
        {
            using (AmazonContext db = new AmazonContext())
            {
                db.Products.Remove(new ProductEntity(item));
                db.SaveChanges();

                CollectionChanged?.Invoke();
            }
        }

        /// <summary>
        /// Returns all items.
        /// </summary>
        /// <returns></returns>
        public List<IProduct> GetAll()
        {
            using (AmazonContext db = new AmazonContext())
            {
                return Array.ConvertAll(db.Products.ToArray(), i => i as IProduct).ToList();
            }
        }
    }
}
