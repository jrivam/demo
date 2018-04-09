using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        public Empresa(string connectionstringname = "test.connectionstring.name")
            : this(connectionstringname, "empresa", "Empresa")
        {
            SelectDbCommand = (false, ("gsp_empresa_select", CommandType.StoredProcedure, new List<DbParameter>()));
            InsertDbCommand = (false, ("gsp_empresa_insert", CommandType.StoredProcedure, new List<DbParameter>()));
            UpdateDbCommand = (false, ("gsp_empresa_update", CommandType.StoredProcedure, new List<DbParameter>()));
            DeleteDbCommand = (false, ("gsp_empresa_delete", CommandType.StoredProcedure, new List<DbParameter>()));
        }
    }
}

namespace data.Query
{
    public partial class Empresa
    {
        public Empresa(string connectionstringname = "test.connectionstring.name")
            : this(connectionstringname, "empresa", "Empresa")
        {
        }
    }
}