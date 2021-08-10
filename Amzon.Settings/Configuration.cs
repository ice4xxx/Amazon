using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Data.Sql;
using BLL;
using BLL.Comparators;
using DI;
using SimpleInjector;
using WpfBLL;

namespace Amzon.Settings
{
    public class Configuration
    {
        /// <summary>
        /// IoC container.
        /// </summary>
        public Container Container { get; }

        public Configuration()
        {
            Container = new Container();

            Setup();
        }

        /// <summary>
        /// Setups container.
        /// </summary>
        private void Setup()
        {
            Container.Register<IInputFileService,WpfOpenFileDialog>(Lifestyle.Singleton);
            Container.Register<IImage,WpfImage>(Lifestyle.Transient);
            Container.Register<IUser,User>(Lifestyle.Transient);
            Container.Register<IUserMemory,SqlUserDataMemory>(Lifestyle.Singleton);
            Container.Register<IData<IProduct>,SqlProductMemory>(Lifestyle.Singleton);
            Container.Register<IErrorMessage,WpfErrorMessageBox>(Lifestyle.Singleton);
            Container.Register<IProduct,Product>(Lifestyle.Transient);
            Container.Register<ICartProduct,CartProduct>(Lifestyle.Transient);
            Container.Register<ICart,Cart>(Lifestyle.Singleton);
            Container.Register<IOrderMemory,SqlOrderMemory>(Lifestyle.Singleton);
            Container.Register<IOrder,Order>(Lifestyle.Transient);
            Container.Register<IFolderService,WpfFolderDialog>(Lifestyle.Singleton);
            Container.Register<IEqualityComparer<ICartProduct>,CartProductComparer>(Lifestyle.Singleton);
            Container.Register<IEqualityComparer<IUser>,UserComparer>(Lifestyle.Singleton);
        }
    }
}
