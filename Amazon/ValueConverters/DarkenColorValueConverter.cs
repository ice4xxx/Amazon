using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Amazon.ValueConverters
{
    class DarkenColorValueConverter : BaseValueConverter<DarkenColorValueConverter>
    {
        /// <summary>
        /// Return darken value that input.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = (SolidColorBrush) value;
            return new SolidColorBrush(Color.FromArgb(source.Color.A, System.Convert.ToByte( source.Color.R * 0.8), System.Convert.ToByte(source.Color.G * 0.8), System.Convert.ToByte(source.Color.B * 0.8)));
        }

        /// <summary>
        /// We dont need it.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
