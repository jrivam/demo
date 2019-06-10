using Library.Impl;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Table
{
    public interface ITableDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data, bool isunique) CheckIsUnique();
        (Result result, IList<IColumnTable> columns, bool hasrequired) CheckHasRequiredColumns();
        (Result result, bool isempty) CheckIsEmptyColumn(string columnname);

        (Result result, U data) Select(bool usedbcommand = false);
        (Result result, U data) SelectQuery(int maxdepth = 1);

        (Result result, U data) Insert(bool usedbcommand = false);
        (Result result, U data) Update(bool usedbcommand = false);
        (Result result, U data) Delete(bool usedbcommand = false);
    }
}
