using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Amazon.Core.TextsValidations
{
    public static class TextValidation
    {
        /// <summary>
        /// Email validation for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EmailValidation(string value)
        {
            if (value is null)
            {
                return false;
            }

            Regex regex = new Regex(
                "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");

            return regex.IsMatch(value);
        }

        /// <summary>
        /// Name validation for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NameValidation(string value)
        {
            if (value is null)
            {
                return false;
            }

            if (value.Length < 1)
            {
                return false;
            }

            foreach (var @char in value)
            {
                if (!char.IsLetter(@char))
                {
                    return false;
                }
            }

            return char.IsUpper(value[0]);
        }


        /// <summary>
        /// Address validation for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddressValidation(string value)
        {
            if (value is null)
            {
                return false;
            }

            if (value.Length < 1)
            {
                return false;
            }

            foreach (var @char in value)
            {
                if (!char.IsLetterOrDigit(@char) && @char != '.' && @char != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Phone validation for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool PhoneValidation(string value)
        {
            if (value is null)
            {
                return false;
            }

            Regex regex = new Regex(
                "(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]‌​)\\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)([2-9]1[02-9]‌​|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})\\s*(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+)\\s*)?$");

            return regex.IsMatch(value);
        }

        /// <summary>
        /// Digits validation for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DigitsValidation(string value)
        {
            if (value is null)
            {
                return false;
            }

            foreach (var s in value)
            {
                if (!char.IsDigit(s))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
