﻿using jrivam.Library.Impl.Persistence.Sql;
using System.Data;

namespace demo.Persistence.Table
{
    public partial class Sucursal
    {
        protected override void Init()
        {
            base.Init();

            //SelectDbCommand = (false, new SqlCommand() { Text = "gsp_sucursal_select", Type = CommandType.StoredProcedure });
            //InsertDbCommand = (false, new SqlCommand() { Text = "gsp_sucursal_insert", Type = CommandType.StoredProcedure });
            //UpdateDbCommand = (false, new SqlCommand() { Text = "gsp_sucursal_update", Type = CommandType.StoredProcedure });
            //DeleteDbCommand = (false, new SqlCommand() { Text = "gsp_sucursal_delete", Type = CommandType.StoredProcedure });
        }
    }
}

namespace demo.Persistence.Query
{
    public partial class Sucursal
    {
    }
}
