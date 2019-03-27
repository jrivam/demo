using Library.Interface.Persistence.Table;
using Library.Interface.Business;
using Library.Interface.Business.Mapper;
using Library.Interface.Business.Table;
using Library.Interface.Entities;

namespace Library.Impl.Domain
{
    public class Logic<T, U, V> : ILogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        protected readonly IMapperLogic<T, U, V> _mapper;

        public Logic(IMapperLogic<T, U, V> mapper)
        {
            _mapper = mapper;
        }
    }
}
