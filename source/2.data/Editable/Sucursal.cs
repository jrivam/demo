using library.Impl.Data.Sql;
using library.Interface.Data;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name", "sucursal", "Sucursal")
        {
            SelectDbCommand = ("gsp_sucursal_select", CommandType.StoredProcedure, new List<DbParameter>());
            InsertDbCommand = ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<DbParameter>());
            UpdateDbCommand = ("gsp_sucursal_update", CommandType.StoredProcedure, new List<DbParameter>());
            DeleteDbCommand = ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<DbParameter>());
        }
    }
}

namespace data.Query
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name", "sucursal", "Sucursal")
        {            
        }
    }
}