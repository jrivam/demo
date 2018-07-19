using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain
{
    public interface ILogic<T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>
    {
    }
}
