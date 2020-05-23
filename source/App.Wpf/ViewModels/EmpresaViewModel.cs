using Library.Impl.Presentation;

namespace WpfApp.ViewModels
{
    public class EmpresaViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.Empresa _empresa;
        public Presentation.Table.Empresa Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    OnPropertyChanged("Empresa");
                }
            }
        }

        public EmpresaViewModel(Presentation.Table.Empresa empresa = null)
            : base()
        {
            Empresa = empresa ?? new Presentation.Table.Empresa();
        }
    }
}
