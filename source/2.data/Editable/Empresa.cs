using library.Impl.Data.Sql;
using library.Interface.Data;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        public Empresa()
           : this("test.connectionstring.name", "empresa", "Empresa")
        {
            SelectDbCommand = ("gsp_empresa_select", CommandType.StoredProcedure, new List<DbParameter>());
            InsertDbCommand = ("gsp_empresa_insert", CommandType.StoredProcedure, new List<DbParameter>());
            UpdateDbCommand = ("gsp_empresa_update", CommandType.StoredProcedure, new List<DbParameter>());
            DeleteDbCommand = ("gsp_empresa_delete", CommandType.StoredProcedure, new List<DbParameter>());
        }
    }
}

namespace data.Query
{
    public partial class Empresa
    {
        public Empresa()
          : this("test.connectionstring.name", "empresa", "Empresa")
        {
        }
    }
}