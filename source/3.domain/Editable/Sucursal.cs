using library.Interface.Data;

namespace domain.Model
{
    public partial class Sucursal
    {
        public virtual string Direccion { get; set; }
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
        public virtual domain.Model.Sucursal Load(domain.Model.Sucursal entity)
        {
            entity.Direccion = "mi direccion sucursal";

            return entity;
        }
    }
}

