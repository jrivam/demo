﻿namespace Library.Interface.Data.Table
{
    public interface IColumnTable : IColumn
    {
        IBuilderTableData Table { get; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }
        bool IsUnique { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
