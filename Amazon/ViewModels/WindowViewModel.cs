using System;
using System.Threading.Tasks;
using Amazon.Core.ViewModels.Base;
using Amazon.Pages;
using Fixes;
using System.Windows;
using System.Windows.Input;
using Amazon.Core.Enums;
using Amazon.Core.ViewModels;
using DI;

namespace Amazon.ViewModels
{
    /// <summary>
    /// The View Model for the custom window.
    /// </summary>
    class WindowViewModel : BaseViewModel
    {
        #region Private members

        /// <summary>
        /// The window this vm controlls.
        /// </summary>
        private Window _window;

        /// <summary>
        /// Margin of windows to allow dropshadow.
        /// </summary>
        private int _outerMarginSize = 3;

        /// <summary>
        /// Corner radius of window.
        /// </summary>
        private int _windowCornerRadius = 13;

        #endregion

        #region Commands

        /// <summary>
        /// Command to minimize the window.
        /// </summary>
        public ICommand MinimizeCommand => new RelayCommand(() => _window.WindowState = WindowState.Minimized);

        /// <summary>
        /// Command to maximize the window.
        /// </summary>
        public ICommand MaximizeCommand => new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);

        /// <summary>
        /// Command to close the window.
        /// </summary>
        public ICommand CloseCommand => new RelayCommand(_window.Close);

        /// <summary>
        /// Command to show system menu.
        /// </summary>
        public ICommand MenuCommand =>
            new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, _window.PointToScreen(Mouse.GetPosition(_window))));

        public ICommand LogOutCommand => new RelayCommand(async () =>
        {
            ApplicationViewModel.CurrentPage = PageType.LoginPage;
            ApplicationViewModel.Container.GetInstance<IUserMemory>().LogOutUser();
            OnPropertyChanged(nameof(IsLogOutButtonVisible));
        });

        #endregion

        #region Public members

        /// <summary>
        /// Margin of window.
        /// </summary>
        public Thickness OuterMarginSizeThickness => _window.WindowState == WindowState.Maximized ? new Thickness(0) : new Thickness(_outerMarginSize);

        /// <summary>
        /// Corner radius of window.
        /// </summary>
        public CornerRadius WindowCornerRadius => _window.WindowState == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(_windowCornerRadius);

        /// <summary>
        /// Size of resize border of window.
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// Size of  resize border of window.
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + _outerMarginSize);

        /// <summary>
        /// The padding of the inner content.
        /// </summary>
        public Thickness InnerContentPadding => new Thickness(0/*ResizeBorder*/);

        /// <summary>
        /// Height of title.
        /// </summary>
        public int TitleHeight { get; set; } = 30;

        /// <summary>
        /// Return height for caption block of the window.
        /// </summary>
        public int CaptionHeight => TitleHeight + (int)OuterMarginSizeThickness.Bottom;

        /// <summary>
        /// Height of title.
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight);


        /// <summary>
        /// Set true to animate page closing.
        /// </summary>
        public bool IsPageClosing { get; set; }

        /// <summary>
        /// True if exit button is visible.
        /// </summary>
        public bool IsLogOutButtonVisible =>
            ApplicationViewModel.Container.GetInstance<IUserMemory>().LoggedInUser != null;

        /// <summary>
        /// Current page of window.
        /// </summary>
        public PageType CurrentPage { get; set; } = ApplicationViewModel.CurrentPage;

        /// <summary>
        /// Width of <see cref="CurrentPage"/>.
        /// </summary>
        public double PageWidth => _window.Width - _outerMarginSize * 2 - ResizeBorder * 2;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowViewModel(Window window)
        {
            _window = window;

            _window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
            };

            ApplicationViewModel.PageTypeChanged += async (sender,e) =>
            {
                IsPageClosing = true;
                await Task.Delay(TimeSpan.FromSeconds(0.2));
                IsPageClosing = false;
                CurrentPage = ApplicationViewModel.CurrentPage;
                OnPropertyChanged(nameof(IsLogOutButtonVisible));
            };

            //Фикс для углов кастомного окна - придуман не мной.
            var resizer = new WindowResizer(_window);
        }

        #endregion
    }
}
