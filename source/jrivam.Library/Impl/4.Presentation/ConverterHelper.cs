using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace jrivam.Library.Impl.Presentation
{
    [ValueConversion(typeof(Dictionary<string, string>), typeof(string))]
    public class DictionaryItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dictionary = (value as Dictionary<string, string>);
            var key = parameter as string;

            if (!string.IsNullOrWhiteSpace(key))
            {
                return (dictionary.ContainsKey(key)) ? dictionary[key] : string.Empty;
            }

            return string.Join(" / ", dictionary.Where(y => !string.IsNullOrWhiteSpace(y.Value)).Select(x => x.Value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
