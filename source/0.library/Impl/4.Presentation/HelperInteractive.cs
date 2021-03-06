﻿using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Presentation
{
    public class HelperInteractive<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public static W CreateInstance(V domain, int maxdepth = 1)
        {
            return (W)Activator.CreateInstance(typeof(W),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null,
                           new object[] { domain, maxdepth },
                           CultureInfo.CurrentCulture);
        }
    }
}
