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

            builder.RegisterGeneric(typeof(BaseMapperRepository<,>))
                     .As(typeof(library.Interface.Data.Mapper.IMapperRepository<,>))
                     .InstancePerRequest();
            //builder.RegisterGeneric(typeof(library.Impl.Data.Repository.Repository<,>))
            //       .As(typeof(library.Interface.Data.IRepository<,>))
            //       .InstancePerRequest();

            //builder.RegisterType<library.Impl.Entities.Repository<entities.Model.Empresa, data.Model.Empresa>>()
            //       .As<library.Interface.Data.IRepository<entities.Model.Empresa, data.Model.Empresa>>()
            //       .InstancePerRequest();
            //builder.RegisterType<data.Model.Empresa>()
            //       .As<ITableRepository<entities.Model.Empresa>>()
            //       .As<ITableRepositoryMethods<entities.Model.Empresa, data.Model.Empresa>>()
            //       .InstancePerRequest();

            return builder.Build();
        }
    }
}