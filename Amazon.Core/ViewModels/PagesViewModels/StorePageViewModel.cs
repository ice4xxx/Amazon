using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.ViewModels.Base;
using Amazon.Core.ViewModels.Controls;
using DI;

namespace Amazon.Core.ViewModels.PagesViewModels
{
    public class StorePageViewModel : BaseViewModel
    {
        /// <summary>
        /// List of products in store.
        /// </summary>
        public List<StoreItemViewModel> ProductStore
        {
            get
            {
                try
                {
                    return ApplicationViewModel.Container.GetInstance<IData<IProduct>>()
                        .GetAll().Select(i => new StoreItemViewModel() { Product = i }).ToList();
                }
                catch (Exception e)
                {
                    ApplicationViewModel.Container.GetInstance<IErrorMessage>().Show("Сервер спит!");
                }

                return null;
            }
        }
    }
}
