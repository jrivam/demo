using library.Impl.Entities;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System.ComponentModel;

namespace library.Impl.Presentation.Table
{
    public abstract class AbstractEntityInteractiveMethods<S, R, Q, T, U, V, W> : AbstractEntityInteractiveProperties<T, U, V, W>, IEntityInteractiveMethods<S, R, Q, T, U, V, W>, INotifyPropertyChanged
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
        where Q : IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>, IEntityLogicMethods<T, U, V>, new()
        where W : class, IEntityInteractiveProperties<T, U, V>
    {
        protected readonly IInteractiveTable<T, U, V, W> _interactive;

        public AbstractEntityInteractiveMethods(IInteractiveTable<T, U, V, W> interactive,
            int maxdepth = 1)
            : base(maxdepth)
        {
            _interactive = interactive;

            ClearCommand = new RelayCommand(delegate (object parameter) { Messenger.Default.Send<W>(Clear(), $"{Domain.Data.Description.Reference}Clear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Load, LoadQuery(_maxdepth)), $"{Domain.Data.Description.Reference}Load");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Save, Save()), $"{Domain.Data.Description.Reference}Save");
            }, delegate (object parameter) { return Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Erase, Erase()), $"{Domain.Data.Description.Reference}Erase");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(W oldvalue, W newvalue)>((this as W, this as W), $"{Domain.Data.Description.Reference}Edit");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });
        }

        public virtual W Clear()
        {
            return _interactive.Clear(this as W, Domain);
        }

        public virtual (Result result, W presentation) Load(bool usedbcommand = false)
        {
            return _interactive.Load(this as W, Domain, usedbcommand);
        }
        public virtual (Result result, W presentation) LoadQuery(int maxdepth = 1)
        {
            return _interactive.LoadQuery(this as W, Domain, maxdepth);
        }

        public virtual (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            (Result result, W presentation) save = _interactive.Save(this as W, Domain, useinsertdbcommand, useupdatedbcommand);

            return save;
        }
        public virtual (Result result, W presentation) Erase(bool usedbcommand = false)
        {
            var erase = _interactive.Erase(this as W, Domain, usedbcommand);

           return erase;
        }

        public W SetProperties(T entity)
        {
            return Helper.SetProperties<T, W>(entity, this as W);
        }
    }
}
