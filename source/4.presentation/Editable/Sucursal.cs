namespace presentation.Model
{
    public partial class Sucursal
    {
        protected presentation.Model.Empresas _empresas;
        public virtual presentation.Model.Empresas Empresas
        {
            get
            {
                return _empresas ?? (Empresas = new presentation.Model.Empresas(Domain?.Empresas));
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Domain.Empresas = (domain.Model.Empresas)new domain.Model.Empresas().Load(_empresas?.Domains);
                }
            }
        }
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