using Library.Impl.Persistence;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Business;
using Library.Interface.Business.Mapper;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Impl.Domain.Query
{
    public class LogicQuery<S, T, U, V> : Logic<T, U, V>, ILogicQuery<S, T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
        public LogicQuery(IMapperLogic<T, U, V> mapper)
            : base(mapper)
        {
        }

        public virtual (Result result, V domain) Retrieve(IQueryDomain<S, T, U, V> query, int maxdepth = 1, V domain = default(V))
        {
            var selectsingle = query.Data.SelectSingle(maxdepth, (domain != null ? domain.Data : default(U)));

            if (selectsingle.result.Success && selectsingle.data != null)
            {
                var instance = _mapper.CreateInstance(selectsingle.data);

                _mapper.Clear(instance, maxdepth, 0);

                _mapper.Extra(instance, maxdepth, 0);

                instance.Changed = false;
                instance.Deleted = false;

                return (selectsingle.result, instance);
            }

            return (selectsingle.result, default(V));
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<S, T, U, V> query, int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null)
        {
            var enumeration = new List<V>();

            var selectmultiple = query.Data.SelectMultiple(maxdepth, top, (domains?.Datas != null ? domains?.Datas : new ListData<T, U>()));
            if (selectmultiple.result.Success && selectmultiple.datas != null)
            {
                foreach (var data in selectmultiple.datas)
                {
                    var instance = _mapper.CreateInstance(data);

                    _mapper.Clear(instance, maxdepth, 0);

                    _mapper.Extra(instance, maxdepth, 0);

                    instance.Changed = false;
                    instance.Deleted = false;

                    enumeration.Add(instance);
                }

                return (selectmultiple.result, enumeration);
            }

            return (selectmultiple.result, default(IList<V>));
        }
    }
}
