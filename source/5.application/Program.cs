using library.Impl.Data;
using presentation.Model;

namespace application
{
    class Program
    {
        static void Main(string[] args)
        {
            var b1 = new presentation.Query.Empresa();
            b1.Domains.Data["RazonSocial"].Where("test");

            //b1["RazonSocial"].Where(("a", WhereOperator.Like), ("b", WhereOperator.Equals), ("c", WhereOperator.Equals));
            //b1["RazonSocial"].Where(new string[] { "f", "b", "c" }, WhereOperator.Like);
            var b2 = b1.Retrieve().presentation;

            b2.RazonSocial = "test2";
            b2.Save();

            var a = new Empresa() { RazonSocial = "test" }.Save().presentation;            

            var c = new Empresa() { Id = 1}.Load().presentation;

            var c1 = c.Erase().presentation;

            var d = new presentation.Query.Empresa();
            d.Domains.Data["RazonSocial"].Where("test", WhereOperator.Like);
            var d1 = d.List();
        }
    }
}
