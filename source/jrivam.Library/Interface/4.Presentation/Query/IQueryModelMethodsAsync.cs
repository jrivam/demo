using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Presentation.Query
{
    public interface IQueryModelMethodsAsync<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        Task<(Result result, W model)> RetrieveAsync(int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, IEnumerable<W> models)> ListAsync(int maxdepth = 1, int top = 0, IDbConnection connection = null, int? commandtimeout = null);
    }
}
