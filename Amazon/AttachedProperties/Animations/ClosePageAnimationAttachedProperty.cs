using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Amazon.Pages;

namespace Amazon.AttachedProperties.Animations
{
    class ClosePageAnimationAttachedProperty : BaseAnimationAttachedProperty<ClosePageAnimationAttachedProperty>
    {
        /// <summary>
        /// Fires when ValueProperty has changed.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page = (d as Frame)?.Content as BasePage;

            //if new value is true closes page.
            if ((bool) e.NewValue)
            {
                page?.Close();
            }
        }
    }
}
