using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IInputFileService
    {
        /// <summary>
        /// Returns string with file path.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        string GetFile(string filter);
    }
}
