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
    }
}

