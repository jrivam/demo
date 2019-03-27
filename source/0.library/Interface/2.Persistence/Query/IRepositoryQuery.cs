using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Query
{
    public interface IRepositoryQuery<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) 
            SelectSingle
            (IQueryData<T, U> query, 
            int maxdepth = 1, U data = default(U));

        (Result result, U data) 
            SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1, U data = default(U));

        (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IQueryData<T, U> query,
            int maxdepth = 1, int top = 0, IListData<T, U> datas = null);

        (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1, IListData<T, U> datas = null);

        (Result result, int rows)
            Update
            (IQueryData<T, U> query,
            IList<IColumnTable> tablecolumns,
            int maxdepth = 1);

        (Result result, int rows) 
            Update
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);

        (Result result, int rows)
            Delete
            (IQueryData<T, U> query,
            int maxdepth = 1);
        
        (Result result, int rows) 
            Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
