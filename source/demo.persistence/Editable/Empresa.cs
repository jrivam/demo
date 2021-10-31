using jrivam.Library.Impl.Persistence.Sql;
using System.Data;

namespace demo.Persistence.Table
{
    public partial class Empresa
    {
        protected override void Init()
        {
            base.Init();

            //SelectDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_select", Type = CommandType.StoredProcedure });
            //InsertDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_insert", Type = CommandType.StoredProcedure });
            //UpdateDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_update", Type = CommandType.StoredProcedure });
            //DeleteDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_delete", Type = CommandType.StoredProcedure });
        }
    }
}

namespace demo.Persistence.Query
{
}
