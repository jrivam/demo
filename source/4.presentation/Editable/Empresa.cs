namespace presentation.Model
{
    public partial class Empresa
    {
    }
}

namespace presentation.Query
{
    public partial class Empresa
    {
    }
}

namespace presentation.Mapper
{
    public partial class Empresa
    {
        public virtual presentation.Model.Empresa Load(presentation.Model.Empresa entity)
        {
            return entity;
        }
    }
}

