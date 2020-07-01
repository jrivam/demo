using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Business.Validator
{
    public class UniqueValidator<T, U> : ColumnValidator
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly IQueryData<T, U> _query;

        public UniqueValidator(IColumnTable column, IQueryData<T, U> query)
            : base(column)
        {
            _query = query;
        }

        public override Result Validate()
        {
            var validate = base.Validate();

            if (validate.Success)
            {
                _query.ClearConditions();

                var primarykeycolumns = _column.Table.Columns?.Where(x => x.IsPrimaryKey);
                if (primarykeycolumns != null)
                {
                    foreach (var primarykeycolumn in primarykeycolumns)
                    {
                        _query.Columns[primarykeycolumn.Description.Name].Where(_column.Table.Columns[primarykeycolumn.Description.Name].Value, WhereOperator.NotEquals);
                    }
                }

                _query.Columns[_column.Description.Name].Where(_column.Value, WhereOperator.Equals);

                var selectsingle = _query.SelectSingle(1);

                if (selectsingle.result.Success)
                {
                    if (selectsingle.data != null)
                    {
                        return new Result(
                            new ResultMessage()
                                {
                                    Category = ResultCategory.Error,
                                    Name = "UniqueValidation",
                                    Description =  $"{_column.Description.Name} {_column.Value} already exists in {_column.Table.Description.Name}.{_column.Description.Name}: {selectsingle.data?.Columns[_column.Description.Name].Value}"
                                }
                            );
                    }
                }
                else
                {
                    return selectsingle.result;
                }
            }

            return validate;
        }
    }
}
