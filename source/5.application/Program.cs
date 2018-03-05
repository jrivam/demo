using library.Impl.Business;
using library.Interface.Data;
using presentation.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using library.Impl.Data;
using library.Impl.Data.Repository;
using library.Interface.Data.Repository;

namespace application
{
    class Program
    {
        static void Main(string[] args)
        {
            var b1 = new presentation.Query.Empresa();
            b1.Business.Data["RazonSocial"].Where("test");

            //b1["RazonSocial"].Where(("a", WhereOperator.Like), ("b", WhereOperator.Equals), ("c", WhereOperator.Equals));
            //b1["RazonSocial"].Where(new string[] { "f", "b", "c" }, WhereOperator.Like);
            var b2 = b1.Retrieve().presentation;

            b2.RazonSocial = "test2";
            b2.Save();

            var a = new Empresa() { RazonSocial = "test" }.Save().presentation;            

            var c = new Empresa() { Id = 1}.Load().presentation;

            var c1 = c.Erase().presentation;

            var d = new presentation.Query.Empresa();
            d.Business.Data["RazonSocial"].Where("test", WhereOperator.Like);
            var d1 = d.List();
        }
    }
}
