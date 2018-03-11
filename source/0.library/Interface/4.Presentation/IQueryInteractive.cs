using library.Impl;
using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
using System.Collections.Generic;

namespace library.Interface.Presentation
{
    public interface IQueryInteractive<T, U, V, W> where T : IEntity
                                                    where U : IEntityTable<T>
                                                    where V : IEntityState<T, U>
                                                    where W : IEntityView<T, U, V>
    {
        (Result result, W presentation) Retrieve(int maxdepth, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null);
    }
}
