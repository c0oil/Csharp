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
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instanse ?? (Instanse = new ByteImageConvertor());
        }

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
    }

    public class StringToDoubleConverter : MarkupExtension, IValueConverter
    {
        public static StringToDoubleConverter Instanse = new StringToDoubleConverter();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instanse ?? (Instanse = new StringToDoubleConverter());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0:0.00}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(double))
                throw new NotImplementedException();

            return string.IsNullOrEmpty(value.ToString())? 0: System.Convert.ToDouble(value.ToString());
        }
    }

    public class StringToPercentConverter : MarkupExtension, IValueConverter
    {
        public static StringToPercentConverter Instanse = new StringToPercentConverter();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instanse ?? (Instanse = new StringToPercentConverter());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0:0.00}%", (double)value * 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
