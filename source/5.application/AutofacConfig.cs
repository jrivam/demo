using Autofac;
using library.Impl.Data.Mapper;
using library.Interface.Data.Model;

namespace application
{
    public class AutofacConfig
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(AbstractMapperTable<,>))
                     .As(typeof(library.Interface.Data.Mapper.IMapperTable<,>))
                     .InstancePerRequest();
            //builder.RegisterGeneric(typeof(library.Impl.Data.Repository.Repository<,>))
            //       .As(typeof(library.Interface.Data.IRepository<,>))
            //       .InstancePerRequest();
            builder.RegisterType<library.Impl.Data.Repository<entities.Model.Empresa, data.Model.Empresa>>()
                   .As<library.Interface.Data.IRepository<entities.Model.Empresa, data.Model.Empresa>>()
                   .InstancePerRequest();

            builder.RegisterType<data.Model.Empresa>()
                   .As<IEntityTable<entities.Model.Empresa>>()
                   .As<IEntityRepository<entities.Model.Empresa, data.Model.Empresa>>()
                   .InstancePerRequest();

            return builder.Build();
        }
    }
}