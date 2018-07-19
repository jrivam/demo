using library.Impl.Domain;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace library.Interface.Presentation
{
    public interface IListPresentation<S, R, T, U, V, W> : IList<W>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where W : class, ITableInteractive<T, U, V>, ITableInteractiveMethods<T, U, V, W>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
    {
        ListDomain<S, R, T, U, V> Domains { get; set; }
    }
}
