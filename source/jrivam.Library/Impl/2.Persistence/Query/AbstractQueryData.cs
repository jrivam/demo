using jrivam.Library.Extension;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Query
{
    public abstract class AbstractQueryData<T, U> : IQueryData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
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

        public void Clear()
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

        protected readonly IRepositoryQuery<T, U> _repositoryquery;

        protected AbstractQueryData(IRepositoryQuery<T, U> repositoryquery,
            string name = null, 
            string dbname = null)
        {
            _repositoryquery = repositoryquery;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();
        }

        public virtual (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U))
        {
            var selectsingle = _repositoryquery.SelectSingle(this, maxdepth, data);

            return selectsingle;
        }
        public virtual (Result result, IEnumerable<U> datas) Select(int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            var select = _repositoryquery.Select(this, maxdepth, top, datas);

            return select;
        }

        public virtual (Result result, int rows) Update(IList<IColumnTable> columns, int maxdepth = 1)
        {
            var update = _repositoryquery.Update(this, columns, maxdepth);

            return update;
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            var delete = _repositoryquery.Delete(this, maxdepth);

            return delete;
        }

        public virtual void Clear(U data)
        {
            foreach (var column in data.Columns)
            {
                data[column.Description.Name].DbValue = null;
            }
        }
        public virtual void Map(U data, int maxdepth = 1, int depth = 0)
        {
            foreach (var property in typeof(U).GetPropertiesFromType(isprimitive: true, isforeign: true, attributetypes: new System.Type[] { typeof(DataAttribute) }))
            {
                if (property.isprimitive)
                {
                    var entityproperty = typeof(T).GetPropertyFromType(property.info.Name);
                    data[property.info.Name].DbValue = entityproperty.GetValue(data.Entity);
                }

                if (property.isforeign)
                {
                    depth++;
                    if (depth < maxdepth || maxdepth == 0)
                    {
                        var foreign = property.info.GetValue(this);
                        if (foreign != null)
                        {
                            foreign.GetType().GetMethod("Map").Invoke(foreign, new object[] { foreign, maxdepth, depth });
                        }
                    }
                }
            }
        }
    }
}
