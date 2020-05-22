using Library.Impl;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business
{
    public interface IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Result SaveAll();
        Result EraseAll();

        void ItemEdit(V olddomain, V newdomain);
        bool ItemAdd(V domain);
        bool ItemRemove(V domain);
    }
}
