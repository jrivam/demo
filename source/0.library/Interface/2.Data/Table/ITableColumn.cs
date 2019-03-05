using library.Impl.Data.Definition;
using System;

namespace library.Interface.Data.Table
{
    public interface ITableColumn
    {
        Type Type { get; }

        Description Description { get; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }
        bool IsUnique { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
