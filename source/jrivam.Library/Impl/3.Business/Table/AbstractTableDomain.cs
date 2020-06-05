using jrivam.Library.Extension;
using jrivam.Library.Impl.Business.Attributes;
using jrivam.Library.Impl.Entities;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Query;
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

        protected IQueryDomain<T, U, V> _query;
        public virtual IQueryDomain<T, U, V> Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
            }
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public IList<(string name, IColumnValidator validator)> Validations { get; set; } = new List<(string, IColumnValidator)>();

        protected readonly ILogicTable<T, U, V> _logictable;

        public AbstractTableDomain(ILogicTable<T, U, V> logictable,
            IQueryDomain<T, U, V> query, 
            U data = default(U))
        {
            _logictable = logictable;

            Query = query;

            if (data == null)
                Data = HelperTableRepository<T, U>.CreateData(HelperEntities<T>.CreateEntity());
            else
                Data = data;

            Init();
        }

        protected virtual void Init()
        {
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
            var load = _logictable.Load(this as V, usedbcommand);

            return load;
        }
        public virtual (Result result, V domain) LoadQuery(IQueryDomain<T, U, V> query, 
            int maxdepth = 1)
        {
            var load = _logictable.LoadQuery(this as V, query, maxdepth);

            return load;
        }

        public virtual (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _logictable.Save(this as V, useinsertdbcommand, useupdatedbcommand);

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
                var erase = _logictable.Erase(this as V, usedbcommand);

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
                    savechildren.Append((Result)collection.GetType().GetMethod(nameof(IListDomainEdit<T, U, V>.SaveAll)).Invoke(collection, null));
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
                    var refresh = collection.GetType().GetMethod(nameof(IListDomainRefresh<T, U, V>.Refresh)).Invoke(collection, new object[] { null });
                    var item2 = refresh.GetType().GetField("Item2").GetValue(refresh);
                    if (item2 != null)
                    {
                        erasechildren.Append((Result)item2.GetType().GetMethod(nameof(IListDomainEdit<T, U, V>.EraseAll)).Invoke(item2, null));
                    }
                }
            }

            return erasechildren;
        }

        //public virtual void Clear(V domain)
        //{
        //}

        //public virtual void Load(V domain, int maxdepth = 1, int depth = 0)
        //{
        //}
    }
}
