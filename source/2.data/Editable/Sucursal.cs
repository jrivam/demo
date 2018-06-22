using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        protected const string _defaultappconnectionstringname = "test.connectionstring.name";

        public Sucursal()
            : this(_defaultappconnectionstringname)
        {

        }
        public Sucursal(entities.Model.Sucursal entity)
            : this(entity, _defaultappconnectionstringname)
        {

        }

        public override void InitDbCommands()
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public virtual data.Model.Empresas Empresas_Load(data.Query.Sucursal query = null)
        {
            var _query = query ?? Query;

            _query.Empresa().Activo = (true, WhereOperator.Equals);

            return _empresas = (data.Model.Empresas)new data.Model.Empresas().Load(_query?.Empresa());
        }
        protected data.Model.Empresas _empresas;
        public virtual data.Model.Empresas Empresas
        {
            get
            {
                return _empresas ?? Empresas_Load();
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
        protected const string _defaultappconnectionstringname = "test.connectionstring.name";

        public Sucursal()
            : this(_defaultappconnectionstringname)
        {            
        }
    }
}

namespace data.Mapper
{
    public partial class Sucursal
    {
        protected const string _defaultappconnectionstringname = "test.connectionstring.name";

        public Sucursal()
            : this(_defaultappconnectionstringname)
        {
        }
    }
}