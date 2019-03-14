using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Windows.Input;

namespace library.Interface.Presentation.Table
{
    public interface ITableInteractive<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
        string Status { get; }

        Dictionary<string, string> Validations { get; }
        string Validation { get; }

        V Domain { get; set; }

        ITableColumn this[string reference] { get; }

        void OnPropertyChanged(string propertyName);

        ICommand LoadCommand { get; }
        ICommand SaveCommand { get; }
        ICommand EraseCommand { get; }
        ICommand EditCommand { get; }
    }
}
