using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace library.Impl.Presentation
{
    [ValueConversion(typeof(Dictionary<string, string>), typeof(string))]
    public class DictionaryItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dictionary = (value as Dictionary<string, string>);
            var key = parameter as string;

            return (dictionary.ContainsKey(key)) ? dictionary[key] : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
