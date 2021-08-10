using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Amazon.Pages;

namespace Amazon.AttachedProperties
{
    abstract class BaseAnimationAttachedProperty<T>
    where T :BaseAnimationAttachedProperty<T>, new()
    {
        /// <summary>
        /// Property to animate uielement.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(BaseAnimationAttachedProperty<T>), new PropertyMetadata(false, OnValuePropertyChanged));

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static T Instance { get; private set; } = new T();


        /// <summary>
        /// Fires when ValueProperty has changed.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d,e);
        }

        /// <summary>
        /// Fires when ValueProperty has changed.
        /// Created to override in children class.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected abstract void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);

        /// <summary>
        /// Sets ValueProperty value.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="value"></param>
        public static void SetValue(UIElement frame, bool value)
        {
            frame.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Returns ValueProperty value.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetValue(UIElement frame, bool value) => (bool)frame.GetValue(ValueProperty);
    }
}
