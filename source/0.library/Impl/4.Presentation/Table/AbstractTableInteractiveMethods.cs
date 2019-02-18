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

        public AbstractTableInteractiveMethods(V domain, IInteractiveTable<T, U, V, W> interactive,
            int maxdepth = 1)
            : base(domain, maxdepth)
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
            Status = "Loading...";
            var load = _interactive.Load(this as W, usedbcommand);

            Status = (load.result.Success) ? "Loaded." : load.result.Message;

            return load;
        }
        public virtual (Result result, W presentation) LoadQuery(int maxdepth = 1)
        {
            Status = "Loading...";
            var loadquery = _interactive.LoadQuery(this as W, maxdepth);

            Status = (loadquery.result.Success) ? "Loaded." : loadquery.result.Message;

            return loadquery;
        }

        public virtual (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            Status = "Saving...";
            var save = _interactive.Save(this as W, useinsertdbcommand, useupdatedbcommand);

            Status = (save.result.Success) ? "Saved." : save.result.Message;

            return save;
        }
        public virtual (Result result, W presentation) Erase(bool usedbcommand = false)
        {
            Status = "Deleting...";
            var erase = _interactive.Erase(this as W, usedbcommand);

            Status = (erase.result.Success) ? "Deleted." : erase.result.Message;

            return erase;
        }

        public W SetProperties(T entity, bool nulls = false)
        {
            return Helper.SetProperties<T, W>(entity, this as W, nulls);
        }
    }
}
