using Library.Interface.Business;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Business
{
    public class ListDomain<T, U, V> : List<V>, IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        protected IListData<T, U> _datas;
        public virtual IListData<T, U> Datas
        {
            get
            {
                return _datas;
            }
        }

        public ListDomain()
        {
        }
        public ListDomain(IListData<T, U> datas)
            : this()
        {
            _datas = datas;
            _datas?.ToList()?.ForEach(x => this.Add(Business.HelperTableLogic<T, U, V>.CreateDomain(x)));    
        }

        public virtual IListDomain<T, U, V> Load(IEnumerable<V> domains)
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
            var result = new Result() { Success = true };

            foreach (var domain in this)
            {
                result.Append(domain.Save().result);

                if (!result.Success) break;
            }

            return result;
        }
        public virtual Result EraseAll()
        {
            var result = new Result() { Success = true };

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
            if (!domain.Deleted)
            {
                if (Datas.ItemAdd(domain.Data))
                {
                    this.Add(domain);

                    return true;
                }
            }

            return false;
        }

        public virtual bool ItemRemove(V domain)
        {
            if (Datas.ItemRemove(domain.Data))
            {
                this.Remove(domain);

                return true;
            }

            return false;
        }
    }
}
