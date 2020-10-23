using Autofac;
using jrivam.Library.Impl.Business;
using jrivam.Library.Impl.Business.Loader;
using jrivam.Library.Impl.Business.Query;
using jrivam.Library.Impl.Business.Table;
using jrivam.Library.Impl.Entities.Reader;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Database;
using jrivam.Library.Impl.Persistence.Mapper;
using jrivam.Library.Impl.Persistence.Query;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Builder;
using jrivam.Library.Impl.Persistence.Table;
using jrivam.Library.Impl.Presentation;
using jrivam.Library.Impl.Presentation.Query;
using jrivam.Library.Impl.Presentation.Raiser;
using jrivam.Library.Impl.Presentation.Table;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections;
using System.Collections.Generic;

namespace jrivam.Library
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EntityReader>()
                   .As<IEntityReader>();
            builder.RegisterType<DataMapper>()
                   .As<IDataMapper>();
            builder.RegisterType<DomainLoader>()
                   .As<IDomainLoader>();
            builder.RegisterType<ModelRaiser>()
                   .As<IModelRaiser>();

            builder.RegisterType<DbCommandExecutorAsync>()
                   .As<IDbCommandExecutorAsync>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DbObjectCreator>()
                   .As<IDbObjectCreator>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SqlCreator>()
                   .As<ISqlCreator>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SqlBuilderQuery>()
                   .As<ISqlBuilderQuery>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SqlBuilderTable>()
                   .As<ISqlBuilderTable>()
                   .InstancePerLifetimeScope();


            //        builder.RegisterType<SqlServerCommandBuilder>()
            //.           As<ISqlCommandBuilder>().AsSelf();
            //        builder.RegisterType<PostgreSqlCommandBuilder>()
            //            .As<ISqlCommandBuilder>().AsSelf();
            //        builder.RegisterType<MySqlCommandBuilder>()
            //            .As<ISqlCommandBuilder>().AsSelf();

            //        builder.RegisterType<SqlServerSyntaxSign>()
            //            .As<ISqlSyntaxSign>().AsSelf();
            //        builder.RegisterType<PostgreSqlSyntaxSign>()
            //            .As<ISqlSyntaxSign>().AsSelf();
            //        builder.RegisterType<MySqlSyntaxSign>()
            //            .As<ISqlSyntaxSign>().AsSelf();

            //builder.RegisterType<Repository>()
            //       .As<IRepository>()
            //       .InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(AbstractTableData<,>))
            //    .As(typeof(AbstractTableData<,>)).InstancePerDependency();

            builder.RegisterGeneric(typeof(RepositoryTableAsync<,>))
                   .As(typeof(IRepositoryTableAsync<,>));
            builder.RegisterGeneric(typeof(RepositoryQueryAsync<,>))
                   .As(typeof(IRepositoryQueryAsync<,>));

            builder.RegisterGeneric(typeof(LogicAsync<,>))
                   .As(typeof(ILogicAsync<,>));
            builder.RegisterGeneric(typeof(LogicTableAsync<,,>))
                   .As(typeof(ILogicTableAsync<,,>));
            builder.RegisterGeneric(typeof(LogicQueryAsync<,,>))
                   .As(typeof(ILogicQueryAsync<,,>));

            builder.RegisterGeneric(typeof(InteractiveAsync<,,>))
                   .As(typeof(IInteractiveAsync<,,>));
            builder.RegisterGeneric(typeof(InteractiveTableAsync<,,,>))
                   .As(typeof(IInteractiveTableAsync<,,,>));
            builder.RegisterGeneric(typeof(InteractiveQueryAsync<,,,>))
                   .As(typeof(IInteractiveQueryAsync<,,,>));

            builder.RegisterGeneric(typeof(List<>))
                   .As(typeof(ICollection<>));

            builder.RegisterGeneric(typeof(ListData<,>))
                   .As(typeof(IListData<,>));
            builder.RegisterGeneric(typeof(ListDataEdit<,>))
                   .As(typeof(IListDataEdit<,>));
            builder.RegisterGeneric(typeof(ListDataReload<,,>))
                   .As(typeof(IListDataReloadAsync<,>));

            builder.RegisterGeneric(typeof(ListDomain<,,>))
                   .As(typeof(IListDomain<,,>));
            builder.RegisterGeneric(typeof(ListDomainEdit<,,>))
                   .As(typeof(IListDomainEditAsync<,,>));
            builder.RegisterGeneric(typeof(ListDomainReload<,,,>))
                   .As(typeof(IListDomainReloadAsync<,,>));

            builder.RegisterGeneric(typeof(ListModel<,,,>))
                   .As(typeof(IListModel<,,,>));
            builder.RegisterGeneric(typeof(ListModelEdit<,,,>))
                   .As(typeof(IListModelEdit<,,,>));
            builder.RegisterGeneric(typeof(ListModelReload<,,,,>))
                   .As(typeof(IListModelReloadAsync<,,,>));

            return builder;
        }
    }
}
