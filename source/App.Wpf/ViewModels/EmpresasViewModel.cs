namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : presentation.Model.EmpresasQuery
    {
        public EmpresasViewModel()
            : base(new presentation.Query.Empresa())
        {
        }
    }
}
