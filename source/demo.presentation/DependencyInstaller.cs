using Autofac;

namespace demo.Presentation
{ 
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = demo.Business.DependencyInstaller.RegisterServices(builder);
            
            return builder;
        }
    }
}
