using entities.Model;
using System.Data.Entity;

namespace app.cmd
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext() 
            : base(nameOrConnectionString: "testEntities") { }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TestDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
