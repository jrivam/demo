using System.Windows.Input;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModelCommands
    {
        ICommand LoadCommand { get; }
        ICommand SaveCommand { get; }
        ICommand EraseCommand { get; }
        ICommand EditCommand { get; }
    }
}
