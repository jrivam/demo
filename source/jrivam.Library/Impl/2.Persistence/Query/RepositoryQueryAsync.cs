using Autofac;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Query
{
    public partial class RepositoryQuery<T, U> : IRepositoryQueryAsync<T, U> 
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly IRepositoryAsync _repositoryasync;

        public RepositoryQuery(IRepositoryAsync repositoryasync,
            ISqlBuilderQuery sqlbuilderquery, ISqlCommandBuilder sqlcommandbuilder,
            IDataMapper datamapper)
            : this((IRepository)repositoryasync,
                  sqlbuilderquery, sqlcommandbuilder,
                  datamapper)
        {
            _repositoryasync = repositoryasync;
        }

        public virtual async Task<(Result result, U data)> SelectSingleAsync(IQueryData<T, U> query,
            int commandtimeout = 30,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var select = await SelectAsync(query,
                commandtimeout,
                maxdepth, 1,
                connection);

            return (select.result, select.datas?.FirstOrDefault());
        }
        public virtual async Task<(Result result, IEnumerable<U> datas)> SelectAsync(IQueryData<T, U> query,
            int commandtimeout = 30,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            var parameters = new List<ISqlParameter>();

            var querycolumns = _sqlbuilderquery.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilderquery.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuilderquery.GetSelectColumns(querycolumns),
                _sqlbuilderquery.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilderquery.GetWhere(querycolumns, parameters), top);

            return await SelectAsync(selectcommandtext, CommandType.Text, commandtimeout,
                parameters,
                maxdepth,
                connection);
        }
        public virtual async Task<(Result result, IEnumerable<U> datas)> SelectAsync(string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var executequery = await _repositoryasync.ExecuteQueryAsync<T>(
                commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
                maxdepth,
                connection);
            if (executequery.result.Success)
            {
                var enumeration = new List<U>();

                if (executequery.entities?.Count() > 0)
                {
                    foreach (var entity in executequery.entities)
                    {
                        enumeration.Add(_datamapper.Map<T, U>(HelperTableRepository<T, U>.CreateData(entity), maxdepth));
                    }
                }

                return (executequery.result, enumeration);
            }

            return (executequery.result, default(IList<U>));
        }

        public virtual async Task<(Result result, int rows)> UpdateAsync(IQueryData<T, U> query,
            IList<IColumnTable> columns,
            int commandtimeout = 30,
            int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var querycolumns = _sqlbuilderquery.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilderquery.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var updatecommandtext = _sqlcommandbuilder.Update($"{query.Description.DbName}",
                _sqlbuilderquery.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilderquery.GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
                _sqlbuilderquery.GetWhere(querycolumns, parameters));

            return await _repositoryasync.ExecuteNonQueryAsync(updatecommandtext, CommandType.Text, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
        }

        public virtual async Task<(Result result, int rows)> DeleteAsync(IQueryData<T, U> query,
            int commandtimeout = 30,
            int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var querycolumns = _sqlbuilderquery.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilderquery.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var deletecommandtext = _sqlcommandbuilder.Delete($"{query.Description.DbName}",
                _sqlbuilderquery.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilderquery.GetWhere(querycolumns, parameters));

            return await _repositoryasync.ExecuteNonQueryAsync(deletecommandtext, CommandType.Text, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
        }
    }
}
