﻿using library.Interface.Data.Model;
using library.Interface.Entities;
using System;

namespace library.Interface.Data.Repository
{
    public interface IEntityColumn<T> 
        where T : IEntity
    {
        Type Type { get; }

        IEntityTable<T> Table { get; }

        string Name { get; }
        string Reference { get; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
