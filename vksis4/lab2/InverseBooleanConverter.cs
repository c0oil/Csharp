using System;
using System.Windows.Data;

namespace lab2
{
    public class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Entity =  new InverseBooleanConverter();

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class StringToIntConverter : IValueConverter
    {
        public static StringToIntConverter Entity = new StringToIntConverter();

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");

            return System.Convert.ToByte(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(byte))
                throw new InvalidOperationException("The target must be a string");

            return value.ToString();
        }
    }
}
