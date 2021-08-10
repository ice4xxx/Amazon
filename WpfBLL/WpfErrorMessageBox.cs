using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DI;

namespace WpfBLL
{
    public class WpfErrorMessageBox : IErrorMessage
    {
        /// <summary>
        /// Show error box.
        /// </summary>
        /// <param name="message"></param>
        public void Show(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
