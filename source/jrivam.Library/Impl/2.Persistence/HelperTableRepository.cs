using Autofac;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace jrivam.Library.Impl.Persistence
{
    public class HelperTableRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        //public static U CreateData(T entity)
        //{
        //    var constructor = typeof(U).GetConstructors()[0];

        //    //var constructor = typeof(U).GetConstructor(new Type[] { typeof(string), typeof(IQueryData<T, U>), typeof(T) });
        //    var parameter1 = Expression.Parameter(typeof(string), "connectionstringsettingsname");

        //    //var enumerableGenericType = typeof(IQueryData<,>).MakeGenericType(new Type[] { typeof(T), typeof(U) });
        //    var parameter2 = Expression.Parameter(typeof(IQueryData<T, U>), "query");
        //    var parameter3 = Expression.Parameter(typeof(T), "entity");
        //    var creatorExpression = Expression.Lambda<Func<T, U>>(
        //        Expression.New(constructor, new Expression[] { parameter1, parameter2, parameter3 }), parameter1, parameter2, parameter3).Compile();

        //    return creatorExpression.Invoke(entity);
        //}

        public static ConstructorInfo constructor = typeof(U).GetConstructors().Where(c => c.GetParameters().Length == 0 || c.GetParameters().All(p => p.IsOptional)).SingleOrDefault();

        public static U CreateData(T entity)
        {
            return (U)constructor.Invoke(new object[] { Type.Missing, Type.Missing, entity, Type.Missing, Type.Missing });

            //return (U)Activator.CreateInstance(typeof(U),
            //        BindingFlags.Public | BindingFlags.Instance |
            //        BindingFlags.OptionalParamBinding,
            //        null,
            //        new object[] { Type.Missing, Type.Missing, entity },
            //        CultureInfo.CurrentCulture
            //        );
        }
        public static IEnumerable<U> CreateDataList(IEnumerable<T> entities)
        {
            return entities.Select(x => CreateData(x));
        }

        public static IRepositoryTable<T, U> GetRepositoryTable(ConnectionStringSettings connectionstringsettings)
        {
            return AutofacConfiguration.Container.Resolve<IRepositoryTable<T, U>>(
                        new TypedParameter(typeof(IRepository), AutofacConfiguration.Container.Resolve<IRepository>(
                                new TypedParameter(typeof(ConnectionStringSettings), connectionstringsettings))),
                        new TypedParameter(typeof(ISqlBuilderTable), AutofacConfiguration.Container.Resolve<ISqlBuilderTable>(new TypedParameter(typeof(ISqlSyntaxSign), SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)))),
                        new TypedParameter(typeof(ISqlCommandBuilder), SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName)
                        )
                    );
        }
    }
}
