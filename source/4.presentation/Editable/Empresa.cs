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

                Validate("ValidateRuc", Domain.CheckIsUnique());

                OnPropertyChanged("RucX");
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
        public override Table.Empresa RaiseX(Table.Empresa presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.OnPropertyChanged("RucX");

            return presentation;
        }
    }
}

