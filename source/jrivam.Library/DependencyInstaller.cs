﻿using Autofac;
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

namespace jrivam.Library
{
    public class DependencyInstaller
    {
        public static ContainerBuilder RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EntityReader>()
                   .As<IEntityReader>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DataMapper>()
                   .As<IDataMapper>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DomainLoader>()
                   .As<IDomainLoader>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ModelRaiser>()
                   .As<IModelRaiser>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DbCommandExecutor>()
                   .As<IDbCommandExecutor>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DbCommandExecutorBulk>()
                   .As<IDbCommandExecutorBulk>()
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

            builder.RegisterGeneric(typeof(RepositoryTable<,>))
                   .As(typeof(IRepositoryTable<,>))
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RepositoryQuery<,>))
                   .As(typeof(IRepositoryQuery<,>))
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Logic<,>))
                   .As(typeof(ILogic<,>))
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LogicTable<,,>))
                   .As(typeof(ILogicTable<,,>))
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LogicQuery<,,>))
                   .As(typeof(ILogicQuery<,,>))
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Interactive<,,>))
                   .As(typeof(IInteractive<,,>))
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(InteractiveTable<,,,>))
                   .As(typeof(IInteractiveTable<,,,>))
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(InteractiveQuery<,,,>))
                   .As(typeof(IInteractiveQuery<,,,>))
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ListData<,>))
                   .As(typeof(IListData<,>))
                   .InstancePerDependency();
            builder.RegisterGeneric(typeof(ListDataQuery<,,>))
                   .As(typeof(IListDataQuery<,,>))
                   .InstancePerDependency();

            builder.RegisterGeneric(typeof(ListDomain<,,>))
                   .As(typeof(IListDomain<,,>))
                   .InstancePerDependency();
            builder.RegisterGeneric(typeof(ListDomainQuery<,,,>))
                   .As(typeof(IListDomainQuery<,,,>))
                   .InstancePerDependency();

            builder.RegisterGeneric(typeof(ListModel<,,,>))
                   .As(typeof(IListModel<,,,>))
                   .InstancePerDependency();
            builder.RegisterGeneric(typeof(ListModelQuery<,,,,>))
                   .As(typeof(IListModelQuery<,,,,>))
                   .InstancePerDependency();

            return builder;
        }
    }
}
