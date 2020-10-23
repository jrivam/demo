using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business
{
    public interface ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
    }
}
