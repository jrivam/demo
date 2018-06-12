﻿using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Table
{
    public interface IEntityLogicMethods<T, U, V> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        V Clear();

        (Result result, V domain) Load(bool usedbcommand = false);
        (Result result, V domain) LoadQuery(int maxdepth = 1);
        (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(bool usedbcommand = false);
    }
}