using Library.Impl;
using Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace Persistence.Table
{
    public partial class Sucursal
    {
        public Sucursal(Entities.Table.Sucursal entity)
            : this(entity, "test.connectionstring.name")
        {
        }

        public override void InitX()
        {
            SelectDbCommand = (false, ("gsp_sucursal_select", CommandType.StoredProcedure, new List<SqlParameter>()));
            InsertDbCommand = (false, ("gsp_sucursal_insert", CommandType.StoredProcedure, new List<SqlParameter>()));
            UpdateDbCommand = (false, ("gsp_sucursal_update", CommandType.StoredProcedure, new List<SqlParameter>()));
            DeleteDbCommand = (false, ("gsp_sucursal_delete", CommandType.StoredProcedure, new List<SqlParameter>()));
        }

        public virtual (Result result, Persistence.Table.Empresa data) Empresa_Refresh(int maxdepth = 1, Persistence.Query.Empresa queryempresa = null)
        {
            if (this.IdEmpresa != null)
            {
                var query = queryempresa ?? new Persistence.Query.Empresa();

                query.Id = (this.IdEmpresa, WhereOperator.Equals);

                var selectsingle = query?.SelectSingle(maxdepth);

                Empresa = selectsingle?.data;

                return (selectsingle?.result, _empresa);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Empresa_Refresh", $"IdEmpresa in {this.Description.Name} cannot be null") } }, null);
        }
        public virtual (Result result, Persistence.Table.Empresas datas) Empresas_Refresh(int maxdepth = 1, int top = 0, Persistence.Query.Empresa queryempresa = null)
        {
            var query = queryempresa ?? new Persistence.Query.Empresa();

            query.Activo = (true, WhereOperator.Equals);

            var selectmultiple = query.Select(maxdepth, top);

            Empresas = (Persistence.Table.Empresas)new Persistence.Table.Empresas().Load(selectmultiple.datas);

            return (selectmultiple.result, _empresas);
        }
    }
}

namespace Persistence.Query
{
    public partial class Sucursal
    {
        public Sucursal()
            : this("test.connectionstring.name")
        {            
        }
    }
}
