using library.Impl;
using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal
    {
        public Sucursal(entities.Model.Sucursal entity)
            : this("test.connectionstring.name", entity)
        {
        }

        public override void InitDbCommands()
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public virtual (Result result, data.Model.Empresa data) Empresa_Refresh(int maxdepth = 1, data.Query.Sucursal query = null)
        {
            if (this.IdEmpresa != null)
            {
                var _query = query ?? new data.Query.Sucursal();

                _query.Empresa().Id = (this.IdEmpresa, WhereOperator.Equals);

                var selectsingle = _query?.Empresa()?.SelectSingle(maxdepth);

                Empresa = selectsingle?.data;

                return (selectsingle?.result, _empresa);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, $"Empresa_Refresh: IdEmpresa in {this.Description.Name} cannot be null") } }, null);
        }

        public virtual (Result result, data.Model.Empresas datas) Empresas_Refresh(int maxdepth = 1, int top = 0, data.Query.Sucursal query = null)
        {
            var _query = query ?? new data.Query.Sucursal();

            _query.Empresa().Activo = (true, WhereOperator.Equals);

            var load = new data.Model.Empresas().Load(_query?.Empresa(), maxdepth, top);

            Empresas = (data.Model.Empresas)load.list;

            return (load.result, _empresas);
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
