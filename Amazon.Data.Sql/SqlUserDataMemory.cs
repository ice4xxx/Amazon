using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Data.Sql.Entities;
using Amazon.Data.Sql.SQLite;
using DI;

namespace Amazon.Data.Sql
{
    public class SqlUserDataMemory : IUserMemory
    {
        /// <summary>
        /// Fires when user has logged out.
        /// </summary>
        public event Action UserLoggedOut;

        /// <summary>
        /// Logged in user.
        /// </summary>
        public IUser LoggedInUser { get; private set; }

        /// <summary>
        /// Returns user by user email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IUser GetUserByEmail(string email)
        {
            using (AmazonContext db = new AmazonContext())
            {
                var users = db.Users.Where(i => i.Email == email).ToList();

                if (users.Count == 1)
                {
                    return users[0];
                }

                return null;
            }
        }

        /// <summary>
        /// Signs in user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public void LoginUser(string email, string password)
        {
            using (AmazonContext db = new AmazonContext())
            {
                foreach (var userEntity in db.Users)
                {
                    if (userEntity.Email == email)
                    {
                        var encryptedPassword = Amazon.Encryption.Encryption.Sha256(password + userEntity.Base64Image);
                        if (userEntity.Password == encryptedPassword)
                        {
                            LoggedInUser = userEntity;
                            return;
                        }
                    }
                }
            }

            throw new ArgumentException("No user with such email and password");
        }

        /// <summary>
        /// Logs out user.
        /// </summary>
        public void LogOutUser()
        {
            if (LoggedInUser != null)
            {
                LoggedInUser = null;
                UserLoggedOut?.Invoke();
            }
        }

        /// <summary>
        /// Fires when collection has changed.
        /// </summary>
        public event Action CollectionChanged;

        /// <summary>
        /// Adds item to memory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IUser item)
        {
            using (AmazonContext db = new AmazonContext())
            {
                db.Users.Add(new UserEntity(item));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Removes item from memory.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(IUser item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all items.
        /// </summary>
        /// <returns></returns>
        public List<IUser> GetAll()
        {
            using (AmazonContext db = new AmazonContext())
            {
                return db.Users.ToArray().Select(i => i as IUser).ToList();
            }
        }
    }
}
