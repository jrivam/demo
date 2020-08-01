using Autofac;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Table
{
    public class RepositoryTable<T, U> : IRepositoryTable<T, U> 
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly IRepository _repository;

        protected readonly ISqlBuilderTable _sqlbuildertable;
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;

        protected readonly IDataMapper _datamapper;

        public RepositoryTable(IRepository repository,
            ISqlBuilderTable sqlbuildertable, ISqlCommandBuilder sqlcommandbuilder,
            IDataMapper datamapper)
        {
            _repository = repository;

            _sqlbuildertable = sqlbuildertable;
            _sqlcommandbuilder = sqlcommandbuilder;

            _datamapper = datamapper;
        }

        public virtual (Result result, U data) Select(U data,
            IDbConnection connection = null)
        {
            var parameters = new List<ISqlParameter>();

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuildertable.GetSelectColumns(data.Columns),
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(data, selectcommandtext, CommandType.Text, parameters?.ToArray(),
                connection);
        }
        public virtual (Result result, U data) Select(U data, ISqlCommand dbcommand,
            IDbConnection connection = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), dbcommand.Parameters));

            return Select(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray(),
                connection);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null,
            IDbConnection connection = null)
        {
            var executequery = _repository.ExecuteQuery<T>(
                commandtext, commandtype, parameters,
                1,
                connection);
            if (executequery.result.Success && executequery.entities?.Count() > 0)
            {
                data.Entity = executequery.entities.FirstOrDefault();

                _datamapper.Map<T, U>(data, 1);

                return (executequery.result, data);
            }

            return (executequery.result, default(U));
        }

        public virtual (Result result, U data) Insert(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var output = data.Columns.Any(c => c.IsIdentity);

            var insertcommandtext = _sqlcommandbuilder.Insert($"{data.Description.DbName}",
                _sqlbuildertable.GetInsertColumns(data.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuildertable.GetInsertValues(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(data, insertcommandtext, CommandType.Text, parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Insert(U data, ISqlCommand dbcommand,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => !c.IsIdentity).ToList(), dbcommand.Parameters));

            return Insert(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executescalar = _repository.ExecuteScalar<int>(commandtext, commandtype, parameters,
                connection, transaction);
            if (executescalar.result.Success)
            {
                data.Entity.Id = executescalar.scalar;

                _datamapper.Map<T, U>(data, 1);

                return (executescalar.result, data);
            }

            return (executescalar.result, default(U));
        }

        public virtual (Result result, U data) Update(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var updatecommandtext = _sqlcommandbuilder.Update($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetUpdateSet(data.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(data, updatecommandtext, CommandType.Text, parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Update(U data, ISqlCommand dbcommand,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.ToList(), dbcommand.Parameters));

            return Update(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executenonquery = _repository.ExecuteNonQuery(commandtext, commandtype, parameters,
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                _datamapper.Map<T, U>(data, 1);

                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }

        public virtual (Result result, U data) Delete(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var deletecommandtext = _sqlcommandbuilder.Delete($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(data, deletecommandtext, CommandType.Text, parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Delete(U data, ISqlCommand dbcommand,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), dbcommand.Parameters)) ;

            return Delete(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray(),
                connection, transaction);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executenonquery = _repository.ExecuteNonQuery(commandtext, commandtype, parameters,
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }
    }
}
