using System;
using System.Globalization;
using System.Windows.Data;

namespace Test.BaseUI
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanToInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = (bool)value;
            return !booleanValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = (bool)value;
            return !booleanValue;
        }
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleToPersistantStringConverter : IValueConverter
    {
        private string lastConvertBackString;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double)) return null;

            var stringValue = lastConvertBackString ?? value.ToString();
            lastConvertBackString = null;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) return null;

            double result;
            if (double.TryParse((string)value, out result))
            {
                lastConvertBackString = (string)value;
                return result;
            }

            return null;
        }
    }

    [ValueConversion(typeof(double?), typeof(string))]
    public class NullableDoubleToStringConverter : IValueConverter
    {
        private string lastConvertBackString;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double?)) 
                return value;

            var stringValue = lastConvertBackString ?? value.ToString();
            lastConvertBackString = null;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return value;

            if (string.IsNullOrWhiteSpace((string)value))
            {
                return null;
            }

            double result;
            if (double.TryParse((string)value, out result))
            {
                lastConvertBackString = (string)value;
                return result;
            }

            return value;
        }
    }
}
