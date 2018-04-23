using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using System.Windows.Input;

namespace library.Interface.Presentation.Model
{
    public interface IEntityView<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        V Domain { get; }

        void OnPropertyChanged(string propertyName);

        ICommand ClearCommand { get; }
        ICommand LoadCommand { get; }
        ICommand SaveCommand { get; }
        ICommand EraseCommand { get; }
        ICommand EditCommand { get; }
    }
}
