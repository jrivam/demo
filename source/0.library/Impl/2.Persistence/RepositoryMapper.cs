using Library.Interface.Entities;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Persistence
{
    public class RepositoryMapper<T, U> : Repository<T>
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly IMapper<T, U> _mapper;

        public RepositoryMapper(ISqlRepository<T> sqlrepository, ISqlRepositoryBulk sqlrepositorybulk,
            IMapper<T, U> mapper)
            : base(sqlrepository, sqlrepositorybulk)
        {
            _mapper = mapper;
        }

        protected virtual U Map(U data, int maxdepth = 1)
        {
            _mapper.Map(data, maxdepth, 0);
            _mapper.MapX(data, maxdepth, 0);

            return data;
        }

        protected virtual IEnumerable<U> MapEntities(IEnumerable<T> entities, int maxdepth = 1)
        {
            foreach (var entity in entities)
            {
                var data = HelperRepository<T, U>.CreateInstance(entity);

                Map(data, maxdepth);

                yield return data;
            }
        }
    }
}
