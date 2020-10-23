using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using System.Threading.Tasks;

namespace demo.App.Wpf.ViewModels
{
    public class SucursalesViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.SucursalesReload _sucursales;
        public Presentation.Table.SucursalesReload Sucursales
        {
            get
            {
                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;
                    OnPropertyChanged(nameof(Sucursales));
                }
            }
        }

        protected Presentation.Table.EmpresasReload _empresas;
        public Presentation.Table.EmpresasReload Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    Empresas = AutofacConfiguration.Container.Resolve<Presentation.Table.EmpresasReload>();
                    _empresas.Query.Activo = (true, WhereOperator.Equals);

                    Task.Run(async () => await _empresas.RefreshAsync());
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

        public SucursalesViewModel(Presentation.Table.SucursalesReload sucursales)
            : base()
        {
            Sucursales = sucursales;

            Task.Run(async () => await Sucursales.RefreshAsync());
        }

        public SucursalesViewModel()
            : this(AutofacConfiguration.Container.Resolve<Presentation.Table.SucursalesReload>(new NamedParameter("maxdepth", 2)))
        {
        }
    }
}
