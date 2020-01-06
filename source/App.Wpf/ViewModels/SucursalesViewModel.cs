using Library.Impl.Persistence.Sql;
using Library.Impl.Presentation;

namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.SucursalesQuery _sucursalesquery;
        public Presentation.Table.SucursalesQuery SucursalesQuery
        {
            get
            {
                return _sucursalesquery;
            }
            set
            {
                if (_sucursalesquery != value)
                {
                    _sucursalesquery = value;
                    OnPropertyChanged("SucursalesQuery");
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

                    var list = query.List();
                    if (list.result.Success)
                    {
                        Empresas = (Presentation.Table.Empresas)new Presentation.Table.Empresas().Load(list.models);
                    }
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

        public SucursalesViewModel(Presentation.Table.SucursalesQuery sucursalesquery)
            : base()
        {
            SucursalesQuery = sucursalesquery;
        }

        public SucursalesViewModel()
            : this(new Presentation.Table.SucursalesQuery(new Presentation.Query.Sucursal(), 2))
        {
        }
    }
}
