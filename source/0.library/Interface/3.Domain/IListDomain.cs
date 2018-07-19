using library.Impl.Data;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain
{
    public interface IListDomain<S, T, U, V> : IList<V>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where S : IQueryRepositoryMethods<T, U>
    {
        ListData<S, T, U> Datas { get; set; }
    }
}
