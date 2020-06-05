using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;

namespace demo.App.Wpf.ViewModels
{
    public class SucursalViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.Sucursal _sucursal;
        public Presentation.Table.Sucursal Sucursal
        {
            get
            {
                return _sucursal;
            }
            set
            {
                if (_sucursal != value)
                {
                    _sucursal = value;
                    OnPropertyChanged(nameof(Sucursal));
                }
            }
        }

        protected Presentation.Table.EmpresasQuery _empresas;
        public Presentation.Table.EmpresasQuery Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    Empresas = new Presentation.Table.EmpresasQuery();
                    _empresas.Query.Activo = (true, WhereOperator.Equals);

                    _empresas.Refresh();
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
                    OnPropertyChanged(nameof(Empresas));
                }
            }
        }

        public SucursalViewModel(Presentation.Table.Sucursal sucursal)
            : base()
        {
            Sucursal = sucursal;
        }

        public SucursalViewModel()
            : this(new Presentation.Table.Sucursal())
        {
        }
    }
}
