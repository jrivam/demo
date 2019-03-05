using System;
using System.Linq;

namespace presentation.Model
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

namespace presentation.Query
{
    public partial class Sucursal
    {
    }
}

namespace presentation.Raiser
{
    public partial class Sucursal
    {
        public override Model.Sucursal Extra(Model.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.CodigoX = presentation.Codigo;

            return presentation;
        }
    }
}