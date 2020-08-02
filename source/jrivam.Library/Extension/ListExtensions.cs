using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace jrivam.Library.Extension
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}
