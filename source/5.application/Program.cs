namespace application
{
    class Program
    {
        static void Main(string[] args)
        {
            var empresa = new data.Model.Empresa() { Id = 1};
            //var list = empresa.Sucursales_Load();

            //IContainer container = AutofacConfig.Build();
            //var repository = container.Resolve<Repository<entities.Model.Empresa, data.Model.Empresa>>();

            //var a = new data.Model.Empresa(repository);


            //var c = new Empresa() { Id = 1 }.Load().presentation;
            //c.Sucursales_Load();
            //c.Domain.Data.Id = 2;

            //var c1 = c.Erase().presentation;


            //var b1 = new presentation.Query.Empresa();
            //b1.Domain.Data["RazonSocial"]?.Where("empresa 1");

            ////b1["RazonSocial"].Where(("a", WhereOperator.Like), ("b", WhereOperator.Equals), ("c", WhereOperator.Equals));
            ////b1["RazonSocial"].Where(new string[] { "f", "b", "c" }, WhereOperator.Like);
            //var b2 = b1.Retrieve().presentation;

            //b2.RazonSocial = "test2";
            //b2.Save();

            //var a = new Empresa() { RazonSocial = "test" }.Save().presentation;            



            //var d = new presentation.Query.Empresa();
            //d.Domain.Data["RazonSocial"]?.Where("test", WhereOperator.Like);
            //var d1 = d.List();
        }
    }
}
