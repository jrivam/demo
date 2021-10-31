using Autofac;
using Autofac.Integration.WebApi;
using jrivam.Library;
using System.Web.Http;

namespace demo.Web.Api.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, AutofacConfiguration.BuildContainer(DependencyInstaller.RegisterServices(new ContainerBuilder())));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}