using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Entities;

namespace library.Interface.Domain.Model
{
    public interface IEntityLogic<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        ILogicState<T, U, V> _logic { get; }

        V Clear();

        (Result result, V domain) Load(bool usedbcommand = false);
        (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(bool usedbcommand = false);
    }
}
