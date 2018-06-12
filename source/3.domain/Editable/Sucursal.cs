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
                return _empresas ?? (Empresas = new domain.Model.Empresas(Data?.Empresas));
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

