using jrivam.Library.Extension;
using jrivam.Library.Impl.Business.Attributes;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Business.Table
{
    public abstract class AbstractTableDomain<T, U, V> : ITableDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
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

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public IList<(string name, IColumnValidator validator)> Validations { get; set; } = new List<(string, IColumnValidator)>();

        protected virtual void Init()
        {
        }

        protected readonly ILogicTable<T, U, V> _logic;

        public AbstractTableDomain(U data,
            ILogicTable<T, U, V> logic)
        {
            _logic = logic;

            Data = data;

            Init();
        }

        public virtual Result Validate()
        {
            var result = new Result() { Success = true };

            foreach (var validation in Validations)
            {
                result.Append(validation.validator.Validate());
            }

            return result;
        }
        public virtual Result Validate(string name)
        {
            return Validations.FirstOrDefault(x => x.name == name).validator.Validate();
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

            foreach (var property in typeof(V).GetPropertiesFromType(iscollection: true, attributetypes: new System.Type[] { typeof(DomainAttribute) }))
            {
                var collection = property.info.GetValue(this);
                if (collection != null)
                {
                    savechildren.Append((Result)collection.GetType().GetMethod("SaveAll").Invoke(collection, null));
                }
            }

            return savechildren;
        }
        protected virtual Result EraseChildren()
        {
            var erasechildren = new Result() { Success = true };

            foreach (var property in typeof(V).GetPropertiesFromType(iscollection: true, attributetypes: new System.Type[] { typeof(DomainAttribute) }))
            {
                var collection = property.info.GetValue(this);
                if (collection != null)
                {
                    var refresh = collection.GetType().GetMethod("Refresh").Invoke(collection, new object[] { null });
                    var item2 = refresh.GetType().GetField("Item2").GetValue(refresh);
                    if (item2 != null)
                    {
                        erasechildren.Append((Result)item2.GetType().GetMethod("EraseAll").Invoke(item2, null));
                    }
                }
            }

            return erasechildren;
        }
    }
}
