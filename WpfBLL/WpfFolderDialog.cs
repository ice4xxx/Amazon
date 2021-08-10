using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;
using WPFFolderBrowser;

namespace WpfBLL
{
    public class WpfFolderDialog : IFolderService
    {
        /// <summary>
        /// Show folder dialog and returns folder path.
        /// </summary>
        /// <returns></returns>
        public string ChoseFolder()
        {
            WPFFolderBrowserDialog dialog = new WPFFolderBrowserDialog();

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
