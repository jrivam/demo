namespace jrivam.Library.Interface.Persistence.Table
{
    public interface IColumnTable : IColumn
    {
        IBuilderTableData Table { get; }

        object Value { get; set; }
        object DbValue { get; set; }

        bool IsPrimaryKey { get; }
        bool IsIdentity { get; }
    }
}
