using library.Impl;
using library.Interface.Data;
using library.Interface.Domain;

namespace library.Interface.Business
{
    public interface IEntityLogic<T, U, V> where T : IEntity
                                            where U : IEntityTable<T>
                                            where V : IEntityState<T, U>
    {
        V Clear();

        (Result result, V business) Load();
        (Result result, V business) Save();
        (Result result, V business) Erase();
    }
}
