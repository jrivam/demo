using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        protected const string _defaultconnectionstringname = "test.connectionstring.name";

        public Sucursal()
            : this(_defaultconnectionstringname)
        {

        }
        public Sucursal(entities.Model.Sucursal entity)
            : this(entity, _defaultconnectionstringname)
        {

        }

        public virtual void InitDbCommands()
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
        protected const string _defaultconnectionstringname = "test.connectionstring.name";

        public Sucursal()
            : this(_defaultconnectionstringname)
        {            
        }
    }
}