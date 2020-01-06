using Library.Impl.Persistence.Sql;
using System.Data;

namespace Persistence.Table
{
    public partial class Empresa
    {
        public Empresa(Entities.Table.Empresa entity)
            : this(entity, "test.connectionstring.name")
        {
        }

        protected override void InitX()
        {
            base.InitX();

            SelectDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_select", Type = CommandType.StoredProcedure });
            InsertDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_insert", Type = CommandType.StoredProcedure });
            UpdateDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_update", Type = CommandType.StoredProcedure });
            DeleteDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_delete", Type = CommandType.StoredProcedure });
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
