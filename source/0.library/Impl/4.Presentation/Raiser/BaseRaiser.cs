using Hellosam.Net.Collections;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Presentation.Raiser
{
    public class BaseRaiser<T, U, V, W> : IRaiser<T, U, V, W> 
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

        public virtual W Clear(W presentation)
        {
            presentation.Validations = new Dictionary<string, string>();

            return presentation;
        }

        public virtual W Raise(W presentation, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in presentation.Domain.Data.Columns)
            {
                presentation.OnPropertyChanged(column.Description.Reference);
            }

            presentation.OnPropertyChanged("Validations");

            return presentation;
        }
        public virtual W RaiseX(W presentation, int maxdepth = 1, int depth = 0)
        {
            return presentation;
        }
    }
}
