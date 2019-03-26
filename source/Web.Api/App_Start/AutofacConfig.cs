using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Library.Impl.Data.Mapper;
using Library.Impl.Domain.Mapper;
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


            builder.RegisterGeneric(typeof(BaseReaderEntity<>))
                     .As(typeof(Library.Interface.Entities.Reader.IReaderEntity<>))
                     .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseMapperRepository<,>))
                     .As(typeof(Library.Interface.Data.Mapper.IMapperRepository<,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Data.Table.RepositoryTable<,>))
                   .As(typeof(Library.Interface.Data.Table.IRepositoryTable<,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Data.Query.RepositoryQuery<,>))
                   .As(typeof(Library.Interface.Data.Query.IRepositoryQuery<,>))
                   .InstancePerRequest();

            builder.RegisterType(typeof(Library.Impl.Data.Sql.Builder.SqlBuilderTable))
                   .As(typeof(Library.Interface.Data.Sql.Builder.ISqlBuilderTable))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Data.Sql.Builder.SqlBuilderQuery))
                   .As(typeof(Library.Interface.Data.Sql.Builder.ISqlBuilderQuery))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Data.Sql.Repository.SqlRepository<>))
                   .As(typeof(Library.Interface.Data.Sql.Repository.ISqlRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Data.Sql.Repository.SqlRepositoryBulk))
                   .As(typeof(Library.Interface.Data.Sql.Repository.ISqlRepositoryBulk))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Data.Database.DbRepository<>))
                   .As(typeof(Library.Interface.Data.Database.IDbRepository<>))
                   .InstancePerRequest();
            builder.RegisterType(typeof(Library.Impl.Data.Database.DbRepositoryBulk))
                   .As(typeof(Library.Interface.Data.Database.IDbRepositoryBulk))
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseMapperLogic<,,>))
                     .As(typeof(Library.Interface.Domain.Mapper.IMapperLogic<,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Domain.Table.LogicTable<,,>))
                   .As(typeof(Library.Interface.Domain.Table.ILogicTable<,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Domain.Query.LogicQuery<,,,>))
                   .As(typeof(Library.Interface.Domain.Query.ILogicQuery<,,,>))
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Domain.Logic<,,>))
                 .As(typeof(Library.Interface.Domain.ILogic<,,>))
                 .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseRaiserInteractive<,,,>))
                     .As(typeof(Library.Interface.Presentation.Raiser.IRaiserInteractive<,,,>))
                     .InstancePerRequest();

            builder.RegisterGeneric(typeof(Library.Impl.Presentation.Table.InteractiveTable<,,,>))
                   .As(typeof(Library.Interface.Presentation.Table.IInteractiveTable<,,,>))
                   .InstancePerRequest();
            builder.RegisterGeneric(typeof(Library.Impl.Presentation.Query.InteractiveQuery<,,,,,>))
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