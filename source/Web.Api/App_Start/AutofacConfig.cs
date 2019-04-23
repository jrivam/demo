using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Library.Impl.Persistence.Mapper;
using Library.Impl.Domain.Loader;
using Library.Impl.Entities.Reader;
using Library.Impl.Presentation.Raiser;
using System.Reflection;
using System.Web.Http;

namespace Web.Api.App_Start
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
                     .As(typeof(Library.Interface.Entities.Reader.IReader<>))
                     .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseMapper<,>))
                     .As(typeof(Library.Interface.Persistence.Mapper.IMapper<,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Persistence.Table.RepositoryTable<,>))
                   .As(typeof(Library.Interface.Persistence.Table.IRepositoryTable<,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Persistence.Query.RepositoryQuery<,>))
                   .As(typeof(Library.Interface.Persistence.Query.IRepositoryQuery<,>))
                   .InstancePerRequest();

            builder.RegisterType(typeof(Library.Impl.Persistence.Sql.Builder.SqlBuilderTable))
                   .As(typeof(Library.Interface.Persistence.Sql.Builder.ISqlBuilderTable))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Persistence.Sql.Builder.SqlBuilderQuery))
                   .As(typeof(Library.Interface.Persistence.Sql.Builder.ISqlBuilderQuery))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Persistence.Sql.Repository.SqlRepository<>))
                   .As(typeof(Library.Interface.Persistence.Sql.Repository.ISqlRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Persistence.Sql.Repository.SqlRepositoryBulk))
                   .As(typeof(Library.Interface.Persistence.Sql.Repository.ISqlRepositoryBulk))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Persistence.Database.DbRepository<>))
                   .As(typeof(Library.Interface.Persistence.Database.IDbRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Persistence.Database.DbRepositoryBulk))
                   .As(typeof(Library.Interface.Persistence.Database.IDbRepositoryBulk))
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseLoader<,,>))
                     .As(typeof(Library.Interface.Business.Loader.ILoader<,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Domain.Table.LogicTable<,,>))
                   .As(typeof(Library.Interface.Business.Table.ILogicTable<,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Domain.Query.LogicQuery<,,,>))
                   .As(typeof(Library.Interface.Business.Query.ILogicQuery<,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Domain.Logic<,,>))
                 .As(typeof(Library.Interface.Business.ILogic<,,>))
                 .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseRaiser<,,,>))
                     .As(typeof(Library.Interface.Presentation.Raiser.IRaiser<,,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Presentation.Table.InteractiveTable<,,,>))
                   .As(typeof(Library.Interface.Presentation.Table.IInteractiveTable<,,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Presentation.Query.InteractiveQuery<,,,,,,>))
                   .As(typeof(Library.Interface.Presentation.Query.IInteractiveQuery<,,,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Presentation.Interactive<,,,>))
                 .As(typeof(Library.Interface.Presentation.IInteractive<,,,>))
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