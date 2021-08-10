using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IUserMemory : IData<IUser>
    {

        event Action UserLoggedOut;

        /// <summary>
        /// Current logged in user.
        /// </summary>
        IUser LoggedInUser { get; }


        /// <summary>
        /// Returns user by email.
        /// </summary>S
        IUser GetUserByEmail(string email);

        /// <summary>
        /// Logs in user.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="password">Entered password.</param>
        /// <exception cref="ArgumentException">If email or password is incorrect.</exception>
        void LoginUser(string email, string password);

        /// <summary>
        /// Logs out user.
        /// </summary>
        void LogOutUser();
    }
}
