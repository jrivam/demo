using System;
using System.Linq;

namespace Presentation.Table
{
    public partial class Sucursal
    {
        public string CodigoX
        {
            get
            {
                return Codigo;
            }
            set
            {
                Codigo = value;

                var checkisunique = Domain.Data.CheckIsUnique();
                ValidateCodigo = String.Join("/", checkisunique.result.Messages.Select(x => x.message).ToArray()).Replace(Environment.NewLine, string.Empty);

                OnPropertyChanged("CodigoX");
            }
        }
        public string ValidateCodigo
        {
            get
            {
                return Validations["ValidateCodigo"];
            }
            set
            {
                var contains = Validations.ContainsKey("ValidateCodigo");

                if ((contains && Validations["ValidateCodigo"] != value) || !contains)
                {
                    Validations["ValidateCodigo"] = value;

                    OnPropertyChanged("Validations");
                    OnPropertyChanged("Validation");
                }
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
        public override Table.Sucursal Extra(Table.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.CodigoX = presentation.Codigo;

            return presentation;
        }
    }
}