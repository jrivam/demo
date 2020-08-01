using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Business
{
    public class ListDomainEdit<T, U, V> : ListDomain<T, U, V>, IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        public ListDomainEdit(IListDataEdit<T, U> datas = null)
            : base(datas)
        {
        }

        public virtual void ItemModify(V olddomain, V newdomain)
        {
            ((IListDataEdit<T, U>)Datas).ItemModify(olddomain.Data, olddomain.Data);
        }
        public virtual bool ItemAdd(V domain)
        {
            if (domain != null)
            {
                if (!domain.Deleted)
                {
                    if (((IListDataEdit<T, U>)Datas).ItemAdd(domain.Data))
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
                if (((IListDataEdit<T, U>)Datas).ItemRemove(domain.Data))
                {
                    this.Remove(domain);

                    return true;
                }
            }

            return false;
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
    }
}
