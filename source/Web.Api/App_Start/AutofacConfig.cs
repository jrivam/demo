using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using library.Impl.Data.Mapper;
using library.Impl.Domain.Mapper;
using library.Impl.Entities.Reader;
using library.Impl.Presentation.Raiser;
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


            builder.RegisterGeneric(typeof(BaseReaderEntity<>))
                     .As(typeof(library.Interface.Entities.Reader.IReaderEntity<>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Entities.Repository.Repository<>))
                   .As(typeof(library.Interface.Entities.Repository.IRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(library.Impl.Entities.Repository.RepositoryBulk))
                   .As(typeof(library.Interface.Entities.Repository.IRepositoryBulk))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Entities.Repository.DbRepository<>))
                   .As(typeof(library.Interface.Entities.Repository.IDbRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(library.Impl.Entities.Repository.DbRepositoryBulk))
                   .As(typeof(library.Interface.Entities.Repository.IDbRepositoryBulk))
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseMapperRepository<,>))
                     .As(typeof(library.Interface.Data.Mapper.IMapperRepository<,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Data.Table.RepositoryTable<,>))
                   .As(typeof(library.Interface.Data.Table.IRepositoryTable<,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(library.Impl.Data.Query.RepositoryQuery<,,>))
                   .As(typeof(library.Interface.Data.Query.IRepositoryQuery<,,>))
                   .InstancePerRequest();

            builder.RegisterType(typeof(library.Impl.Data.Sql.Builder.SqlBuilderTable))
                   .As(typeof(library.Interface.Data.Sql.Builder.ISqlBuilderTable))
                   .InstancePerRequest();
            builder.RegisterType(typeof(library.Impl.Data.Sql.Builder.SqlBuilderQuery))
                   .As(typeof(library.Interface.Data.Sql.Builder.ISqlBuilderQuery))
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseMapperLogic<,,>))
                     .As(typeof(library.Interface.Domain.Mapper.IMapperLogic<,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Domain.Table.LogicTable<,,>))
                   .As(typeof(library.Interface.Domain.Table.ILogicTable<,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(library.Impl.Domain.Query.LogicQuery<,,>))
                   .As(typeof(library.Interface.Domain.Query.ILogicQuery<,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Domain.Logic<,>))
                 .As(typeof(library.Interface.Domain.ILogic<,>))
                 .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseRaiserInteractive<,,,>))
                     .As(typeof(library.Interface.Presentation.Raiser.IRaiserInteractive<,,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Presentation.Table.InteractiveTable<,,,>))
                   .As(typeof(library.Interface.Presentation.Table.IInteractiveTable<,,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(library.Impl.Presentation.Query.InteractiveQuery<,,,>))
                   .As(typeof(library.Interface.Presentation.Query.IInteractiveQuery<,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(library.Impl.Presentation.Interactive<,,>))
                 .As(typeof(library.Interface.Presentation.IInteractive<,,>))
                 .InstancePerRequest();


            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());


            //builder.RegisterType<data.Model.Empresa>()
            //       .As<IEntityTable<entities.Model.Empresa>>()
            //       .As<IEntityRepository<entities.Model.Empresa, data.Model.Empresa>>()
            //       .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}