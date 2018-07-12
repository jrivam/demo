using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Table
{
    public interface ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>
    {
        V Clear(V domain, ITableRepositoryMethods<T, U> entityrepository);

        (Result result, V domain) Load(V domain, ITableRepositoryMethods<T, U> entityrepository, bool usedbcommand = false);
        (Result result, V domain) LoadQuery(V domain, ITableRepositoryMethods<T, U> entityrepository, int maxdepth = 1);
        (Result result, V domain) Save(V domain, ITableRepositoryMethods<T, U> entityrepository, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V domain, ITableRepositoryMethods<T, U> entityrepository, bool usedbcommand = false);
    }
}
