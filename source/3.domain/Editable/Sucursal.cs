using library.Impl;

namespace domain.Model
{
    public partial class Sucursal
    {
        //public virtual string Direccion { get; set; }

        protected domain.Model.Empresas _empresas;
        public virtual domain.Model.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new domain.Query.Empresa();
                    query.Data["Activo"]?.Where(true);

                    Empresas = (domain.Model.Empresas)new domain.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Data.Empresas = (data.Model.Empresas)new data.Model.Empresas().Load(_empresas?.Datas);
                }
            }
        }

        protected override Result SaveChildren()
        {
            return SaveChildren2();
        }
        protected override Result EraseChildren()
        {
            return EraseChildren2();
        }
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
            //entity.Direccion = "mi direccion sucursal";

            return entity;
        }
    }
}

