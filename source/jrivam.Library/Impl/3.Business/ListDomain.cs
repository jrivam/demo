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
    public class ListDomain<T, U, V> : List<V>, IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        protected IListData<T, U> _datas;
        public virtual IListData<T, U> Datas
        {
            get
            {
                return _datas;
            }
        }

        public ListDomain(IListData<T, U> datas = null)
        {
            _datas = datas ?? new ListData<T, U>();
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
    }
}
