using Autofac;
using System.Collections.Generic;
using System.Configuration;

namespace jrivam.Library
{
    public class AutofacConfiguration
    {
        public static IContainer Container;

        public static Dictionary<string, ConnectionStringSettings> ConnectionStringSettings = new Dictionary<string, ConnectionStringSettings>();

        public static IContainer BuildContainer(ContainerBuilder builder)
        {
            Container = builder.Build();

            return Container;
        }
    }
}
