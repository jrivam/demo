namespace Business.Table
{
    public partial class Empresa
    {
        public virtual string Descripcion
        {
            get
            {
                return $"{Ruc} - {RazonSocial}";
            }
        }
    }
}

namespace Business.Query
{
    public partial class Empresa
    {
    }
}