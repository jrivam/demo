using System.Windows.Input;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface ITableCommands
    {
        ICommand LoadCommand { get; }
        ICommand SaveCommand { get; }
        ICommand EraseCommand { get; }
        ICommand EditCommand { get; }
    }
}
