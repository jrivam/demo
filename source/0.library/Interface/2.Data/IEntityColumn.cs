using library.Impl.Data.Repository;
using System;

namespace library.Interface.Data
{
    public interface IEntityColumn
    {
        Type Type { get; }

        Description ColumnDescription { get; }
        Description TableDescription { get; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
