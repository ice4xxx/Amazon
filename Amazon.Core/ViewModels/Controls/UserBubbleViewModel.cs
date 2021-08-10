using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.ViewModels.Base;
using DI;

namespace Amazon.Core.ViewModels.Controls
{
    public class UserBubbleViewModel : BaseViewModel
    {
        /// <summary>
        /// Current user.
        /// </summary>
        public IUser User => ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser;
    }
}
