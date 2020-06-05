using Autofac;
using System.Configuration;

namespace jrivam.Library
{
    public class AutofacConfiguration
    {
        public static IContainer Container;

        public static ConnectionStringSettings ConnectionStringSettings;

        public static IContainer BuildContainer(ContainerBuilder builder)
        {
            Container = builder.Build();

            return Container;
        }
    }
}
