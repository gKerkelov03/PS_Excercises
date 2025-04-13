using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    public class PasswordConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value?.ToString() is not string password)
                return string.Empty;

            return new string('*', password.Length);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 