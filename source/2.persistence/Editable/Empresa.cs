﻿using Library.Impl;
using Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace Persistence.Table
{
    public partial class Empresa
    {
        public Empresa(Entities.Table.Empresa entity)
            : this(entity, "test.connectionstring.name")
        {
        }

        public override void InitX()
        {
            SelectDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_select", Type = CommandType.StoredProcedure });
            InsertDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_insert", Type = CommandType.StoredProcedure });
            UpdateDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_update", Type = CommandType.StoredProcedure });
            DeleteDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_delete", Type = CommandType.StoredProcedure });
        }

        public virtual (Result result, Persistence.Table.Sucursales datas) Sucursales_Refresh(int maxdepth = 1, int top = 0, Persistence.Query.Sucursal querysucursal = null)
        {
            if (this.Id != null)
            {
                var query = querysucursal ?? new Persistence.Query.Sucursal();

                query.IdEmpresa = (this.Id, WhereOperator.Equals);

                var selectmultiple = query.Select(maxdepth, top);

                Sucursales = (Persistence.Table.Sucursales)new Persistence.Table.Sucursales().Load(selectmultiple.datas);

                return (selectmultiple.result, _sucursales);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Sucursales_Refresh", $"Id in {this?.Description?.Name} cannot be null") } }, null);
        }
    }
}

namespace Persistence.Query
{
    public partial class Empresa
    {
        public Empresa()
            : this("test.connectionstring.name")
        {
        }
    }
}
