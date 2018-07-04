using library.Impl.Data.Sql;
using System;
using System.Linq;

namespace app.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new TestDbContext())
            //{
            //    var empresas = context.Empresa.ToList();
            //    foreach (var empresa in empresas)
            //    {
            //        Console.WriteLine($"{empresa.Id} {empresa.RazonSocial}");
            //    }
            //}

            //Benchmark.Test();

            var e = new entities.Model.Empresa();

            var query = new domain.Query.Sucursal();

            query.Id = (value: 5, sign: WhereOperator.Equals);
            query.Nombre = (value: "sucursal 1", sign: WhereOperator.Equals);
            query.Activo = (value: true, sign: WhereOperator.Equals);

            query.Empresa().Id = (value: 3, sign: WhereOperator.Equals);

            //query?["Id"]?.Where(3)?["RazonSocial"]?.Where("empresa", WhereOperator.Like)?["Activo"]?.Where(true);

            //var empresa1 = query.List(2);
            var empresa1 = query.Retrieve();

            Console.ReadLine();
        }
    }
}
