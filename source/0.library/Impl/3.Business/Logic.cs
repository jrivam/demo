using Library.Interface.Data.Table;
using Library.Interface.Domain;
using Library.Interface.Domain.Mapper;
using Library.Interface.Domain.Table;
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
