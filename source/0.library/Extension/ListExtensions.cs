using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.Extension
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> l)
        {
            return new ObservableCollection<T>(l);
        }
    }
}
