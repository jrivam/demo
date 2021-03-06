﻿using Library.Impl.Entities;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Builder;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Sql.Repository;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Providers;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Library.Impl.Persistence.Query
{
    public class RepositoryQuery<T, U> : RepositoryMapper<T, U>, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;
        protected readonly ISqlBuilderQuery _sqlbuilder;

        public RepositoryQuery(ISqlCommandExecutor<T> sqlcommandexecutor, ISqlCommandExecutorBulk sqlcommandexecutorbulk,
            IMapper<T, U> mapper, 
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlBuilderQuery sqlbuilder)
            : base(sqlcommandexecutor, sqlcommandexecutorbulk,
                  mapper)
        {
            _sqlcommandbuilder = sqlcommandbuilder;
            _sqlbuilder = sqlbuilder;
        }

        public RepositoryQuery(ISqlSyntaxSign sqlsyntaxsign, 
            IMapper<T, U> mapper, 
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlCommandExecutor<T> sqlcommandexecutor, ISqlCommandExecutorBulk sqlcommandexecutorbulk)
            : this(sqlcommandexecutor, sqlcommandexecutorbulk,
                  mapper, 
                  sqlcommandbuilder,
                  new SqlBuilderQuery(sqlsyntaxsign))
        {
        }
        public RepositoryQuery(IReader<T> reader, IMapper<T, U> mapper,
            ISqlSyntaxSign sqlsyntaxsign, 
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlCreator sqlcreator)
            : this(sqlsyntaxsign, 
                  mapper, 
                  sqlcommandbuilder,
                  new SqlCommandExecutor<T>(sqlcreator, reader), new SqlCommandExecutorBulk(sqlcreator))
        {
        }
        public RepositoryQuery(IReader<T> reader, IMapper<T, U> mapper, 
            ConnectionStringSettings connectionstringsettings)
            : this(reader, mapper,
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings),
                  new SqlCreator(connectionstringsettings))
        {
        }

        public RepositoryQuery(IReader<T> reader, IMapper<T, U> mapper, 
            string appconnectionstringname)
            : this(reader, mapper, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IQueryData<T, U> query,
            int maxdepth = 1, U data = default(U))
        {
            var select = Select(query, maxdepth, 1, (data != null ? new ListData<T, U>() { data } : null));

            return (select.result, select.datas != null ? select.datas.FirstOrDefault() : default(U));
        }

        public virtual (Result result, IEnumerable<U> datas)
            Select
            (IQueryData<T, U> query,
            int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuilder.GetSelectColumns(querycolumns),
                _sqlbuilder.GetFrom(queryjoins, query.Description.Name),
                _sqlbuilder.GetWhere(querycolumns, parameters), top);

            return Select(selectcommandtext, CommandType.Text, parameters, maxdepth, datas);
        }

        public virtual (Result result, IEnumerable<U> datas)
            Select
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null,
            int maxdepth = 1, IListData<T, U> datas = null)
        {
            var enumeration = new List<U>();

            var select = Select(commandtext, commandtype, parameters, maxdepth, (datas?.Entities != null ? datas?.Entities : new ListEntity<T>()));
            if (select.result.Success && select.entities?.Count() > 0)
            {
                foreach (var instance in MapEntities(select.entities, maxdepth))
                {
                    enumeration.Add(instance);
                }

                return (select.result, enumeration);
            }

            return (select.result, default(IList<U>));
        }

        public virtual (Result result, int rows) 
            Update
            (IQueryData<T, U> query,
            IList<IColumnTable> columns,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var updatecommandtext = _sqlcommandbuilder.Update($"{query.Description.Name}",
                _sqlbuilder.GetFrom(queryjoins, query.Description.Name),
                _sqlbuilder.GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters),
                _sqlbuilder.GetWhere(querycolumns, parameters));

            return Update(updatecommandtext, CommandType.Text, parameters);
        }

        public virtual (Result result, int rows) 
            Delete
            (IQueryData<T, U> query,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _sqlbuilder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _sqlbuilder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var deletecommandtext = _sqlcommandbuilder.Delete($"{query.Description.Name}", 
                _sqlbuilder.GetFrom(queryjoins, query.Description.Name),
                _sqlbuilder.GetWhere(querycolumns, parameters));

            return Delete(deletecommandtext, CommandType.Text, parameters);
        }
    }
}
