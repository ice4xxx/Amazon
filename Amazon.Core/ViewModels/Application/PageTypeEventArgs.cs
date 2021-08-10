using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Enums;

namespace Amazon.Core.ViewModels.Application
{
    public class PageTypeEventArgs : EventArgs
    {
        /// <summary>
        /// The previous page of the window.
        /// </summary>
        public PageType OldPage { get; set; }

        /// <summary>
        /// The current page of the window.
        /// </summary>
        public PageType NewPage { get; set; }

        /// <summary>
        /// ctor.
        /// </summary>
        public PageTypeEventArgs(PageType oldPage, PageType newPage)
        {
            OldPage = oldPage;
            NewPage = newPage;
        }
    }
}
