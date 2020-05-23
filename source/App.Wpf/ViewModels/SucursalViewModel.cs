using Library.Impl.Persistence.Sql;
using Library.Impl.Presentation;

namespace WpfApp.ViewModels
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
                    OnPropertyChanged("Sucursal");
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
                    OnPropertyChanged("Empresas");
                }
            }
        }

        public SucursalViewModel(Presentation.Table.Sucursal sucursal = null)
            : base()
        {
            Sucursal = sucursal ?? new Presentation.Table.Sucursal();
        }
    }
}
