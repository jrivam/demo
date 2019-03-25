using System;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System.Reflection;
using System.Globalization;

namespace Library.Impl.Presentation.Raiser
{
    public class BaseRaiserInteractive<T, U, V, W> : IRaiserInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public W CreateInstance(V domain, int maxdepth)
        {
            return (W)Activator.CreateInstance(typeof(W),
                                    BindingFlags.CreateInstance |
                                    BindingFlags.Public |
                                    BindingFlags.Instance |
                                    BindingFlags.OptionalParamBinding,
                                    null, new object[] { domain, maxdepth },
                                    CultureInfo.CurrentCulture);
        }

        public virtual W Clear(W presentation, int maxdepth = 1, int depth = 0)
        {
            return presentation;
        }
        public virtual W Raise(W presentation, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in presentation.Domain.Data.Columns)
            {
                presentation.OnPropertyChanged(column.Description.Reference);
            }

            return presentation;
        }

        public virtual W Extra(W presentation, int maxdepth = 1, int depth = 0)
        {
            return presentation;
        }
    }
}
