using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Business
{
    public class ListDomainEdit<T, U, V> : List<V>, IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        protected IListDataEdit<T, U> _datas;
        public virtual IListDataEdit<T, U> Datas
        {
            get
            {
                return _datas;
            }
        }

        public ListDomainEdit(IListDataEdit<T, U> datas = null)
        {
            _datas = datas ?? new ListDataEdit<T, U>();
            _datas?.ToList()?.ForEach(x => this.Add(Business.HelperTableLogic<T, U, V>.CreateDomain(x)));    
        }

        public virtual IListDomainEdit<T, U, V> Load(IEnumerable<V> domains)
        {
            if (domains != null)
            {
                this?.AddRange(domains);

                _datas?.Clear();
                _datas?.Load(this.Select(x => x.Data));
            }

            return this;
        }

        public virtual Result SaveAll()
        {
            var result = new Result();

            foreach (var domain in this)
            {
                result.Append(domain.Save().result);

                if (!result.Success) break;
            }

            return result;
        }
        public virtual Result EraseAll()
        {
            var result = new Result();

            foreach (var domain in this)
            {
                result.Append(domain.Erase().result);

                if (!result.Success) break;
            }

            return result;
        }

        public virtual void ItemEdit(V olddomain, V newdomain)
        {
            Datas.ItemEdit(olddomain.Data, olddomain.Data);
        }
        public virtual bool ItemAdd(V domain)
        {
            if (domain != null)
            {
                if (!domain.Deleted)
                {
                    if (Datas.ItemAdd(domain.Data))
                    {
                        this.Add(domain);

                        return true;
                    }
                }
            }

            return false;
        }
        public virtual bool ItemRemove(V domain)
        {
            if (domain != null)
            {
                if (Datas.ItemRemove(domain.Data))
                {
                    this.Remove(domain);

                    return true;
                }
            }

            return false;
        }
    }
}
