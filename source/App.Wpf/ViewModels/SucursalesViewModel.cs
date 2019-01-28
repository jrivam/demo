namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : presentation.Model.Sucursales
    {
        public SucursalesViewModel()
            : base(new presentation.Query.Sucursal(), 2)
        {
        }
    }
}
