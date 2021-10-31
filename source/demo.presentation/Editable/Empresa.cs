namespace demo.Presentation.Table
{
    public partial class Empresa
    {
        public string RucNotEmpty
        {
            get
            {
                return Ruc;
            }
            set
            {
                Ruc = value;

                Domain.Validate("RucNotEmpty");

                OnPropertyChanged("RucNotEmpty");
            }
        }
        public string RucUnique
        {
            get
            {
                return Ruc;
            }
            set
            {
                Ruc = value;

                Domain.Validate("RucUnique");

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

                Domain.Validate("RazonSocialNotEmpty");

                OnPropertyChanged("RazonSocialNotEmpty");
            }
        }
    }
}

namespace demo.Presentation.Query
{
    public partial class Empresa
    {
    }
}

