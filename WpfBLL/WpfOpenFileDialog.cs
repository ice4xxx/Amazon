using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;
using Microsoft.Win32;

namespace WpfBLL
{
    public class WpfOpenFileDialog : IInputFileService
    {
        /// <summary>
        /// Show file dialog and returns file path.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;

            dialog.ShowDialog();

            return dialog.FileName;
        }
    }
}
