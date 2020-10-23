using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using System.Threading.Tasks;

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

        public SucursalViewModel(Presentation.Table.Sucursal sucursal)
            : base()
        {
            Sucursal = sucursal;
        }

        public SucursalViewModel()
            : this(AutofacConfiguration.Container.Resolve<Presentation.Table.Sucursal>())
        {
        }
    }
}
