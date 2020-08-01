using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainEdit<T, U, V> : IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Result SaveAll();
        Result EraseAll();

        void ItemModify(V olddomain, V newdomain);
        bool ItemAdd(V domain);
        bool ItemRemove(V domain);
    }
}
