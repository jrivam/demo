namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : presentation.Model.Empresas
    {
        public EmpresasViewModel()
            : base()
        {
            Load(new presentation.Query.Empresa());
        }
    }
}
