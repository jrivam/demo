using Autofac;

namespace demo.Business
{ 
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder =  demo.Persistence.DependencyInstaller.RegisterServices(builder);

            return builder;
        }
    }
}
