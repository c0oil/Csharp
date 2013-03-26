using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Markup;

namespace cosii5
{
    public class ByteImageConvertor : MarkupExtension, IValueConverter
    {
        public static ByteImageConvertor Instanse = new ByteImageConvertor();

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType != typeof(ImageSource))
                throw new NotImplementedException();

            if (value is List<Byte>)
            {
                var bytes = value as List<Byte>;
                if (bytes.Count > 0)
                {
                    var stream = new MemoryStream(bytes.ToArray());
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instanse ?? (Instanse = new ByteImageConvertor());
        }
    }
}
