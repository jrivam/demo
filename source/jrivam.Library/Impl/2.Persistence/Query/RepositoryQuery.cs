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

namespace jrivam.Library.Impl.Persistence.Query
{
    public class RepositoryQuery<T, U> : IRepositoryQuery<T, U> 
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly IRepository _repository;

        protected readonly ISqlBuilderQuery _sqlbuilderquery;
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;

        protected readonly IDataMapper _datamapper;

        public RepositoryQuery(IRepository repository,
            ISqlBuilderQuery sqlbuilderquery, ISqlCommandBuilder sqlcommandbuilder,
            IDataMapper datamapper)
        {
            _repository = repository;

            _sqlbuilderquery = sqlbuilderquery;
            _sqlcommandbuilder = sqlcommandbuilder;

            _datamapper = datamapper;
        }

        public virtual (Result result, U data) SelectSingle(IQueryData<T, U> query,
            int commandtimeout = 30,
            int maxdepth = 1, 
            IDbConnection connection = null)
        {
            var select = Select(query,
                commandtimeout,
                maxdepth, 1,
                connection);

            return (select.result, select.datas?.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) Select(IQueryData<T, U> query,
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

            return Select(selectcommandtext, CommandType.Text, commandtimeout,
                parameters?.ToArray(), maxdepth,
                connection);
        }
        public virtual (Result result, IEnumerable<U> datas) Select(string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            int maxdepth = 1, 
            IDbConnection connection = null)
        {
            var executequery = _repository.ExecuteQuery<T>(
                commandtext, commandtype, commandtimeout, 
                parameters,
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

        public virtual (Result result, int rows) Update(IQueryData<T, U> query,
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

            return _repository.ExecuteNonQuery(updatecommandtext, CommandType.Text, commandtimeout, 
                parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, int rows) Delete(IQueryData<T, U> query,
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

            return _repository.ExecuteNonQuery(deletecommandtext, CommandType.Text, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
        }
    }
}
