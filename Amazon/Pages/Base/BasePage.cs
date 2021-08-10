using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Amazon.Pages
{
    /// <summary>
    /// Base page of this programm.
    /// </summary>
    public abstract class BasePage : Page
    {
        /// <summary>
        /// ctor.
        /// </summary>
        protected BasePage()
        {
            //Animates while page loading.
            Loaded += (sender, args) =>
            {
                var opacityAnimation = new DoubleAnimation(
                    fromValue: 0,
                    toValue: 1,
                    new Duration(TimeSpan.FromSeconds(0.2))
                );

                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

                Storyboard sb = new Storyboard();

                sb.Children.Add(opacityAnimation);
                sb.Begin(this);
            };
        }

        /// <summary>
        /// Animates while page closing.
        /// </summary>
        public virtual void Close()
        {
            if (this.Opacity == 0)
            {
                return;
            }

            var opacityAnimation = new DoubleAnimation(
                fromValue: 1,
                toValue: 0,
                new Duration(TimeSpan.FromSeconds(0.2))
            );

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            Storyboard sb = new Storyboard();

            sb.Children.Add(opacityAnimation);

            sb.Begin(this);
        }
    }
}