using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Persistence.Query
{
    public abstract class AbstractQueryData<T, U> : IQueryData<T, U>
        where T : IEntity, new()
        where U : class, ITableData<T, U>
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
        }
        public virtual void InitX()
        {
        }

        protected readonly IRepositoryQuery<T, U> _repository;

        public AbstractQueryData(IRepositoryQuery<T, U> repository,
            string name, string dbname)
        {
            Description = new Description(name, dbname);

            _repository = repository;

            Init();
            InitX();
        }

        public virtual (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U))
        {
            var selectsingle = _repository.SelectSingle(this, maxdepth, data);

            return selectsingle;
        }
        public virtual (Result result, IEnumerable<U> datas) Select(int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            var select = _repository.Select(this, maxdepth, top, datas);

            return select;
        }

        public virtual (Result result, int rows) Update(IList<IColumnTable> columns, int maxdepth = 1)
        {
            var update = _repository.Update(this, columns, maxdepth);

            return update;
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            var delete = _repository.Delete(this, maxdepth);

            return delete;
        }
    }
}
