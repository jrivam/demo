using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainEdit<T, U, V> : IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Result SaveAll(IDbConnection connection = null, IDbTransaction transaction = null);
        Result EraseAll(IDbConnection connection = null, IDbTransaction transaction = null);

        void ItemModify(V olddomain, V newdomain);
        bool ItemAdd(V domain);
        bool ItemRemove(V domain);
    }
}
