using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace DI
{
    public interface IUser
    {
        /// <summary>
        /// User's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// User's surname.
        /// </summary>
        string Surname { get; set; }

        /// <summary>
        /// User's patronymic.
        /// </summary>
        string Patronymic { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        string PhoneNumber { get; set; }

        /// <summary>
        /// User's image in base64.
        /// </summary>
        [Ignore]
        string Base64Image { get; set; }

        /// <summary>
        /// User's address.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// User's email.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [Ignore]
        string Password { get; set; }

        /// <summary>
        /// Is user seller.
        /// </summary>
        [Ignore]
        bool IsSeller { get; set; }
    }
}
