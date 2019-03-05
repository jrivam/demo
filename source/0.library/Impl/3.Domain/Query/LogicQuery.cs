using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Query
{
    public class LogicQuery<T, U, V> : Logic<T, U>, ILogicQuery<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>
        where V : ITableLogic<T, U>
    {
        protected readonly IMapperLogic<T, U, V> _mapper;

        public LogicQuery(IMapperLogic<T, U, V> mapper)
            : base()
        {
            _mapper = mapper;
        }

        public virtual (Result result, V domain) Retrieve(IQueryRepositoryMethods<T, U> queryrepository, int maxdepth = 1, V domain = default(V))
        {
            var selectsingle = queryrepository.SelectSingle(maxdepth);

            if (selectsingle.result.Success && selectsingle.data != null)
            {
                domain = _mapper.CreateInstance(selectsingle.data);

                _mapper.Clear(domain, maxdepth, 0);

                _mapper.Extra(domain, maxdepth, 0);

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
                    var domain = iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(data);

                    _mapper.Clear(domain, maxdepth, 0);

                    _mapper.Extra(domain, maxdepth, 0);

                    domain.Changed = false;
                    domain.Deleted = false;

                    enumeration.Add(domain);
                }
            }

            return (selectmultiple.result, enumeration);
        }
    }
}
