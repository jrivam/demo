using System;

namespace Presentation.Table
{
    public partial class Sucursal
    {
        public string CodigoNotEmpty
        {
            get
            {
                return Codigo;
            }
            set
            {
                Codigo = value;

                Domain.Validate("CodigoNotEmpty").FilteredAsText(Environment.NewLine);

                OnPropertyChanged("CodigoNotEmpty");
            }
        }
        public string CodigoUnique
        {
            get
            {
                return Codigo;
            }
            set
            {
                Codigo = value;

                Domain.Validate("CodigoUnique");

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

                Domain.Validate("NombreNotEmpty");

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
    }
}