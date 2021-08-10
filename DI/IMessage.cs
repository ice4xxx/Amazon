using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IErrorMessage
    {
        /// <summary>
        /// Shows error message.
        /// </summary>
        /// <param name="message"></param>
        void Show(string message);
    }
}
