using Library.Interface.Entities;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace library.Impl.Persistence
{
    public class RepositoryMapper<T, U> : Repository<T>
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly IMapperRepository<T, U> _mapper;

        public RepositoryMapper(ISqlSyntaxSign sqlsyntaxsign,
            ISqlRepository<T> sqlrepository, ISqlRepositoryBulk sqlrepositorybulk,
            IMapperRepository<T, U> mapper)
            : base(sqlsyntaxsign,
                  sqlrepository, sqlrepositorybulk)
        {
            _mapper = mapper;
        }

        protected virtual U Map(U data, int maxdepth = 1)
        {
            _mapper.Clear(data, maxdepth, 0);
            _mapper.Map(data, maxdepth, 0);
            _mapper.Extra(data, maxdepth, 0);

            return data;
        }

        protected virtual IEnumerable<U> MapEntities(IEnumerable<T> entities, int maxdepth = 1)
        {
            foreach (var entity in entities)
            {
                var data = _mapper.CreateInstance(entity);

                Map(data, maxdepth);

                yield return data;
            }
        }
    }
}
