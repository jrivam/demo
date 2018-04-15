using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        protected const string _defaultconnectionstringname = "test.connectionstring.name";

        public Empresa()
            : this(_defaultconnectionstringname)
        {

        }
        public Empresa(entities.Model.Empresa entity)
            : this(entity, _defaultconnectionstringname)
        {

        }
        public virtual void InitDbCommands()
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
        protected const string _defaultconnectionstringname = "test.connectionstring.name";

        public Empresa()
            : this(_defaultconnectionstringname)
        {
        }
    }
}