using library.Interface.Data.Table;
using library.Interface.Domain;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Impl.Domain
{
    public class Logic<T, U, V> : ILogic<T, U, V> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>
    {
        protected readonly IMapperLogic<T, U, V> _mapper;

        public Logic(IMapperLogic<T, U, V> mapper)
        {
            _mapper = mapper;
        }
    }
}
