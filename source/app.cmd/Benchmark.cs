using entities.Model;
using library.Impl.Data;
using System;
using System.Diagnostics;
using System.Linq;

namespace app.cmd
{
    public static class Benchmark
    {
        public static void Test()
        {
            var queries = 5000;

            //  EF  Test
            Stopwatch entityStopWatch = new Stopwatch();
            entityStopWatch.Start();

            var context = new TestDbContext();
            var empresas2 = context.Set<Empresa>();
            for (int i = 0; i < queries; i++)
            {
                Empresa empresa2 = empresas2.Where(x => x.Id == 3 && x.RazonSocial == "empresa 1" && x.Activo == true).First();
            }

            entityStopWatch.Stop();
            Console.WriteLine("Entity - {0} Queries took {1}", queries, entityStopWatch.ElapsedMilliseconds);

            //  Query Test
            Stopwatch queryStopwatch = new Stopwatch();
            queryStopwatch.Start();

            for (int i = 0; i < queries; i++)
            {
                var query = new domain.Query.Empresa();

                query.Id = (value: 3, sign: WhereOperator.Equals);
                query.RazonSocial = (value: "empresa 1", sign: WhereOperator.Equals);
                query.Activo = (value: true, sign: WhereOperator.Equals);

                //query?["Id"]?.Where(3)?["RazonSocial"]?.Where("empresa", WhereOperator.Like)?["Activo"]?.Where(true);

                var empresa1 = query.Retrieve();
            }

            queryStopwatch.Stop();
            Console.WriteLine("Queries - {0} Queries took {1}", queries, queryStopwatch.ElapsedMilliseconds);


            //  Model Test
            Stopwatch modelStopwatch = new Stopwatch();
            modelStopwatch.Start();

            for (int i = 0; i < queries; i++)
            {
                var model = new domain.Model.Empresa() { Id = 1, RazonSocial = "empresa 1", Activo = true };

                var empresa1 = model.LoadQuery();
            }

            modelStopwatch.Stop();
            Console.WriteLine("Model - {0} Queries took {1}", queries, modelStopwatch.ElapsedMilliseconds);



            //  Stored Proc Test
            Stopwatch spStopwatch = new Stopwatch();
            spStopwatch.Start();

            for (int i = 0; i < queries; i++)
            {
                var model = new domain.Model.Empresa() { Id = 1, RazonSocial = "empresa 1", Activo = true};

                var empresa1 = model.Load(true);
            }

            spStopwatch.Stop();
            Console.WriteLine("Stored Proc - {0} Queries took {1}", queries, spStopwatch.ElapsedMilliseconds);


        }
    }
}
