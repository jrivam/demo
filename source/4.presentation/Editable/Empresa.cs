using System;
using System.Linq;

namespace Presentation.Table
{
    public partial class Empresa
    {
        public string RucX
        {
            get
            {
                return Ruc;
            }
            set
            {
                Ruc = value;

                var checkisunique = Domain.Data.CheckIsUnique();
                ValidateRuc = String.Join("/", checkisunique.result.Messages.Select(x => x.message).ToArray()).Replace(Environment.NewLine, string.Empty);

                OnPropertyChanged("RucX");
            }
        }
        public string ValidateRuc
        {
            get
            {
                return Validations["ValidateRuc"];
            }
            set
            {
                var contains = Validations.ContainsKey("ValidateRuc");

                if ((contains && Validations["ValidateRuc"] != value) || !contains)
                {
                    Validations["ValidateRuc"] = value;

                    OnPropertyChanged("Validations");
                    OnPropertyChanged("Validation");
                }
            }
        }
    }
}

namespace Presentation.Query
{
    public partial class Empresa
    {
    }
}

namespace Presentation.Raiser
{
    public partial class Empresa
    {
        public override Table.Empresa Extra(Table.Empresa presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.RucX = presentation.Ruc;

            return presentation;
        }
    }
}

