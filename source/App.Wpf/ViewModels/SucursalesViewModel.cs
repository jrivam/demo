namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : presentation.Model.SucursalesQuery
    {
        public SucursalesViewModel()
            : base(new presentation.Query.Sucursal(), 2)
        {
        }
    }
}
