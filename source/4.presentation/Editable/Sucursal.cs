﻿namespace Presentation.Table
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

                Validate("ValidateCodigo", Domain.CheckIsUnique());

                OnPropertyChanged("CodigoX");
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
        public override Table.Sucursal RaiseX(Table.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation.OnPropertyChanged("CodigoX");

            return presentation;
        }
    }
}