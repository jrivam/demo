﻿using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Business.Loader
{
    public class BaseLoader<T, U, V> : ILoader<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public BaseLoader()
        {
        }

        public virtual void Clear(V domain)
        {
        }

        public virtual void Load(V domain, int maxdepth = 1, int depth = 0)
        {
        }
        public virtual void LoadX(V domain, int maxdepth = 1, int depth = 0)
        {
        }
    }
}