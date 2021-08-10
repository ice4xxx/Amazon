using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;

namespace BLL
{
    public class User : IUser
    {
        /// <summary>
        /// User's password in sha256/
        /// </summary>
        private string _password;

        /// <summary>
        /// Phone number in +7... style.
        /// </summary>
        private string _phoneNumber;

        /// <summary>
        /// Name of user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Surname of user.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Patronymic of user.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = $"+7{value}";
        }
        /// <summary>
        /// Image of user in base64.
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
        /// User's password.
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = Amazon.Encryption.Encryption.Sha256(value + Base64Image);
        }

        /// <summary>
        /// Is User seller
        /// </summary>
        public bool IsSeller { get; set; }

        /// <summary>
        /// User's orders.
        /// </summary>
        public string Orders { get; set; } = "";
    }
}
