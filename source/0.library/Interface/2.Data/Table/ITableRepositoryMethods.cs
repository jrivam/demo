using library.Impl;
using library.Interface.Data.Query;
using library.Interface.Entities;

namespace library.Interface.Data.Table
{
    public interface ITableRepositoryMethods<T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        (Result result, U data) Select(bool usedbcommand = false);

        IQueryRepositoryMethods<T, U> QuerySelect { get; }
        (Result result, U data) SelectQuery(int maxdepth = 1);

        IQueryRepositoryMethods<T, U> QueryUnique { get; }
        (Result result, U data, bool isunique) CheckIsUnique();

        (Result result, U data) Insert(bool usedbcommand = false);
        (Result result, U data) Update(bool usedbcommand = false);
        (Result result, U data) Delete(bool usedbcommand = false);
    }
}
