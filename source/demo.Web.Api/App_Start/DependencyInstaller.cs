using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace demo.Web.Api.App_Start
{
    public class DependencyInstaller
    {
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return demo.Business.DependencyInstaller.RegisterServices(builder);
        }
    }
}
