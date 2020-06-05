using Autofac;
using jrivam.Library;
using System.Configuration;

namespace demo.Persistence
{
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            AutofacConfiguration.ConnectionStringSettings = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["test.connectionstring.name"]];

            return demo.Entities.DependencyInstaller.RegisterServices(builder);
        }
    }
}
