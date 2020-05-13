using Library.Impl.Persistence;
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
            set
            {
                if (_datas != value)
                {
                    _datas = value;
                    _datas?.ToList().ForEach(x => this.Add(Business.HelperLogic<T, U, V>.CreateInstance(x)));
                }
            }
        }

        public ListDomain(IListData<T, U> datas)
        {
            Datas = datas;
        }
        public ListDomain()
            : this(new ListData<T, U>())
        {
        }

        public virtual IListDomain<T, U, V> Load(IEnumerable<V> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
                _datas = new ListData<T, U>().Load(this?.Select(x => x.Data));
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
