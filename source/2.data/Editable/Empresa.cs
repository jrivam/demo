using library.Impl;
using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa
    {
        public Empresa(entities.Model.Empresa entity)
            : this("test.connectionstring.name", entity)
        {
        }

        public override void InitDbCommands()
        {
            SelectDbCommand = (false, ("gsp_empresa_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_empresa_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_empresa_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_empresa_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public virtual (Result result, data.Model.Sucursales datas) Sucursales_Refresh(int maxdepth = 1, int top = 0, data.Query.Empresa query = null)
        {
            if (this.Id != null)
            {
                var _query = query ?? new data.Query.Empresa();

                _query.Sucursal().IdEmpresa = (this.Id, WhereOperator.Equals);

                var load = new data.Model.Sucursales().Load(_query?.Sucursal(), maxdepth, top);

                Sucursales = (data.Model.Sucursales)load.list;

                return (load.result, _sucursales);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, $"Sucursales_Refresh: Id in {this.Description.Name} cannot be null") } }, null);
        }

    }
}

namespace data.Query
{
    public partial class Empresa
    {
        public Empresa()
            : this("test.connectionstring.name")
        {
        }
    }
}
