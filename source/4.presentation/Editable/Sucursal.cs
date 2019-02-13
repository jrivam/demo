namespace presentation.Model
{
    public partial class Sucursal
    {
    }
}

namespace presentation.Query
{
    public partial class Sucursal
    {
    }
}

namespace presentation.Mapper
{
    public partial class Sucursal
    {
        public virtual presentation.Model.Sucursal Load(presentation.Model.Sucursal entity)
        {
            return entity;
        }
    }
}