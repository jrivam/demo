using Autofac;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

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

        //public static ConstructorInfo constructor = typeof(U).GetConstructors().Where(c => c.GetParameters().Length == 0 || c.GetParameters().All(p => p.IsOptional)).SingleOrDefault();
        //public static ConstructorInfo constructor = typeof(U).GetConstructors().OrderBy(x => x.GetParameters().Length).ThenByDescending(y => y.GetParameters().Count(p => p.IsOptional)).FirstOrDefault();
        //public static ConstructorInfo constructor = typeof(U).GetConstructors().OrderBy(x => x.GetParameters().Length - x.GetParameters().Count(p => p.IsOptional)).ThenBy(x => x.GetParameters().Length).FirstOrDefault();

        public static U CreateData(T entity)
        {
            //var c = typeof(U).GetConstructors();
            //var d = c.Where(x => x.GetParameters().Length >= 3);
            //var e = d.Where(x => x.GetParameters().Any(y => y.GetType() == typeof(T)));

            //return (U)constructor.Invoke(new object[] { Type.Missing, Type.Missing, entity, Type.Missing, Type.Missing });

            return AutofacConfiguration.Container.Resolve<U>(new TypedParameter(typeof(T), entity));
        }
        public static IEnumerable<U> CreateDataList(IEnumerable<T> entities)
        {
            return entities.Select(x => CreateData(x));
        }
    }
}
