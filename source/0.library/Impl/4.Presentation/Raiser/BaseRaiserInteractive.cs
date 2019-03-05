using System;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Raiser;
using library.Interface.Presentation.Table;
using System.Reflection;
using System.Globalization;

namespace library.Impl.Presentation.Raiser
{
    public class BaseRaiserInteractive<T, U, V, W> : IRaiserInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
        where W : ITableInteractive<T, U, V>
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
