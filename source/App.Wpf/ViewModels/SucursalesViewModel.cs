namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : Presentation.Table.SucursalesQuery
    {
        public SucursalesViewModel()
            : base(new Presentation.Query.Sucursal(), 2)
        {
        }
    }
}
