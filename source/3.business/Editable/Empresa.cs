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

        public virtual string Tipo { get; set; }
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
            domain.Tipo = "0";
    
            return domain;
        }
    }
}
