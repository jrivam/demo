namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : presentation.Model.Sucursales
    {
        public SucursalesViewModel()
            : base()
        {
            var load = Load(new presentation.Query.Sucursal(), 2);
        }
    }
}
