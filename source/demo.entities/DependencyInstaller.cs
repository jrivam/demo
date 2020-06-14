using Autofac;

namespace demo.Entities
{ 
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = jrivam.Library.DependencyInstaller.RegisterServices(builder);
            
            return builder;
        }
    }
}
