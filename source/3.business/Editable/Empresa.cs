namespace Business.Table
{
    public partial class Empresa
    {
        public virtual string Descripcion
        {
            get
            {
                return $"{Ruc} - {RazonSocial}";
            }
        }
    }
}

namespace Business.Query
{
    public partial class Empresa
    {
    }
}

namespace Business.Mapper
{
    public partial class Empresa
    {
        public override Business.Table.Empresa LoadX(Business.Table.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            return base.LoadX(domain, maxdepth, depth);
        }
    }
}
