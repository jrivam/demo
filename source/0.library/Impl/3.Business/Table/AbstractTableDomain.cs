using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Impl.Domain.Table
{
    public abstract class AbstractTableDomain<T, U, V> : ITableDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>, new()
        where V : class, ITableDomain<T, U, V>
    {
        public virtual T Entity
        {
            get
            {
                return Data.Entity;
            }
            set
            {
                Data.Entity = value;

                Changed = true;
                Deleted = false;
            }
        }

        protected U _data;
        public virtual U Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public virtual IColumnTable this[string reference]
        {
            get
            {
                return Data[reference];
            }
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        protected readonly ILogicTable<T, U, V> _logic;

        public AbstractTableDomain(U data, 
            ILogicTable<T, U, V> logic)
        {
            _logic = logic;

            Data = data;
        }

        public virtual (Result result, V domain) Load(bool usedbcommand = false)
        {
            var load = _logic.Load(this as V, usedbcommand);

            return load;
        }
        public virtual (Result result, V domain) LoadQuery(int maxdepth = 1)
        {
            var load = _logic.LoadQuery(this as V, maxdepth);

            return load;
        }
        public virtual (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _logic.Save(this as V, useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                save.result.Append(SaveChildren());
            }

            return save;
        }
        public virtual (Result result, V domain) Erase(bool usedbcommand = false)
        {
            (Result result, V domain) erasechildren = (EraseChildren(), default(V));

            if (erasechildren.result.Success)
            {
                var erase = _logic.Erase(this as V, usedbcommand);

                erasechildren.result.Append(erase.result);

                return erase;
            }

            return erasechildren;
        }

        protected virtual Result SaveChildren()
        {
            var savechildren = new Result() { Success = true };

            return savechildren;
        }
        protected virtual Result EraseChildren()
        {
            var erasechildren = new Result() { Success = true };

            return erasechildren;
        }
    }
}
