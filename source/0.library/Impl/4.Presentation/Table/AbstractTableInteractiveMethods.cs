using library.Impl.Entities;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;
using System.ComponentModel;

namespace library.Impl.Presentation.Table
{
    public abstract class AbstractTableInteractiveMethods<T, U, V, W> : AbstractTableInteractive<T, U, V, W>, ITableInteractiveMethods<T, U, V, W>, INotifyPropertyChanged
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>, new()
        where W : class, ITableInteractive<T, U, V>
    {
        protected readonly IInteractiveTable<T, U, V, W> _interactive;

        public AbstractTableInteractiveMethods(IInteractiveTable<T, U, V, W> interactive,
            int maxdepth = 1)
            : base(maxdepth)
        {
            _interactive = interactive;

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W presentation) operation)>((CommandAction.Load, LoadQuery(_maxdepth)), $"{Domain.Data.Description.Reference}Load");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && this.Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W presentation) operation)>((CommandAction.Save, Save()), $"{Domain.Data.Description.Reference}Save");
            }, delegate (object parameter) { return this.Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W presentation) operation)>((CommandAction.Erase, Erase()), $"{Domain.Data.Description.Reference}Erase");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(W oldvalue, W newvalue)>((this as W, this as W), $"{Domain.Data.Description.Reference}Edit");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });
        }


        public virtual (Result result, W presentation) Load(bool usedbcommand = false)
        {
            return _interactive.Load(this as W, usedbcommand);
        }
        public virtual (Result result, W presentation) LoadQuery(int maxdepth = 1)
        {
            return _interactive.LoadQuery(this as W, maxdepth);
        }

        public virtual (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            (Result result, W presentation) save = _interactive.Save(this as W, useinsertdbcommand, useupdatedbcommand);

            return save;
        }
        public virtual (Result result, W presentation) Erase(bool usedbcommand = false)
        {
            var erase = _interactive.Erase(this as W, usedbcommand);

           return erase;
        }

        public W SetProperties(T entity, bool nulls = false)
        {
            return Helper.SetProperties<T, W>(entity, this as W, nulls);
        }
    }
}
