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
    public abstract partial class AbstractQueryData<T, U> : IQueryData<T, U>
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

        protected readonly IRepositoryQueryAsync<T, U> _repositoryqueryasync;

        protected AbstractQueryData(IRepositoryQueryAsync<T, U> repositoryqueryasync,
            string name = null, string dbname = null)
        {
            _repositoryqueryasync = repositoryqueryasync;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();
        }

        public void ClearConditions()
        {
            Columns.ForEach(x => x.Wheres.Clear());
        }

        private void Init()
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

        public virtual async Task<(Result result, U data)> SelectSingleAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var selectsingle = await _repositoryqueryasync.SelectSingleAsync(this,
                maxdepth,
                connection,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return selectsingle;
        }

        public virtual async Task<(Result result, IEnumerable<U> datas)> SelectAsync(int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var select = await _repositoryqueryasync.SelectAsync(this,
                maxdepth, top,
                connection,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return select;
        }

        public virtual async Task<(Result result, int rows)> UpdateAsync(IList<IColumnTable> columns,
            int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var update = await _repositoryqueryasync.UpdateAsync(this, columns,
                maxdepth,
                connection, transaction,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return update;
        }

        public virtual async Task<(Result result, int rows)> DeleteAsync(int maxdepth = 1,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var delete = await _repositoryqueryasync.DeleteAsync(this,
                maxdepth,
                connection, transaction,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return delete;
        }
    }
}
