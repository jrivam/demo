using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Query
{
    public interface IRepositoryQuery<T, U>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        (Result result, U data) SelectSingle(IQueryRepositoryProperties querytable, int maxdepth = 1, U data = default(U));
        (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, U data = default(U));
        (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1, U data = default(U));

        (Result result, IEnumerable<U> datas) SelectMultiple(IQueryRepositoryProperties querytable, int maxdepth = 1, int top = 0, IList<U> datas = null);
        (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<U> datas = null);
        (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1, IList<U> datas = null);

        (Result result, int rows) Update(IList<ITableColumn> columns, IQueryRepositoryProperties querytable, int maxdepth = 1);
        (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, int rows) Update(IDbCommand command);

        (Result result, int rows) Delete(IQueryRepositoryProperties querytable, int maxdepth = 1);
        (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, int rows) Delete(IDbCommand command);
    }
}
