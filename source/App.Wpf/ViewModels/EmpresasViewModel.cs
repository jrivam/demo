using Library.Impl.Presentation;

namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.EmpresasQuery _empresasquery;
        public Presentation.Table.EmpresasQuery EmpresasQuery
        {
            get
            {
                return _empresasquery;
            }
            set
            {
                if (_empresasquery != value)
                {
                    _empresasquery = value;
                    OnPropertyChanged("EmpresasQuery");
                }
            }
        }

        public EmpresasViewModel(Presentation.Table.EmpresasQuery empresasquery = null)
            : base()
        {
            EmpresasQuery = empresasquery ?? new Presentation.Table.EmpresasQuery();
        }
    }
}
