using library.Impl.Entities;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using System.ComponentModel;

namespace library.Impl.Presentation.Model
{
    public abstract class AbstractEntityInteractive<T, U, V, W> : AbstractEntityView<T, U, V, W>, IEntityInteractive<T, U, V, W>, INotifyPropertyChanged
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>, new()
        where W : class, IEntityView<T, U, V>
    {
        public IInteractiveView<T, U, V, W> _interactive { get; protected set; }

        public AbstractEntityInteractive(IInteractiveView<T, U, V, W> interactive,
            int maxdepth = 1)
            : base(maxdepth)
        {
            _interactive = interactive;

            ClearCommand = new RelayCommand(delegate (object parameter) { Messenger.Default.Send<W>(Clear(), $"{Domain.Data.Reference}Clear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Load, LoadQuery()), $"{Domain.Data.Reference}Load");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Save, Save()), $"{Domain.Data.Reference}Save");
            }, delegate (object parameter) { return Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W entity) operation)>((CommandAction.Erase, Erase()), $"{Domain.Data.Reference}Erase");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(W oldvalue, W newvalue)>((this as W, this as W), $"{Domain.Data.Reference}Edit");
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
        public abstract (Result result, W presentation) LoadQuery();

        public virtual (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            (Result result, W presentation) save = _interactive.Save(this as W, Domain, useinsertdbcommand, useupdatedbcommand);

            save.result.Append(SaveChildren());

            return save;
        }
        public virtual (Result result, W presentation) Erase(bool usedbcommand = false)
        {
            (Result result, W presentation) erasechildren = (EraseChildren(), default(W));

            if (erasechildren.result.Success)
            {
                var erase = _interactive.Erase(this as W, Domain, usedbcommand);

                erasechildren.presentation = erase.presentation;
                erasechildren.result.Append(erase.result);
            }

            return erasechildren;
        }

        protected abstract Result SaveChildren();
        protected abstract Result EraseChildren();

        public void SetProperties(T entity)
        {
            Helper.SetProperties<T, W>(entity, this as W);
        }
    }
}
