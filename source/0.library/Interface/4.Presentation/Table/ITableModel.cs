using Library.Interface.Persistence.Table;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModel<T, U, V, W> : IBuilderTableModel, IDomainTable<T, U, V>, ITableModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        string Status { get; set; }

        Dictionary<string, string> Validations { get; }
        string Validation { get; }

        void OnPropertyChanged(string propertyName);
    }
}
