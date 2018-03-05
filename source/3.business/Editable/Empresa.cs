using library.Interface.Data;

namespace business.Model
{
    public partial class Empresa
    {
        public virtual string Direccion { get; set; }  
    }
}

namespace business.Query
{
    public partial class Empresa
    {
    }
}

namespace business.Mapper
{
    public partial class Empresa
    {
        public virtual business.Model.Empresa Load(business.Model.Empresa entity)
        {
            entity.Direccion = "mi direccion empresa";

            return entity;
        }
    }
}
