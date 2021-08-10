using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.ValueConverters
{
    class TextToIntValueConverter : BaseValueConverter<TextToIntValueConverter>
    {
        /// <summary>
        /// Converts ulong to string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() ?? "";
        }

        /// <summary>
        /// Convert input in text box to ulong.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value?.ToString();

            try
            {
                var number = ulong.Parse(text);

                return number;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch
            {
                return ulong.MaxValue;
            }
        }
    }
}