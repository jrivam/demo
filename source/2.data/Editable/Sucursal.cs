using library.Impl.Data.Sql;
using library.Interface.Data;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        public Sucursal(entities.Model.Sucursal entity)
            : this("test.connectionstring.name", entity)
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<DbParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<DbParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<DbParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<DbParameter>()));
        }
    }
}

namespace data.Query
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name")
        {            
        }
    }
}