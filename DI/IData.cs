using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IData<T>
    {
        /// <summary>
        /// Fires when collection has changed.
        /// </summary>
        event Action CollectionChanged;

        /// <summary>
        /// Adds and item to the memory.
        /// </summary>
        /// <param name="item"></param>
        void AddItem(T item);

        /// <summary>
        /// Removes an item from  the memory.
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(T item);

        /// <summary>
        /// Return all items in memory.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();


    }
}
