using library.Impl.Entities;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using System.ComponentModel;

namespace library.Impl.Presentation.Model
{
    public abstract class AbstractEntityInteractive<T, U, V, W> : AbstractEntityView<T, U, V, W>, IEntityView<T, U, V>, INotifyPropertyChanged
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
        }

        public virtual W Clear()
        {
            return _interactive.Clear(this as W, Domain);
        }

        public virtual (Result result, W presentation) Load(bool usedbcommand = false)
        {
            var load = _interactive.Load(this as W, Domain, usedbcommand);

            return load;
        }

        public virtual (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _interactive.Save(this as W, Domain, useinsertdbcommand, useupdatedbcommand);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, W presentation) Erase(bool usedbcommand = false)
        {
            EraseDependencies();

            var erase = _interactive.Erase(this as W, Domain, usedbcommand);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
        }
        protected virtual void EraseDependencies()
        {
        }

        public void SetProperties(T entity)
        {
            Helper.SetProperties<T, W>(entity, this as W);
        }
    }
}
