using Library.Impl.Domain;
using Library.Interface.Business.Loader;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace library.Impl.Business
{
    public class LogicLoader<T, U, V> : Logic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        protected readonly ILoader<T, U, V> _loader;

        public LogicLoader(ILoader<T, U, V> loader)
            : base()
        {
            _loader = loader;
        }

        protected virtual V Load(V domain, int maxdepth = 1)
        {
            _loader.Load(domain, maxdepth, 0);
            _loader.LoadX(domain, maxdepth, 0);

            return domain;
        }

        protected virtual IEnumerable<V> LoadDatas(IEnumerable<U> datas, int maxdepth = 1)
        {
            foreach (var data in datas)
            {
                var domain = _loader.CreateInstance(data);

                Load(domain, maxdepth);

                yield return domain;
            }
        }
    }
}
