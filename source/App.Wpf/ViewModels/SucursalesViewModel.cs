namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : presentation.Model.Sucursales
    {
        public SucursalesViewModel()
            : base()
        {
            Load(new presentation.Query.Sucursal(), 1);
        }
    }
}
