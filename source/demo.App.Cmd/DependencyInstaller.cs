using Autofac;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Impl.Persistence.Table;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System.Configuration;
using System.Data.Entity;

namespace demo.App.Cmd
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = demo.Persistence.DependencyInstaller.RegisterServices(builder);

            ///entity framework
            //builder.RegisterGeneric(typeof(RepositoryTableEF<,>))
            //    .WithParameter(
            //        new TypedParameter(typeof(DbContext), new TestContext())
            //     )
            //    .As(typeof(IRepositoryTable<,>));
            ////.InstancePerLifetimeScope();


            ////////////////
            //builder = jrivam.Library.DependencyInstaller.RegisterServices(builder);

            //var connectionstringsetting = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["connectionstringname.test"]];

            //builder.Register(ctx =>
            //{
            //    return SqlCommandBuilderFactory.Create(connectionstringsetting.ProviderName);
            //}).As<ISqlCommandBuilder>();

            //builder.Register(ctx =>
            //{
            //    return SqlSyntaxSignFactory.Create(connectionstringsetting.ProviderName);
            //}).As<ISqlSyntaxSign>();

            //builder.RegisterType<Repository>()
            //    .WithParameter(
            //        new TypedParameter(typeof(ConnectionStringSettings), connectionstringsetting)
            //     )
            //    .As<IRepository>();

            //dapper
            //builder = jrivam.Library.DependencyInstaller.RegisterServices(builder);

            //var connectionstringsetting = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["connectionstringname.test"]];
            //builder.RegisterType(typeof(RepositoryDapper))
            //    .WithParameter(
            //        new TypedParameter(typeof(ConnectionStringSettings), connectionstringsetting)
            //     )
            //    .As(typeof(IRepository));
            //.InstancePerLifetimeScope();

            return builder;
        }
    }
}
