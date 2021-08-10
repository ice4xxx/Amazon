
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IFolderService
    {
        /// <summary>
        /// Returns path to folder.
        /// </summary>
        /// <returns></returns>
        string ChoseFolder();
    }
}
