using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Amazon.Core.TextsValidations;

namespace Amazon.TextsPreviews
{
    public static class TextPreview
    {
        /// <summary>
        /// Red brush for text box foreground.
        /// </summary>
        private static readonly SolidColorBrush ErrorBrush = Application.Current.FindResource("ErrorBrush") as SolidColorBrush;
        
        /// <summary>
        /// Non error brush for text box foreground.
        /// </summary>
        private static readonly SolidColorBrush InputTextBoxForegroundBrush = Application.Current.FindResource("InputTextBoxForegroundBrush") as SolidColorBrush;

        /// <summary>
        /// Text preview for email text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void EmailTextPreview(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!TextValidation.EmailValidation(textBox.Text))
            {
                textBox.Foreground = ErrorBrush;
            }
            else
            {
                textBox.Foreground = InputTextBoxForegroundBrush;
            }
        }

        /// <summary>
        /// Text preview for only digits text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DigitsTextPreview(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!TextValidation.DigitsValidation(textBox.Text))
            {
                textBox.Foreground = ErrorBrush;
            }
            else
            {
                textBox.Foreground = InputTextBoxForegroundBrush;
            }
        }

        /// <summary>
        /// Text preview for name text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void NameTextPreview(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!TextValidation.NameValidation(textBox.Text))
            {
                textBox.Foreground = ErrorBrush;
            }
            else
            {
                textBox.Foreground = InputTextBoxForegroundBrush;
            }
        }
        

        /// <summary>
        /// Text preview for address text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void AddressTextPreview(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!TextValidation.AddressValidation(textBox.Text))
            {
                textBox.Foreground = ErrorBrush;
            }
            else
            {
                textBox.Foreground = InputTextBoxForegroundBrush;
            }
        }

        /// <summary>
        /// Text preview for phone number text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PhoneTextPreview(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!TextValidation.PhoneValidation(textBox.Text))
            {
                textBox.Foreground = ErrorBrush;
            }
            else
            {
                textBox.Foreground = InputTextBoxForegroundBrush;
            }
        }
    }
}
