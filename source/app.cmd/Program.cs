using library.Impl.Data.Sql;
using System;
using System.Linq;

namespace app.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Benchmark.Test();

            var query = new domain.Query.Sucursal();

            //query.Id = (value: 5, sign: WhereOperator.Equals);

            query.Nombre = (value: "sucursal 1", sign: WhereOperator.Equals);
            query.Nombre = (value: "sucursal 2", sign: WhereOperator.Equals);
            query.Nombre = (value: "sucursal 3", sign: WhereOperator.Equals);
            //query["Nombre"].Where(("sucursal 1", WhereOperator.Equals), ("sucursal 2", WhereOperator.Equals), ("sucursal 3", WhereOperator.Equals));
            //query["Nombre"].Where(new string[] { "sucursal 1", "sucursal 2", "sucursal 3" }, WhereOperator.Equals);

            query.Activo = (true, WhereOperator.Equals);

            //query.Empresa().Id = (value: 3, sign: WhereOperator.Equals);

            var empresa1 = query.List();
            //var empresa1 = query.Retrieve();

            Console.ReadLine();
        }
    }
}
