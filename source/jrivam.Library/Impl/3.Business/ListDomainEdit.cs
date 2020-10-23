using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business
{
    public partial class ListDomainEdit<T, U, V> : ListDomain<T, U, V>, IListDomainEditAsync<T, U, V>
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

        public virtual async Task<Result> SaveAllAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var result = new Result();

            foreach (var domain in this)
            {
                var save = await domain.SaveAsync(connection, transaction,
                    commandtimeout).ConfigureAwait(false);
                result.Append(save.result);

                if (!result.Success) break;
            }

            return result;
        }
        public virtual async Task<Result> EraseAllAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var result = new Result();

            foreach (var domain in this)
            {
                var erase = await domain.EraseAsync(connection, transaction,
                    commandtimeout).ConfigureAwait(false);
                result.Append(erase.result);

                if (!result.Success) break;
            }

            return result;
        }
    }
}
