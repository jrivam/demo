using Autofac;

namespace demo.Entities
{ 
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            return jrivam.Library.DependencyInstaller.RegisterServices(builder);
        }
    }
}
