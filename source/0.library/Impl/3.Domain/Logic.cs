using library.Interface.Data.Model;
using library.Interface.Domain;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Impl.Domain
{
    public class Logic<T, U, V> : ILogic<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        protected readonly IMapperState<T, U, V> _mapper;

        public Logic(IMapperState<T, U, V> mapper)
        {
            _mapper = mapper;
        }
    }
}
