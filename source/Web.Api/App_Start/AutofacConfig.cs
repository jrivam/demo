using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using library.Impl.Data.Mapper;
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

            builder.RegisterGeneric(typeof(BaseMapperTable<,>))
                     .As(typeof(library.Interface.Data.Mapper.IMapperRepository<,>))
                     .InstancePerRequest();
            builder.RegisterGeneric(typeof(library.Impl.Data.Repository<,>))
                   .As(typeof(library.Interface.Data.IRepository<,>))
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