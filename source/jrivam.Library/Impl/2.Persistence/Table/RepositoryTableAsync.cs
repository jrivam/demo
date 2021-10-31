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
using System.Threading.Tasks;

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
            int commandtimeout = 30,
            IDbConnection connection = null)
        {
            var parameters = new List<ISqlParameter>();

            var commandtext = _sqlcommandbuilder.Select(_sqlbuildertable.GetSelectColumns(data.Columns),
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(data, commandtext, CommandType.Text, commandtimeout,
                parameters,
                connection);
        }
        public virtual (Result result, U data) Select(U data,
            string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters)) ;

            var executequery = _repository.ExecuteQuery<T>(
                commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
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

        public virtual async Task<(Result result, U data)> SelectAsync(U data,
            int commandtimeout = 30,
            IDbConnection connection = null)
        {
            var parameters = new List<ISqlParameter>();

            var commandtext = _sqlcommandbuilder.Select(_sqlbuildertable.GetSelectColumns(data.Columns),
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return await SelectAsync(data, commandtext, CommandType.Text, commandtimeout,
                parameters,
                connection);
        }
        public virtual async Task<(Result result, U data)> SelectAsync(U data,
            string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters)) ;

            var executequery = await _repository.ExecuteQueryAsync<T>(
                commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
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
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var output = data.Columns.Any(c => c.IsIdentity);

            var insertcommandtext = _sqlcommandbuilder.Insert($"{data.Description.DbName}",
                _sqlbuildertable.GetInsertColumns(data.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuildertable.GetInsertValues(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(data, insertcommandtext, CommandType.Text, commandtimeout, 
                parameters,
                connection, transaction);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters));

            var executescalar = _repository.ExecuteScalar<int>(commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
            if (executescalar.result.Success)
            {
                data.Entity.Id = executescalar.scalar;

                _datamapper.Map<T, U>(data, 1);

                return (executescalar.result, data);
            }

            return (executescalar.result, default(U));
        }

        public virtual async Task<(Result result, U data)> InsertAsync(U data,
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var output = data.Columns.Any(c => c.IsIdentity);

            var insertcommandtext = _sqlcommandbuilder.Insert($"{data.Description.DbName}",
                _sqlbuildertable.GetInsertColumns(data.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuildertable.GetInsertValues(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return await InsertAsync(data, insertcommandtext, CommandType.Text, commandtimeout,
                parameters,
                connection, transaction);
        }
        public virtual async Task<(Result result, U data)> InsertAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters));

            var executescalar = await _repository.ExecuteScalarAsync<int>(commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
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
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var updatecommandtext = _sqlcommandbuilder.Update($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetUpdateSet(data.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(data, updatecommandtext, CommandType.Text, commandtimeout, 
                parameters,
                connection, transaction);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.ToList(), parameters));

            var executenonquery = _repository.ExecuteNonQuery(commandtext, commandtype, commandtimeout, 
                parameters?.ToArray(),
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                _datamapper.Map<T, U>(data, 1);

                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }

        public virtual async Task<(Result result, U data)> UpdateAsync(U data,
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var updatecommandtext = _sqlcommandbuilder.Update($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetUpdateSet(data.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return await UpdateAsync(data, updatecommandtext, CommandType.Text, commandtimeout,
                parameters,
                connection, transaction);
        }
        public virtual async Task<(Result result, U data)> UpdateAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.ToList(), parameters));

            var executenonquery = await _repository.ExecuteNonQueryAsync(commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                _datamapper.Map<T, U>(data, 1);

                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }

        public virtual (Result result, U data) Delete(U data,
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var deletecommandtext = _sqlcommandbuilder.Delete($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(data, deletecommandtext, CommandType.Text, commandtimeout,
                parameters,
                connection, transaction);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters));

            var executenonquery = _repository.ExecuteNonQuery(commandtext, commandtype, commandtimeout, 
                parameters?.ToArray(),
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }

        public virtual async Task<(Result result, U data)> DeleteAsync(U data,
            int commandtimeout = 30,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameters = new List<ISqlParameter>();

            var deletecommandtext = _sqlcommandbuilder.Delete($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuildertable.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return await DeleteAsync(data, deletecommandtext, CommandType.Text, commandtimeout,
                parameters,
                connection, transaction);
        }
        public virtual async Task<(Result result, U data)> DeleteAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, int commandtimeout = 30,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            foreach (var p in _sqlbuildertable.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters));

            var executenonquery = await _repository.ExecuteNonQueryAsync(commandtext, commandtype, commandtimeout,
                parameters?.ToArray(),
                connection, transaction);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }
    }
}
