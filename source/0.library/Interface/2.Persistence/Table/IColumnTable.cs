namespace Library.Interface.Persistence.Table
{
    public interface IColumnTable : IColumn
    {
        IBuilderTableData Table { get; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }
        bool IsUnique { get; }
        bool IsRequired { get; }

        object Value { get; set; }
        object DbValue { get; set; }
    }
}
