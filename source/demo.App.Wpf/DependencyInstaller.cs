using Autofac;

namespace demo.App.Wpf
{
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            return demo.Presentation.DependencyInstaller.RegisterServices(builder);
        }
    }
}
