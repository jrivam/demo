using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Table
{
    public class RepositoryTable<T, U> : Repository, IRepositoryTable<T, U> 
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly ISqlBuilderTable _sqlbuilder;
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;

        protected readonly IDataMapper _mapper;

        public RepositoryTable(ConnectionStringSettings connectionstringsettings,
            ISqlCreator creator,
            IDbCommandExecutor dbcommandexecutor, IDbCommandExecutorBulk dbcommandexecutorbulk,
            IDataMapper mapper)
            : base(connectionstringsettings,
                  creator,
                  dbcommandexecutor, dbcommandexecutorbulk)
        {
            _sqlbuilder = AutofacConfiguration.Container.Resolve<ISqlBuilderTable>(new NamedParameter("sqlsyntaxsign", SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)));
            _sqlcommandbuilder = SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName);
            
            _mapper = mapper;
        }

        protected virtual bool UseDbCommand(bool classusedbcommand, bool propertyusedbcommand, bool methodusedbcommand)
        {
            bool configusedbcommand = Convert.ToBoolean(ConfigurationManager.AppSettings["database.forceusedbcommand"]);

            return (methodusedbcommand || propertyusedbcommand || classusedbcommand || configusedbcommand);
        } 

        public virtual (Result result, U data) Select(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.SelectDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.SelectDbCommand != null)
                {
                    return Select(data, data.SelectDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Select), "No SelectDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuilder.GetSelectColumns(data.Columns),
                $"{data.Description.DbName}",
                _sqlbuilder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(data, selectcommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U data, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Select(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executequery = ExecuteQuery(commandtext, commandtype, parameters, 1, new Collection<T> { data.Entity });
            if (executequery.result.Success && executequery.entities?.Count() > 0)
            {
                data.Entity = executequery.entities.FirstOrDefault();

                _mapper.Map<T, U>(data, 1);

                return (executequery.result, data);
            }

            return (executequery.result, default(U));
        }

        public virtual (Result result, U data) Insert(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.InsertDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.InsertDbCommand != null)
                {
                    return Insert(data, data.InsertDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Insert), "No InsertDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var output = string.Empty;

            foreach (var c in data.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? " " : ", ")}";
            }

            var insertcommandtext = _sqlcommandbuilder.Insert($"{data.Description.DbName}",
                _sqlbuilder.GetInsertColumns(data.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuilder.GetInsertValues(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(data, insertcommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U data, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(data.Columns.Where(c => !c.IsIdentity).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Insert(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = ExecuteScalar(commandtext, commandtype, parameters);
            if (executescalar.result.Success && executescalar.scalar != null)
            {
                data.Entity.Id = Convert.ToInt32(executescalar.scalar);

                _mapper.Map<T, U>(data, 1);

                return (executescalar.result, data);
            }

            return (executescalar.result, default(U));
        }

        public virtual (Result result, U data) Update(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.UpdateDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.UpdateDbCommand != null)
                {
                    return Update(data, data.UpdateDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Update), "No UpdateDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var updatecommandtext = _sqlcommandbuilder.Update($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuilder.GetUpdateSet(data.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters),
                _sqlbuilder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(data, updatecommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U data, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(data.Columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Update(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = ExecuteNonQuery(commandtext, commandtype, parameters);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                _mapper.Map<T, U>(data, 1);

                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }

        public virtual (Result result, U data) Delete(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.DeleteDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.DeleteDbCommand != null)
                {
                    return Delete(data, data.DeleteDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Delete), "No DeleteDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var deletecommandtext = _sqlcommandbuilder.Delete($"{data.Description.DbName}",
                $"{data.Description.DbName}",
                _sqlbuilder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(data, deletecommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U data, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Delete(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = ExecuteNonQuery(commandtext, commandtype, parameters);
            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                return (executenonquery.result, data);
            }

            return (executenonquery.result, default(U));
        }
    }
}
