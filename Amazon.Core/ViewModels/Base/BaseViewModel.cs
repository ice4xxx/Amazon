using PropertyChanged;
using System.ComponentModel;

namespace Amazon.Core.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotify interface implementation
        /// <summary>
        /// The event that fired when any children property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        } 
        #endregion

        /// <summary>
        /// ctor.
        /// </summary>
        public BaseViewModel()
        {
            SetupCommands();
        }

        /// <summary>
        /// Setups commands.
        /// </summary>
        protected virtual void SetupCommands()
        {
        }
    }
}
