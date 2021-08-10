using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace Amazon.Data.Sql.Entities
{
    [Table("Users")]
    class UserEntity : IUser
    {
        /// <summary>
        /// User's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// User's patronymic.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's image in base64.
        /// </summary>
        public string Base64Image { get; set; }

        /// <summary>
        /// User's address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User's email.
        /// </summary>
        [Key]
        public string Email { get; set; }

        /// <summary>
        /// User's password in sha256.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Is user seller.
        /// </summary>
        public bool IsSeller { get; set; }

        /// <summary>
        /// User's orders.
        /// </summary>
        public string Orders { get; set; }

        public UserEntity()
        {
            
        }


        /// <summary>
        /// Ctor from user.
        /// </summary>
        /// <param name="user"></param>
        public UserEntity(IUser user)
        {
            Name = user.Name;

            Surname = user.Surname;

            Patronymic = user.Patronymic;

            PhoneNumber = user.PhoneNumber;

            IsSeller = user.IsSeller;

            Base64Image = user.Base64Image;

            Address = user.Address;

            Email = user.Email;

            Password = user.Password;
        }
    }
}
