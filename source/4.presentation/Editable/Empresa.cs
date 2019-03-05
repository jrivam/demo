using System;
using System.Linq;

namespace presentation.Model
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

namespace presentation.Query
{
    public partial class Empresa
    {
    }
}

namespace presentation.Raiser
{
    public partial class Empresa
    {
        public override Model.Empresa Extra(Model.Empresa presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.RucX = presentation.Ruc;

            return presentation;
        }
    }
}

