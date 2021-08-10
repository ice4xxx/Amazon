using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Amazon.ValueConverters
{
    class InactiveForegroundValueConverter : MarkupExtension, IMultiValueConverter
    {

        /// <summary>
        /// Convert foreground to innactive color.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = (SolidColorBrush)values[0];
            var isEnabled = (bool)values[1];

            if (!isEnabled)
            {
                Color color = Color.FromArgb(brush.Color.A,
                    System.Convert.ToByte(brush.Color.R == 0 ? 150 : brush.Color.R * 1.2),
                    System.Convert.ToByte(brush.Color.G == 0 ? 150 : brush.Color.G * 1.2),
                    System.Convert.ToByte(brush.Color.B == 0 ? 150 : brush.Color.B * 1.2));

                return new SolidColorBrush(color);
            }

            return brush;
        }

        /// <summary>
        /// WE dont need it.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Singleton.
        /// </summary>
        private static InactiveForegroundValueConverter instance;


        /// <summary>
        /// Return singleton instance.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (instance == null)
            {
                instance = new InactiveForegroundValueConverter();
            }

            return instance;
        }
    }
}
