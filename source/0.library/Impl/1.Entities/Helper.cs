using System.Linq;

namespace library.Impl.Entities
{
    public class Helper
    {
        public static void SetProperties<T, U>(T from, U to)
        {
            var propsfrom = from.GetType().GetProperties();
            var propsto = to.GetType().GetProperties();

            foreach (var propfrom in propsfrom)
            {
                var propto = propsto.Where(x => x.Name == propfrom.Name).SingleOrDefault();

                if (propto != null)
                {
                    propto.SetValue(to, propfrom.GetValue(from, null), null);
                }
            }
        }
    }
}
