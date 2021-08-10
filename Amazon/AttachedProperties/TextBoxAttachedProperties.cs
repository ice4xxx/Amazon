using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Amazon.Enums;
using Amazon.TextsPreviews;

namespace Amazon.AttachedProperties
{
    class TextBoxAttachedProperties
    {
        /// <summary>
        /// Format type of text box.
        /// </summary>
        public static readonly DependencyProperty FormatTypeProperty = DependencyProperty.RegisterAttached("FormatType", typeof(FormatType), typeof(TextBoxAttachedProperties), new FrameworkPropertyMetadata(FormatType.None, OnFormatTypeChanged));

        /// <summary>
        /// Fires when Format type of text box has changed.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnFormatTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetFormatType(d as TextBox, (FormatType)e.NewValue);
        }

        /// <summary>
        /// Sets format type property value and text preview for text box.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFormatType(TextBox element, FormatType value)
        {
            element.TextChanged -= TextPreview.EmailTextPreview;
            element.TextChanged -= TextPreview.NameTextPreview;
            element.TextChanged -= TextPreview.AddressTextPreview;
            element.TextChanged -= TextPreview.PhoneTextPreview;
            element.TextChanged -= TextPreview.DigitsTextPreview;

            switch (value)
            {
                case FormatType.Email:
                    element.TextChanged += TextPreview.EmailTextPreview;
                    break;
                case FormatType.Name:
                    element.TextChanged += TextPreview.NameTextPreview;
                    break;
                case FormatType.Address:
                    element.TextChanged += TextPreview.AddressTextPreview;
                    break;
                case FormatType.PhoneNumber:
                    element.TextChanged += TextPreview.PhoneTextPreview;
                    break;
                case FormatType.Digits:
                    element.TextChanged += TextPreview.DigitsTextPreview;
                    break;
            }
            element.SetValue(FormatTypeProperty,value);
        }

        /// <summary>
        /// Returns format type of text box.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FormatType GetFormatType(TextBox element) => (FormatType)element.GetValue(FormatTypeProperty);
    }
}
