using library.Impl;
using library.Interface.Data.Query;
using library.Interface.Entities;

namespace library.Interface.Data.Table
{
    public interface IEntityRepositoryMethods<T, U> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
    {
        U Clear();

        (Result result, U data) Select(bool usedbcommand = false);
        (Result result, U data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<T, U> query = default(IQueryRepositoryMethods<T, U>));
        (Result result, U data) Insert(bool usedbcommand = false);
        (Result result, U data) Update(bool usedbcommand = false);
        (Result result, U data) Delete(bool usedbcommand = false);
    }
}
