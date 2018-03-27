using library.Interface.Data;

namespace domain.Model
{
    public partial class Empresa
    {
        public virtual string Direccion { get; set; }  
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
        public virtual domain.Model.Empresa Load(domain.Model.Empresa entity)
        {
            entity.Direccion = "mi direccion empresa";

            return entity;
        }
    }
}
