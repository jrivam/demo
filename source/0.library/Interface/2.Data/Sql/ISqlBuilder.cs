using library.Impl.Data;
using library.Impl.Data.Sql;
using library.Interface.Entities;
using library.Interface.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilder<T> where T : IEntity
    {
        ISqlSyntaxSign SyntaxSign { get; }

        IDbDataParameter GetParameter(string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input);
        IDbCommand GetCommand(string commandtext = "", CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);

        IDbCommand Select(IQueryTable querytable, int maxdepth = 1, int top = 0);
        IDbCommand Update(IEntityTable<T> entitytable, IQueryTable querytable, int maxdepth = 1);
        IDbCommand Delete(IQueryTable querytable, int maxdepth = 1);

        IDbCommand Select(IEntityTable<T> entitytable);

        IDbCommand Insert(IEntityTable<T> entitytable);
        IDbCommand Update(IEntityTable<T> entitytable);
        IDbCommand Delete(IEntityTable<T> entitytable);

        IEnumerable<(IEntityColumn<T> column, IDbDataParameter parameter)> GetEntityParameters(IList<IEntityColumn<T>> columns, IDbCommand command);
        IEnumerable<((object value, WhereOperator sign) where, IDbDataParameter parameter, int counter)> GetQueryParameters((IQueryColumn column, IList<string> aliases, IList<string> parameters) column, IDbCommand command);
    }
}
