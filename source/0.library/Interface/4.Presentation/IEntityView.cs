using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
using System.Windows.Input;

namespace library.Interface.Presentation
{
    public interface IEntityView<T, U, V> where T : IEntity
                                        where U : IEntityTable<T>
                                        where V : IEntityState<T, U>
    {
        V Business { get; set; }

        void OnPropertyChanged(string propertyName);

        ICommand ClearCommand { get; set; }
        ICommand LoadCommand { get; set; }
        ICommand SaveCommand { get; set; }
        ICommand EraseCommand { get; set; }
        ICommand EditCommand { get; set; }
    }
}
