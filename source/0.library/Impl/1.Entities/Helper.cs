using System.Linq;

namespace Library.Impl.Entities
{
    public class Helper
    {
        public static B SetProperties<A, B>(A from, B to, bool nulls = false)
        {
            var propsfrom = from?.GetType().GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType.IsValueType || (x.PropertyType == typeof(string)));
            var propsto = to?.GetType().GetProperties();

            foreach (var propfrom in propsfrom)
            {
                var propto = propsto?.Where(x => x.Name == propfrom.Name).SingleOrDefault();

                var value = propfrom.GetValue(from, null);
                if (value != null || nulls)
                {
                    propto?.SetValue(to, value, null);
                }
            }

            return to;
        }
    }
}
