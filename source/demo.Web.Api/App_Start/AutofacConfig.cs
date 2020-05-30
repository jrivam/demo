using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using jrivam.Library.Impl.Business.Loader;
using jrivam.Library.Impl.Entities.Reader;
using jrivam.Library.Impl.Presentation.Raiser;
using System.Reflection;
using System.Web.Http;

namespace demo.Web.Api.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            builder.RegisterGeneric(typeof(BaseReader<>))
                     .As(typeof(jrivam.Library.Interface.Entities.Reader.IReader<>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Persistence.Table.RepositoryTable<,>))
                   .As(typeof(jrivam.Library.Interface.Persistence.Table.IRepositoryTable<,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Persistence.Query.RepositoryQuery<,>))
                   .As(typeof(jrivam.Library.Interface.Persistence.Query.IRepositoryQuery<,>))
                   .InstancePerRequest();

            builder.RegisterType(typeof(jrivam.Library.Impl.Persistence.Sql.Builder.SqlBuilderTable))
                   .As(typeof(jrivam.Library.Interface.Persistence.Sql.Builder.ISqlBuilderTable))
                   .InstancePerRequest();
            builder.RegisterType(typeof(jrivam.Library.Impl.Persistence.Sql.Builder.SqlBuilderQuery))
                   .As(typeof(jrivam.Library.Interface.Persistence.Sql.Builder.ISqlBuilderQuery))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Persistence.Sql.Repository.SqlCommandExecutor<>))
                   .As(typeof(jrivam.Library.Interface.Persistence.Sql.Repository.ISqlCommandExecutor<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(jrivam.Library.Impl.Persistence.Sql.Repository.SqlCommandExecutorBulk))
                   .As(typeof(jrivam.Library.Interface.Persistence.Sql.Repository.ISqlCommandExecutorBulk))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Persistence.Database.DbCommandExecutor<>))
                   .As(typeof(jrivam.Library.Interface.Persistence.Database.IDbCommandExecutor<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(jrivam.Library.Impl.Persistence.Database.DbCommandExecutorBulk))
                   .As(typeof(jrivam.Library.Interface.Persistence.Database.IDbCommandExecutorBulk))
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseLoader<,,>))
                     .As(typeof(jrivam.Library.Interface.Business.Loader.ILoader<,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Business.Table.LogicTable<,,>))
                   .As(typeof(jrivam.Library.Interface.Business.Table.ILogicTable<,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Business.Query.LogicQuery<,,,>))
                   .As(typeof(jrivam.Library.Interface.Business.Query.ILogicQuery<,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Business.Logic<,,>))
                 .As(typeof(jrivam.Library.Interface.Business.ILogic<,,>))
                 .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseRaiser<,,,>))
                     .As(typeof(jrivam.Library.Interface.Presentation.Raiser.IRaiser<,,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Presentation.Table.InteractiveTable<,,,>))
                   .As(typeof(jrivam.Library.Interface.Presentation.Table.IInteractiveTable<,,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Presentation.Query.InteractiveQuery<,,,,,,>))
                   .As(typeof(jrivam.Library.Interface.Presentation.Query.IInteractiveQuery<,,,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(jrivam.Library.Impl.Presentation.Interactive<,,,>))
                 .As(typeof(jrivam.Library.Interface.Presentation.IInteractive<,,,>))
                 .InstancePerRequest();


            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());


            //builder.RegisterType<data.Model.Empresa>()
            //       .As<IEntityTable<Entities.Table.Empresa>>()
            //       .As<IEntityRepository<Entities.Table.Empresa, data.Model.Empresa>>()
            //       .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}