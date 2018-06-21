using library.Interface.Data;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Windows.Input;

namespace library.Interface.Presentation.Table
{
    public interface IEntityInteractiveProperties<T, U, V> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        V Domain { get; }

        IEntityColumn this[string reference] { get; }

        void OnPropertyChanged(string propertyName);

        ICommand ClearCommand { get; }
        ICommand LoadCommand { get; }
        ICommand SaveCommand { get; }
        ICommand EraseCommand { get; }
        ICommand EditCommand { get; }
    }
}
