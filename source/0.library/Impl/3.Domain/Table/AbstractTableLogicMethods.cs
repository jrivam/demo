using library.Impl.Entities;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Impl.Domain.Table
{
    public abstract class AbstractTableLogicMethods<T, U, V> : AbstractTableLogicProperties<T, U>, ITableLogicMethods<T, U, V>
        where T : IEntity
        where U : ITableRepositoryProperties<T>, ITableRepositoryMethods<T, U>, new()
        where V : class, ITableLogicProperties<T, U>
    {
        protected readonly ILogicTable<T, U, V> _logic;

        public AbstractTableLogicMethods(ILogicTable<T, U, V> logic)
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
        public virtual (Result result, V domain) LoadQuery(int maxdepth = 1)
        {
            var load = _logic.LoadQuery(this as V, Data, maxdepth);

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

        public V SetProperties(T entity, bool nulls = false)
        {
            return Helper.SetProperties<T, V>(entity, this as V, nulls);
        }
    }
}
