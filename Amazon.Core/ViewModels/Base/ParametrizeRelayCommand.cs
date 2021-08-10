using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Amazon.Core.ViewModels.Base
{
    public class ParametrizeRelayCommand : ICommand
    {

        /// <summary>
        /// The action to run.
        /// </summary>
        private readonly Action<object> mAction;

        /// <summary>
        /// Always true.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="action"></param>
        public ParametrizeRelayCommand(Action<object> action)
        {
            this.mAction = action;
        }

        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };
    }
}
