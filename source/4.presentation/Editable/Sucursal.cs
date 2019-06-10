namespace Presentation.Table
{
    public partial class Sucursal
    {
        public string CodigoUnique
        {
            get
            {
                return Codigo;
            }
            set
            {
                Codigo = value;

                Validate("Codigo", CheckIsUnique());

                OnPropertyChanged("CodigoUnique");
            }
        }
        public string NombreNotEmpty
        {
            get
            {
                return Nombre;
            }
            set
            {
                Nombre = value;

                Validate("Nombre", CheckIsRequiredColumn("Nombre"));

                OnPropertyChanged("NombreNotEmpty");
            }
        }
    }
}

namespace Presentation.Query
{
    public partial class Sucursal
    {
    }
}

namespace Presentation.Raiser
{
    public partial class Sucursal
    {
        public override Presentation.Table.Sucursal RaiseX(Presentation.Table.Sucursal model, int maxdepth = 1, int depth = 0)
        {
            model.OnPropertyChanged("CodigoUnique");
            model.OnPropertyChanged("NombreNotEmpty");

            return model;
        }
    }
}