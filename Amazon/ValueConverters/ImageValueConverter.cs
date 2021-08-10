using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DI;

namespace Amazon.ValueConverters
{
    class ImageValueConverter : BaseValueConverter<ImageValueConverter>
    {
        /// <summary>
        /// Converts <see cref="IImage"/> to string in base64. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = "";
            if (value is IImage iimage)
            {
                image = iimage.Base64Image;
            }
            else
            {
                image = value.ToString();
            }

            var bytes = System.Convert.FromBase64String(image);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
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
