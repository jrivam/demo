using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace library.Impl.Presentation.Model
{
    public abstract class AbstractEntityView<T, U, V, W> : IEntityView<T, U, V>, INotifyPropertyChanged
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, new()
        where W : IEntityView<T, U, V>
    {
        public virtual V Domain { get; protected set; } = new V();

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

        public AbstractEntityView(int maxdepth = 1)
        {
            _maxdepth = maxdepth;
        }
    }
}
