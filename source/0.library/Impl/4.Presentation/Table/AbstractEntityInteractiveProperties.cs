using library.Interface.Data;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;
using System.ComponentModel;
using System.Windows.Input;

namespace library.Impl.Presentation.Table
{
    public abstract class AbstractEntityInteractiveProperties<T, U, V, W> : IEntityInteractiveProperties<T, U, V>, INotifyPropertyChanged
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>, new()
        where W : IEntityInteractiveProperties<T, U, V>
    {
        public virtual V Domain { get; protected set; }

        public virtual ITableColumn this[string reference]
        {
            get
            {
                return Domain[reference];
            }
        }

        protected int _maxdepth;

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual ICommand ClearCommand { get; protected set; }

        public virtual ICommand LoadCommand { get; protected set; }
        public virtual ICommand SaveCommand { get; protected set; }
        public virtual ICommand EraseCommand { get; protected set; }

        public virtual ICommand EditCommand { get; protected set; }

        public AbstractEntityInteractiveProperties(int maxdepth = 1)
        {
            _maxdepth = maxdepth;
        }
    }
}
