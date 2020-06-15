using Autofac;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using System.Configuration;

namespace demo.Persistence
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder = demo.Entities.DependencyInstaller.RegisterServices(builder);

            var connectionstringsetting = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["connectionstring.name.test"]];

            builder.Register(ctx =>
            {
                return SqlCommandBuilderFactory.Create(connectionstringsetting.ProviderName);
            }).As<ISqlCommandBuilder>();

            builder.Register(ctx =>
            {
                return SqlSyntaxSignFactory.Create(connectionstringsetting.ProviderName);
            }).As<ISqlSyntaxSign>();

            builder.RegisterType<Repository>()
                .WithParameter(
                    new TypedParameter(typeof(ConnectionStringSettings), connectionstringsetting)
                 )
                .As<IRepository>();

            /////////////////////////////////////////////////////////////////////////////

            // builder.Register((c, p) =>
            //       SqlCommandBuilderFactory.Create(p.TypedAs<string>()))
            //.As<ISqlCommandBuilder>();
            // builder.Register((c, p) =>
            //       SqlSyntaxSignFactory.Create(p.Named<string>("sqlsyntaxsign")))
            //.As<ISqlSyntaxSign>();

            // builder.RegisterType<Repository>()
            //.As<IRepository>()
            //.WithParameter(new TypedParameter(typeof(ConnectionStringSettings), ConnectionStringSetting));

            // builder.RegisterType<demo.Persistence.Query.Empresa>()
            // .As<IQueryData<demo.Entities.Table.Empresa, demo.Persistence.Table.Empresa>>()
            // .WithParameter(
            //     new ResolvedParameter(
            //             (pi, ctx) => pi.ParameterType == typeof(IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //             (pi, ctx) => ctx.Resolve<IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>>(
            //                     new TypedParameter(typeof(IRepository), ctx.Resolve<IRepository>(
            //                         new TypedParameter(typeof(ConnectionStringSettings), ConnectionStringSetting))),
            //                     new TypedParameter(typeof(ISqlBuilderQuery), ctx.Resolve<ISqlBuilderQuery>(
            //                         new TypedParameter(typeof(ISqlSyntaxSign), ctx.Resolve<ISqlSyntaxSign>(new NamedParameter("sqlsyntaxsign", ConnectionStringSetting.ProviderName))))),
            //                     new TypedParameter(typeof(ISqlCommandBuilder), ctx.Resolve<ISqlCommandBuilder>(new TypedParameter(typeof(string), ConnectionStringSetting.ProviderName)))
            //             )));


            //AutofacConfiguration.ConnectionStringSettings.Add("test.connectionstring.name", ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["test.connectionstring.name"]]);

            //builder.RegisterType<demo.Persistence.Table.Empresa>()
            //    .UsingConstructor(
            //        typeof(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //        typeof(Persistence.Query.Empresa),
            //        typeof(Entities.Table.Empresa),
            //        typeof(string),
            //        typeof(string),
            //        typeof(string)
            //     )
            //    .As<ITableData<demo.Entities.Table.Empresa, demo.Persistence.Table.Empresa>>()
            //    .WithParameter(
            //        new ResolvedParameter(
            //                (pi, ctx) => pi.ParameterType == typeof(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //                (pi, ctx) => new ResolvedParameter(
            //                        (pi2, ctx2) => pi2.ParameterType == typeof(IRepository),
            //                        (pi2, ctx2) => new ResolvedParameter(
            //                            (pi3, ctx3) => pi3.ParameterType == typeof(ConnectionStringSettings),
            //                            (pi3, ctx3) => connectionstringsettings)
            //                        )
            //                    ))
            //    .WithParameter(
            //        new ResolvedParameter(
            //                (pi, ctx) => pi.ParameterType == typeof(ISqlBuilderTable),
            //                (pi, ctx) => new ResolvedParameter(
            //                        (pi2, ctx2) => pi2.ParameterType == typeof(ISqlSyntaxSign),
            //                        (pi2, ctx2) => SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)
            //                    )
            //            )
            //        )
            //    .WithParameter(
            //        new ResolvedParameter(
            //                (pi, ctx) => pi.ParameterType == typeof(ISqlCommandBuilder),
            //                (pi, ctx) => SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName)
            //                )
            //            );

            //builder.RegisterType<Entities.Table.Empresa>()
            //    .As<IEntity>();

            //builder.RegisterType<RepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>>()
            //    .As<IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>>();
            //builder.RegisterType<demo.Persistence.Query.Empresa>()
            //.As<IQueryData<demo.Entities.Table.Empresa, demo.Persistence.Table.Empresa>>();

            //builder.RegisterType<demo.Persistence.Table.Empresa>()
            //    //.UsingConstructor(
            //    //    typeof(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //    //    typeof(IQueryData<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //    //    typeof(Entities.Table.Empresa),
            //    //    typeof(string),
            //    //    typeof(string)
            //    // )
            //.As<ITableData<demo.Entities.Table.Empresa, demo.Persistence.Table.Empresa>>();

            //builder.RegisterType<RepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>>()
            //    .As<IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>>();


            ////////////////////
            ///


            //builder.RegisterType<demo.Persistence.Table.Empresa>()
            //.UsingConstructor(
            //    typeof(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //    typeof(Persistence.Query.Empresa),
            //    typeof(Entities.Table.Empresa),
            //    typeof(string),
            //    typeof(string)
            // )
            //.As<ITableData<demo.Entities.Table.Empresa, demo.Persistence.Table.Empresa>>();
            //.WithParameter(
            //    new ResolvedParameter(
            //            (pi, ctx) => pi.ParameterType == typeof(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>),
            //            (pi, ctx) => ctx.Resolve<IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>>(
            //                    new TypedParameter(typeof(IRepository), ctx.Resolve<IRepository>(
            //                        new TypedParameter(typeof(ConnectionStringSettings), AutofacConfiguration.ConnectionStringSetting))),
            //                    new TypedParameter(typeof(ISqlBuilderTable), ctx.Resolve<ISqlBuilderTable>(
            //                        new TypedParameter(typeof(ISqlSyntaxSign), SqlSyntaxSignFactory.Create(AutofacConfiguration.ConnectionStringSetting.ProviderName)))),
            //                    new TypedParameter(typeof(ISqlCommandBuilder), SqlCommandBuilderFactory.Create(AutofacConfiguration.ConnectionStringSetting.ProviderName)))
            //            ));

            return builder;
        }
    }
}
