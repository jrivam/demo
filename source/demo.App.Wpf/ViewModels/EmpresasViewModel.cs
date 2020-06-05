using jrivam.Library.Impl.Presentation;

namespace demo.App.Wpf.ViewModels
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
                    OnPropertyChanged(nameof(EmpresasQuery));
                }
            }
        }

        public EmpresasViewModel(Presentation.Table.EmpresasQuery empresasquery)
            : base()
        {
            EmpresasQuery = empresasquery;
            EmpresasQuery.Refresh();
        }

        public EmpresasViewModel()
            : this(new Presentation.Table.EmpresasQuery())
        {
        }
    }
}
