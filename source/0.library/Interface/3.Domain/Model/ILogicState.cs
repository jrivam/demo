using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Entities;

namespace library.Interface.Domain.Model
{
    public interface ILogicState<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        V Clear(V domain, IEntityRepository<T, U> entityrepository);

        (Result result, V domain) Load(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false);
        (Result result, V domain) Save(V domain, IEntityRepository<T, U> entityrepository, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false);
    }
}
