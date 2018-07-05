using System.Linq;

namespace library.Impl.Entities
{
    public class Helper
    {
        public static U SetProperties<T, U>(T from, U to)
        {
            var propsfrom = from?.GetType().GetProperties();
            var propsto = to?.GetType().GetProperties();

            foreach (var propfrom in propsfrom)
            {
                var propto = propsto?.Where(x => x.Name == propfrom.Name).SingleOrDefault();

                var value = propfrom.GetValue(from, null);
                if (value != null)
                {
                    propto?.SetValue(to, value, null);
                }
            }

            return to;
        }
    }
}
