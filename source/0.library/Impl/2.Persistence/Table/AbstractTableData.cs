using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Sql;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence.Table
{
    public abstract class AbstractTableData<T, U> : ITableData<T, U>
        where T : IEntity, new()
        where U : class, ITableData<T, U>
    {
        protected T _entity;
        public virtual T Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;

                foreach (var column in Persistence.HelperRepository<T, U>.GetPropertiesValue(this as U))
                {
                    Columns[column.name].Value = column.value;
                }
            }
        }

        public virtual Description Description { get; protected set; }

        public virtual IColumnTable this[string reference]
        {
            get
            {
                return Columns[reference];
            }
        }
        public virtual ListColumns<IColumnTable> Columns { get; set; } = new ListColumns<IColumnTable>();

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? DeleteDbCommand { get; set; }

        public virtual void Init()
        {
        }
        public virtual void InitX()
        {
        }

        protected readonly IQueryData<T, U> _query;
        protected readonly IRepositoryTable<T, U> _repository;

        public AbstractTableData(T entity,
            IRepositoryTable<T, U> repository,
            IQueryData<T, U> query,
            string name, string reference)
        {
            Description = new Description(name, reference);

            _repository = repository;
            _query = query;

            Init();
            InitX();

            Entity = entity;
        }

        public virtual (Result result, U data, bool isunique) CheckIsUnique()
        {
            _query.Clear();

            var primarykeycolumn = Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn?.Value != null)
            {
                _query.Columns[primarykeycolumn.Description.Reference].Where(Columns[primarykeycolumn.Description.Reference].Value, WhereOperator.NotEquals);
            }

            var uniquecolumn = Columns.FirstOrDefault(x => x.IsUnique);
            if (uniquecolumn != null)
            {
                _query.Columns[uniquecolumn.Description.Reference].Where(Columns[uniquecolumn.Description.Reference].Value, WhereOperator.Equals);
            }

            if (uniquecolumn != null)
            {
                if (uniquecolumn.Value == null)
                {
                    return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"{uniquecolumn.Description?.Reference} cannot be null") } }, default(U), false);
                }
                if (uniquecolumn.Value is string && string.IsNullOrWhiteSpace(uniquecolumn.Value.ToString()))
                {
                    return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"{uniquecolumn.Description?.Reference} cannot be empty") } }, default(U), false);
                }

                var selectsingle = _query.SelectSingle(1);

                if (selectsingle.result.Success)
                {
                    if (selectsingle.data != null)
                    {
                        selectsingle.result.Append(new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsUnique", $"{uniquecolumn.Description.Reference} {uniquecolumn.Value} already exists in {primarykeycolumn.Table.Description.Reference}.{primarykeycolumn.Description.Reference}: {selectsingle.data?.Columns[primarykeycolumn.Description.Reference].Value}") } });
                    }

                    return (selectsingle.result, selectsingle.data, selectsingle.data == null);
                }

                return (selectsingle.result, default(U), false);
            }

            return (new Result() { Success = true }, default(U), true);
        }
        public virtual (Result result, IList<IColumnTable> columns, bool hasrequired) CheckHasRequiredColumns()
        {
            var requiredcolumns = Columns?.Where(x => x.IsRequired && string.IsNullOrWhiteSpace(x.Value?.ToString())).ToList();

            if (requiredcolumns != null)
            {
                var columnnames = string.Join(", ", requiredcolumns.Select(x => x.Description.Reference));

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckHasRequiredColumns", $"{columnnames} must have a value") } }, requiredcolumns, true);
            }

            return (new Result() { Success = true }, null, false);
        }

        public virtual (Result result, bool isempty) CheckIsEmptyColumn(string columnname)
        {
            var requiredcolumn = CheckHasRequiredColumns().columns?.FirstOrDefault(x => x.Description.Reference == columnname);

            if (requiredcolumn != null)
            {
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "CheckIsEmptyColumn", $"{requiredcolumn.Description.Reference} must have a value") } }, true);
            }

            return (new Result() { Success = true }, false);
        }

        public virtual (Result result, U data) SelectQuery(int maxdepth = 1)
        {
            _query.Clear();

            var primarykeycolumn = Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn?.Value != null)
            {
                _query.Columns[primarykeycolumn.Description.Reference].Where(Columns[primarykeycolumn.Description.Reference].Value, WhereOperator.Equals);
            }

            var selectsingle = _query.SelectSingle(maxdepth);

            return selectsingle;
        }    

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var selectsingle = _repository.Select(this as U, usedbcommand);

            return selectsingle;
        }

        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            var insert = _repository.Insert(this as U, usedbcommand);

            return insert;
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            var update = _repository.Update(this as U, usedbcommand);

            return update;
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false)
        {
            var delete = _repository.Delete(this as U, usedbcommand);

            return delete;
        }
    }
}
