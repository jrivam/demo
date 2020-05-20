using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Presentation
{
    public class HelperQueryInteractive<Q, R, S, T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        public static Q CreateQuery(R domain)
        {
            return (Q)Activator.CreateInstance(typeof(Q),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null,
                           new object[] { domain },
                           CultureInfo.CurrentCulture);
        }
    }
}
