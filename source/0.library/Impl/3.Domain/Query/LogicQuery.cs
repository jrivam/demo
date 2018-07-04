﻿using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Domain.Query
{
    public class LogicQuery<T, U, V> : Logic<T, U, V>, ILogicQuery<T, U, V> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        public LogicQuery(IMapperLogic<T, U, V> mapper)
            : base(mapper)
        {
        }

        public virtual (Result result, V domain) Retrieve(IQueryRepositoryMethods<T, U> queryrepository, int maxdepth = 1, V domain = default(V))
        {
            var selectsingle = queryrepository.SelectSingle(maxdepth);

            if (selectsingle.result.Success && selectsingle.data != null)
            {
                domain = (V)Activator.CreateInstance(typeof(V),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, 
                    null, new object[] { selectsingle.data }, CultureInfo.CurrentCulture);

                _mapper.Clear(domain, maxdepth, 0);

                domain.Changed = false;
                domain.Deleted = false;
            }

            return (selectsingle.result, domain);
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryRepositoryMethods<T, U> queryrepository, int maxdepth = 1, int top = 0, IList<V> domains = null)
        {
            var enumeration = new List<V>();
            var iterator = (domains ?? new List<V>()).GetEnumerator();

            var selectmultiple = queryrepository.SelectMultiple(maxdepth, top);
            if (selectmultiple.result.Success && selectmultiple.datas != null)
            {
                foreach (var data in selectmultiple.datas)
                {
                    var domain = iterator.MoveNext() ? iterator.Current : (V)Activator.CreateInstance(typeof(V),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, 
                        null, new object[] { data }, CultureInfo.CurrentCulture);

                    _mapper.Clear(domain, maxdepth, 0);

                    domain.Changed = false;
                    domain.Deleted = false;

                    enumeration.Add(domain);
                }
            }

            return (selectmultiple.result, enumeration);
        }
    }
}
