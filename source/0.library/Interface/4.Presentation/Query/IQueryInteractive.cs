﻿using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using System.Collections.Generic;

namespace library.Interface.Presentation.Query
{
    public interface IQueryInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        (Result result, W presentation) Retrieve(int maxdepth, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null);
    }
}