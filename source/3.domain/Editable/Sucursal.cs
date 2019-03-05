namespace domain.Model
{
    public partial class Sucursal
    {
        public virtual string Descripcion
        {
            get
            {
                return $"{Codigo} - {Nombre}";
            }
        }
    }
}

namespace domain.Query
{
    public partial class Sucursal
    {
    }
}

namespace domain.Mapper
{
    public partial class Sucursal
    {
        public override domain.Model.Sucursal Load(domain.Model.Sucursal domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}

