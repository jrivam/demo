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

        public override void InitDbCommands()
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<DbParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<DbParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<DbParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<DbParameter>()));
        }

        protected data.Model.Empresas _empresas;

        public virtual data.Model.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new data.Query.Empresa();
                    query["Activo"]?.Where(true);

                    Empresas = (data.Model.Empresas)new data.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
                }
            }
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