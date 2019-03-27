namespace Business.Table
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

namespace Business.Query
{
    public partial class Sucursal
    {
    }
}

namespace Business.Mapper
{
    public partial class Sucursal
    {
        public override Business.Table.Sucursal Load(Business.Table.Sucursal domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}

