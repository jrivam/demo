using library.Interface.Data;

namespace business.Model
{
    public partial class Sucursal
    {
        public virtual string Direccion { get; set; }
    }
}

namespace business.Query
{
    public partial class Sucursal
    {
    }
}

namespace business.Mapper
{
    public partial class Sucursal
    {
        public virtual business.Model.Sucursal Load(business.Model.Sucursal entity)
        {
            entity.Direccion = "mi direccion sucursal";

            return entity;
        }
    }
}

