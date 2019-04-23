using library.Interface.Presentation.Table;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModel<T, U, V, W> : IBuilderTableModel, ITableModelValidation, IDomainTable<T, U, V>, ITableModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        string Status { get; set; }

        void OnPropertyChanged(string propertyName);
    }
}
