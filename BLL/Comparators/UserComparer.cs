using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace BLL.Comparators
{
    public class UserComparer : IEqualityComparer<IUser>
    {
        /// <summary>
        /// Compares two users by user email.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(IUser x, IUser y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Email == y.Email;
        }

        public int GetHashCode(IUser obj)
        {
            return (obj.Email != null ? obj.Email.GetHashCode() : 0);
        }
    }
}
