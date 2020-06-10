using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Query
{
    public class RepositoryQuery<T, U> : Repository, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly ISqlBuilderQuery _sqlbuilder;
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;

        protected readonly IDataMapper _mapper;

        public RepositoryQuery(ConnectionStringSettings connectionstringsettings,
            ISqlCreator creator,
            IDbCommandExecutor dbcommandexecutor, IDbCommandExecutorBulk dbcommandexecutorbulk,
            IDataMapper mapper)
             : base(connectionstringsettings,
                  creator,
                  dbcommandexecutor, dbcommandexecutorbulk)
        {
            _sqlbuilder = AutofacConfiguration.Container.Resolve<ISqlBuilderQuery>(new NamedParameter("sqlsyntaxsign", SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)));
            _sqlcommandbuilder = SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName);

            _mapper = mapper;
        }

        public virtual (Result result, U data) SelectSingle(IQueryData<T, U> query,
            int maxdepth = 1, 
            U data = default(U))
        {
            var select = Select(query, maxdepth, 1, data != null ? new ListData<T, U>() { data } : null);

            return (select.result, select.datas?.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) Select(IQueryData<T, U> query,
            int maxdepth = 1, int top = 0, 
            IListData<T, U> datas = null)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuilder.GetSelectColumns(querycolumns),
                _sqlbuilder.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilder.GetWhere(querycolumns, parameters), top);

            return Select(selectcommandtext, CommandType.Text, parameters, maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) Select(string commandtext, CommandType commandtype = CommandType.Text, 
            IList<SqlParameter> parameters = null,
            int maxdepth = 1, 
            IListData<T, U> datas = null)
        {
            var executequery = ExecuteQuery(commandtext, commandtype, parameters, maxdepth, datas?.Entities ?? new Collection<T>());
            if (executequery.result.Success)
            {
                var enumeration = new List<U>();

                if (executequery.entities?.Count() > 0)
                {
                    foreach (var entity in executequery.entities)
                    {
                        var instance = HelperTableRepository<T, U>.CreateData(entity);

                        _mapper.Map<T, U>(instance, maxdepth);

                        enumeration.Add(instance);
                    }
                }
                return (executequery.result, enumeration);
            }

            return (executequery.result, default(IList<U>));
        }

        public virtual (Result result, int rows) Update(IQueryData<T, U> query,
            IList<IColumnTable> columns,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var updatecommandtext = _sqlcommandbuilder.Update($"{query.Description.DbName}",
                _sqlbuilder.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilder.GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters),
                _sqlbuilder.GetWhere(querycolumns, parameters));

            return ExecuteNonQuery(updatecommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, int rows) Delete(IQueryData<T, U> query,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.DbName }, maxdepth, 0);

            var deletecommandtext = _sqlcommandbuilder.Delete($"{query.Description.DbName}", 
                _sqlbuilder.GetFrom(queryjoins, query.Description.DbName),
                _sqlbuilder.GetWhere(querycolumns, parameters));

            return ExecuteNonQuery(deletecommandtext, CommandType.Text, parameters);
        }
    }
}
