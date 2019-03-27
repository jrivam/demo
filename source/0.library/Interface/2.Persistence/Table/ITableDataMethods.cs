using Library.Impl;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;

namespace Library.Interface.Persistence.Table
{
    public interface ITableDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Select(bool usedbcommand = false);

        IQueryData<T, U> QuerySelect { get; }
        (Result result, U data) SelectQuery(int maxdepth = 1);

        IQueryData<T, U> QueryUnique { get; }
        (Result result, U data, bool isunique) CheckIsUnique();

        (Result result, U data) Insert(bool usedbcommand = false);
        (Result result, U data) Update(bool usedbcommand = false);
        (Result result, U data) Delete(bool usedbcommand = false);
    }
}
