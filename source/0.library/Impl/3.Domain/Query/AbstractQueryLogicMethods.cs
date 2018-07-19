﻿using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Query
{
    public abstract class AbstractQueryLogicMethods<S, T, U, V> : AbstractQueryLogic<S>, IQueryLogicMethods<T, U, V>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>
        where V : ITableLogic<T, U>
        where S : IQueryRepository, IQueryRepositoryMethods<T, U>, new()
    {
        protected readonly ILogicQuery<T, U, V> _logic;

        public AbstractQueryLogicMethods(ILogicQuery<T, U, V> logic)
            : base()
        {
            _logic = logic;
        }

        public virtual (Result result, V domain) Retrieve(int maxdepth = 1, V domain = default(V))
        {
            return _logic.Retrieve(Data, maxdepth, domain);
        }
        public virtual (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IList<V> domains = null)
        {
            return _logic.List(Data, maxdepth, top, domains);
        }
    }
}
