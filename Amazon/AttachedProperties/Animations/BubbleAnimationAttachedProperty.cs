using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Amazon.Controls;

namespace Amazon.AttachedProperties.Animations
{
    /// <summary>
    /// Class to animate custom bubbles.
    /// </summary>
    class BubbleAnimationAttachedProperty : BaseAnimationAttachedProperty<BubbleAnimationAttachedProperty>
    {

        /// <summary>
        /// Fires when ValueProperty has changed.
        /// </summary>
        protected override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                AnimateIn(d);
            }
            else
            {
                AnimateOut(d);
            }
        }

        /// <summary>
        /// Animates in custom bubble.
        /// </summary>
        /// <param name="d">custom bubble.</param>
        private static void AnimateIn(DependencyObject d)
        {
            var bubble = d as UserControl;

            Storyboard sb = new Storyboard();

            var animation = new ThicknessAnimation()
            {
                From = new Thickness(0,0,bubble.Margin.Right,0),
                To = new Thickness(0, -1 * bubble.ActualHeight, bubble.Margin.Right, bubble.ActualHeight),
                Duration = TimeSpan.FromSeconds(0.2)
            };

            var fade = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));

            sb.Children.Add(animation);
            sb.Children.Add(fade);

            sb.Begin(bubble);
        }


        /// <summary>
        /// Animates out custom bubble.
        /// </summary>
        /// <param name="d">custom bubble.</param>
        private static void AnimateOut(DependencyObject d)
        {
            var bubble = d as UserControl;

            Storyboard sb = new Storyboard();

            var animation = new ThicknessAnimation()
            {
                From = new Thickness(0, -1 * bubble.ActualHeight, bubble.Margin.Right, bubble.ActualHeight),
                To = new Thickness(0,0,bubble.Margin.Right,0),
                Duration = TimeSpan.FromSeconds(0.2)
            };

            var fade = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2),
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity"));

            sb.Children.Add(animation);
            sb.Children.Add(fade);

            sb.Begin(bubble);
        }
    }
}
