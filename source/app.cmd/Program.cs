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

            Benchmark.Test();

            Console.ReadLine();
        }
    }
}
