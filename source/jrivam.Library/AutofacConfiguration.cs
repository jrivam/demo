using Autofac;
using Autofac.Builder;
using Autofac.Features.ResolveAnything;
using System.Collections.Generic;
using System.Configuration;

namespace jrivam.Library
{
    public class AutofacConfiguration
    {
        public static IContainer Container;

        //public static ConnectionStringSettings ConnectionStringSetting { get; set; }
        //public static Dictionary<string, ConnectionStringSettings> ConnectionStringSettings = new Dictionary<string, ConnectionStringSettings>();

        public static IContainer BuildContainer(ContainerBuilder builder)
        {
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            Container = builder.Build(ContainerBuildOptions.ExcludeDefaultModules);

            return Container;
        }
    }
}
