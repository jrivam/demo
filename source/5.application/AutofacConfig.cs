using Autofac;
using library.Impl.Data.Mapper;
using library.Interface.Data.Table;

namespace application
{
    public class AutofacConfig
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(BaseMapperTable<,>))
                     .As(typeof(library.Interface.Data.Mapper.IMapperRepository<,>))
                     .InstancePerRequest();
            //builder.RegisterGeneric(typeof(library.Impl.Data.Repository.Repository<,>))
            //       .As(typeof(library.Interface.Data.IRepository<,>))
            //       .InstancePerRequest();
            builder.RegisterType<library.Impl.Data.Repository<entities.Model.Empresa, data.Model.Empresa>>()
                   .As<library.Interface.Data.IRepository<entities.Model.Empresa, data.Model.Empresa>>()
                   .InstancePerRequest();

            builder.RegisterType<data.Model.Empresa>()
                   .As<IEntityRepositoryProperties<entities.Model.Empresa>>()
                   .As<IEntityRepositoryMethods<entities.Model.Empresa, data.Model.Empresa>>()
                   .InstancePerRequest();

            return builder.Build();
        }
    }
}