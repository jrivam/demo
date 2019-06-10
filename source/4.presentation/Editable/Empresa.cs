namespace Presentation.Table
{
    public partial class Empresa
    {
        public string RucUnique
        {
            get
            {
                return Ruc;
            }
            set
            {
                Ruc = value;

                Validate("Ruc", CheckIsUnique());

                OnPropertyChanged("RucUnique");
            }
        }
        public string RazonSocialNotEmpty
        {
            get
            {
                return RazonSocial;
            }
            set
            {
                RazonSocial = value;

                Validate("RazonSocial", CheckIsRequiredColumn("RazonSocial"));

                OnPropertyChanged("RazonSocialNotEmpty");
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
        public override Presentation.Table.Empresa RaiseX(Presentation.Table.Empresa model, int maxdepth = 1, int depth = 0)
        {
            model.OnPropertyChanged("RucUnique");
            model.OnPropertyChanged("RazonSocialNotEmpty");

            return model;
        }
    }
}

