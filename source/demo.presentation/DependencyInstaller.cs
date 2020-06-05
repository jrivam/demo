using Autofac;

namespace demo.Presentation
{ 
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            return demo.Business.DependencyInstaller.RegisterServices(builder);
        }
    }
}
