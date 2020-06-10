using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using System.Data;

namespace demo.Persistence.Table
{
    public partial class Empresa
    {
        public Empresa(string connectionstringsettingsname = "test.connectionstring.name",
            Persistence.Query.Empresa query = null, Entities.Table.Empresa entity = null, 
            string name = null, string dbname = null)
            : this(AutofacConfiguration.ConnectionStringSettings[connectionstringsettingsname],
                  query, entity, 
                  name, dbname)
        {
        }

        protected override void Init()
        {
            base.Init();

            SelectDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_select", Type = CommandType.StoredProcedure });
            InsertDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_insert", Type = CommandType.StoredProcedure });
            UpdateDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_update", Type = CommandType.StoredProcedure });
            DeleteDbCommand = (false, new SqlCommand() { Text = "gsp_empresa_delete", Type = CommandType.StoredProcedure });
        }
    }
}

namespace demo.Persistence.Query
{
    public partial class Empresa
    {
        public Empresa(string connectionstringsettingsname = "test.connectionstring.name",
            string name = null, string dbname = null)
            : this(AutofacConfiguration.ConnectionStringSettings[connectionstringsettingsname],
                  name, dbname)
        {
        }
    }
}
