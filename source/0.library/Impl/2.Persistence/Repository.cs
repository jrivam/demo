using library.Interface.Persistence;
using Library.Interface.Entities;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Providers;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;

namespace library.Impl.Persistence
{
    public class Repository<T, U> : IRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlRepository<T> _repository;
        protected readonly ISqlRepositoryBulk _repositorybulk;

        protected readonly IMapperRepository<T, U> _mapper;

        protected readonly ISqlSyntaxSign _syntaxsign;
        protected readonly ISqlCommandBuilder _commandbuilder;

        public Repository(ISqlRepository<T> repository, ISqlRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
        {
            _repository = repository;
            _repositorybulk = repositorybulk;

            _mapper = mapper;
            _syntaxsign = syntaxsign;
            _commandbuilder = commandbuilder;
        }
    }
}
