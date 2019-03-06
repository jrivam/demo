namespace library.Interface.Data.Table
{
    public interface ITableColumn : IColumn
    {
        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }
        bool IsUnique { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
