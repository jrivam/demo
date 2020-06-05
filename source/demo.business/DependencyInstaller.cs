using Autofac;

namespace demo.Business
{ 
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            return demo.Persistence.DependencyInstaller.RegisterServices(builder);
        }
    }
}
