using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Query
{
    public interface IRepositoryQuery<S, T, U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        (Result result, U data) 
            SelectSingle
            (IQueryRepository query, 
            int maxdepth = 1);

        (Result result, U data) 
            SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1);

        (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IQueryRepository query,
            int maxdepth = 1, int top = 0);

        (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1);

        (Result result, int rows)
            Update
            (IQueryRepository query,
            IList<(ITableRepository table, ITableColumn column)> tablecolumns,
            int maxdepth = 1);

        (Result result, int rows) 
            Update
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);

        (Result result, int rows)
            Delete
            (IQueryRepository query,
            int maxdepth = 1);
        
        (Result result, int rows) 
            Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
