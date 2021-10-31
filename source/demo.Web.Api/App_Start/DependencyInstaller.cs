using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace demo.Web.Api.App_Start
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = demo.Business.DependencyInstaller.RegisterServices(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder;
        }
    }
}
