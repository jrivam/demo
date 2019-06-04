using library.Impl.Presentation;
using Library.Impl.Persistence.Sql;

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

        protected Presentation.Table.Empresas _empresas;
        public Presentation.Table.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new Presentation.Query.Empresa();
                    query.Activo = (true, WhereOperator.Equals);

                    Empresas = (Presentation.Table.Empresas)new Presentation.Table.Empresas().Load(query.List().models);
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
