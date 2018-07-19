using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        protected const string _defaultappconnectionstringname = "test.connectionstring.name";

        public Empresa()
            : this(_defaultappconnectionstringname)
        {

        }
        public Empresa(entities.Model.Empresa entity)
            : this(_defaultappconnectionstringname, entity)
        {

        }

        public override void InitDbCommands()
        {
            SelectDbCommand = (false, ("gsp_empresa_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_empresa_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_empresa_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_empresa_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }
    }
}

namespace data.Query
{
    public partial class Empresa
    {
        protected const string _defaultappconnectionstringname = "test.connectionstring.name";

        public Empresa()
            : this(_defaultappconnectionstringname)
        {
        }
    }
}
