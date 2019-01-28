namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : presentation.Model.Empresas
    {
        public EmpresasViewModel()
            : base(new presentation.Query.Empresa())
        {
        }
    }
}
