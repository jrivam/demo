using library.Impl;

namespace domain.Model
{
    public partial class Empresa
    {
        //public virtual string Direccion { get; set; }

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
            //entity.Direccion = "mi direccion empresa";

            return entity;
        }
    }
}
