using jrivam.Library.Extension;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Query
{
    public abstract class AbstractQueryData<T, U> : IQueryData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public bool Exclude { get; set; }

        public virtual Description Description { get; protected set; }

        public virtual IColumnQuery this[string name]
        {
            get
            {
                return Columns[name];
            }
        }
        public virtual ListColumns<IColumnQuery> Columns { get; set; } = new ListColumns<IColumnQuery>();

        public virtual IList<(IColumnQuery internalkey, IColumnQuery externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            return new List<(IColumnQuery internalkey, IColumnQuery externalkey)>();
        }

        public virtual IList<(IColumnQuery column, OrderDirection flow)> Orders { get; } = new List<(IColumnQuery, OrderDirection)>();

        protected readonly IRepositoryQuery<T, U> _repositoryquery;

        protected AbstractQueryData(IRepositoryQuery<T, U> repositoryquery,
            string name = null, string dbname = null)
        {
            _repositoryquery = repositoryquery;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();
        }

        public void ClearConditions()
        {
            Columns.ForEach(x => x.Wheres.Clear());
        }

        public virtual void Init()
        {
            Columns.Clear();

            foreach (var property in typeof(U).GetPropertiesFromType(isprimitive: true, attributetypes: new System.Type[] { typeof(DataAttribute) }))
            {
                var attributes = typeof(T).GetAttributesFromProperty(property.info.Name);

                var dbname = ((ColumnAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(ColumnAttribute)))?.Name ?? property.info.Name;

                var column = (IColumnQuery)Activator.CreateInstance(typeof(ColumnQuery<>).MakeGenericType(property.info.PropertyType),
                                    new object[] { this, property.info.Name, dbname });

                Columns.Add(column);
            }
        }

        public virtual (Result result, U data) SelectSingle(int? commandtimeout = null,
            int maxdepth = 1, 
            IDbConnection connection = null)
        {
            var selectsingle = _repositoryquery.SelectSingle(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection);

            return selectsingle;
        }
        public virtual async Task<(Result result, U data)> SelectSingleAsync(int? commandtimeout = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var selectsingle = await _repositoryquery.SelectSingleAsync(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection);

            return selectsingle;
        }

        public virtual (Result result, IEnumerable<U> datas) Select(int? commandtimeout = null, 
            int maxdepth = 1, int top = 0, 
            IDbConnection connection = null)
        {
            var select = _repositoryquery.Select(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth, top,
                connection);

            return select;
        }
        public virtual async Task<(Result result, IEnumerable<U> datas)> SelectAsync(int? commandtimeout = null,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            var select = await _repositoryquery.SelectAsync(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth, top,
                connection);

            return select;
        }

        public virtual (Result result, int rows) Update(IList<IColumnTable> columns,
            int? commandtimeout = null,
            int maxdepth = 1, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = _repositoryquery.Update(this, columns,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection, transaction);

            return update;
        }
        public virtual async Task<(Result result, int rows)> UpdateAsync(IList<IColumnTable> columns,
            int? commandtimeout = null,
            int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = await _repositoryquery.UpdateAsync(this, columns,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection, transaction);

            return update;
        }

        public virtual (Result result, int rows) Delete(int? commandtimeout = null, 
            int maxdepth = 1, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = _repositoryquery.Delete(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection, transaction);

            return delete;
        }
        public virtual async Task<(Result result, int rows)> DeleteAsync(int? commandtimeout = null,
            int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = await _repositoryquery.DeleteAsync(this,
                Helper.CommandTimeout(commandtimeout),
                maxdepth,
                connection, transaction);

            return delete;
        }
    }
}
