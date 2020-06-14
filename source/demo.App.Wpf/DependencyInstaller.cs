using Autofac;

namespace demo.App.Wpf
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = demo.Presentation.DependencyInstaller.RegisterServices(builder);
            
            return builder;
        }
    }
}
