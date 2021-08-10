using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Amazon.AttachedProperties
{
    /// <summary>
    /// Attached properties to work with password box.
    /// </summary>
    public class PasswordBoxAttachedProperties
    {
        /// <summary>
        /// Property to monitor password in password box.
        /// </summary>
        public static readonly DependencyProperty MonitorPasswordProperty =
            DependencyProperty.RegisterAttached("MonitorPassword",
                typeof(bool),
                typeof(PasswordBoxAttachedProperties),
                new FrameworkPropertyMetadata(false, OnMonitorPasswordChanged));

        /// <summary>
        /// Fires when monitor password property has changed.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            //Clears all monitor's method.
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool) e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            }
        }


        /// <summary>
        /// Fires when password in password box has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetSecurePassword(passwordBox, passwordBox.SecurePassword);
            SetHasText(passwordBox);
        }

        /// <summary>
        /// Sets monitor password property value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetMonitorPassword(PasswordBox element, bool value)
        {
            element.SetValue(MonitorPasswordProperty,value);
            SetHasText(element);
        }


        /// <summary>
        /// Property that return true if password box has password.
        /// </summary>
        public static readonly DependencyProperty HasPasswordProperty =
            DependencyProperty.RegisterAttached("HasPassword", typeof(bool), typeof(PasswordBoxAttachedProperties), new PropertyMetadata(false));

        /// <summary>
        /// Sets has password property value.
        /// </summary>
        /// <param name="element"></param>
        public static void SetHasText(PasswordBox element)
        {
            element.SetValue(HasPasswordProperty, element.Password.Length > 0);
        }

        /// <summary>
        /// Returns has password property value.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetHasPassword(PasswordBox element)
        {
            return (bool)element.GetValue(HasPasswordProperty);
        }

        /// <summary>
        /// Property to bind secure password.
        /// </summary>
        public static readonly DependencyProperty SecurePasswordProperty = 
            DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(PasswordBoxAttachedProperties), new PropertyMetadata(default(SecureString)));

        /// <summary>
        /// Sets secure password.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSecurePassword(PasswordBox element, SecureString value)
        {
            element.SetValue(SecurePasswordProperty,value);
        }

        /// <summary>
        /// Returns secure password.
        /// </summary>
        /// <param name="element"></param>
        public static void GetSecurePassword(PasswordBox element)
        {
            element.GetValue(SecurePasswordProperty);
        }

    }
}
