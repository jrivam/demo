using library.Impl.Entities;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Impl.Domain.Model
{
    public abstract class AbstractEntityLogic<T, U, V> : AbstractEntityState<T, U>, IEntityLogic<T, U, V>
        where T : IEntity
        where U : IEntityTable<T>, IEntityRepository<T, U>, new()
        where V : class, IEntityState<T, U>
    {
        public ILogicState<T, U, V> _logic { get; protected set; }

        public AbstractEntityLogic(ILogicState<T, U, V> logic)
            : base()
        {
            _logic = logic;
        }

        public virtual V Clear()
        {
            return _logic.Clear(this as V, Data);
        }

        public virtual (Result result, V domain) Load(bool usedbcommand = false)
        {
            var load = _logic.Load(this as V, Data, usedbcommand);

            return load;
        }
        public virtual (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            (Result result, V domain) save = _logic.Save(this as V, Data, useinsertdbcommand, useupdatedbcommand);

            save.result.Append(SaveChildren());

            return save;
        }
        public virtual (Result result, V domain) Erase(bool usedbcommand = false)
        {
            (Result result, V domain) erasechildren = (EraseChildren(), default(V));

            if (erasechildren.result.Success)
            {
                var erase = _logic.Erase(this as V, Data, usedbcommand);

                erasechildren.domain = erase.domain;
                erasechildren.result.Append(erase.result);
            }

            return erasechildren;
        }

        protected abstract Result SaveChildren();
        protected abstract Result EraseChildren();

        public void SetProperties(T entity)
        {
            Helper.SetProperties<T, V>(entity, this as V);
        }
    }
}
