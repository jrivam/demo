using demo.Entities.Table;
using System.Data.Entity;

namespace demo.App.Cmd
{
    public class TestContext : DbContext
    {
        public TestContext(string name = "test.mysql.vmware")
            : base(name)
        {

        }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
    }
}
