using Autofac;
using jrivam.Library;

namespace demo.App.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfiguration.BuildContainer(demo.App.Cmd.DependencyInstaller.RegisterServices(new ContainerBuilder()));

            //var empresas = AutofacConfiguration.Container.Resolve<Persistence.Table.EmpresasQuery>();
            //empresas.Refresh();

            //var empresa1 = AutofacConfiguration.Container.Resolve<Persistence.Table.Empresa>();
            //empresa1.Id = 1;
            //empresa1.Select();

            //empresa1.Sucursales.Refresh();

            //var empresa2 = AutofacConfiguration.Container.Resolve<Persistence.Table.Empresa>();
            //empresa2.Id = 3;
            //empresa2.Select();


            //var empresa3 = AutofacConfiguration.Container.Resolve<Persistence.Table.Empresa>();
            //empresa3.Id = 10;
            //empresa3.RazonSocial = "rrrrzxcxzcr";
            //empresa3.Ruc = "33333";
            //empresa3.Activo = true;
            //empresa3.Insert();

            //var empresa4 = AutofacConfiguration.Container.Resolve<Persistence.Table.Empresa>();
            //empresa4.Id = 8126;
            //empresa4.Select();
            //empresa4.RazonSocial = "ccccccccccccccc";
            //empresa4.Update();

            //var sucursalesquery = AutofacConfiguration.Container.Resolve<Persistence.Table.SucursalesQuery>(new NamedParameter("maxdepth", 2));            
            //sucursalesquery.Refresh();

            var sucursales = AutofacConfiguration.Container.Resolve<Persistence.Query.Sucursal>();
            sucursales.Empresa.Id = (1, jrivam.Library.Impl.Persistence.Sql.WhereOperator.Equals);
            var r = sucursales.Select();
        }
    }
}
