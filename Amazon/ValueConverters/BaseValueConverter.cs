using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Amazon.ValueConverters
{
    /// <summary>
    /// Base value converter.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()

    {
        /// <summary>
        /// Converts value to.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


        /// <summary>
        /// Converts value from.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);


        /// <summary>
        /// Singleton.
        /// </summary>
        protected static T instance;

        /// <summary>
        /// Returns singleton instance.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }
}
