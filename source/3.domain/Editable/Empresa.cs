namespace domain.Model
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

namespace domain.Query
{
    public partial class Empresa
    {
    }
}

namespace domain.Mapper
{
    public partial class Empresa
    {
        public override domain.Model.Empresa Load(domain.Model.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
