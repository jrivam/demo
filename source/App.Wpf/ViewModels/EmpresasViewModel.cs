namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : Presentation.Table.EmpresasQuery
    {
        public EmpresasViewModel()
            : base(new Presentation.Query.Empresa())
        {
        }
    }
}
