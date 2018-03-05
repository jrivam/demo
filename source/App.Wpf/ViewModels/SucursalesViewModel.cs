namespace WpfApp.ViewModels
{
    public class SucursalesViewModel : presentation.Model.Sucursales
    {
        public SucursalesViewModel()
            : base()
        {
            Load(new presentation.Query.Sucursal(), 2);
            
            //Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalAdd, "SucursalAdd");
            //Messenger.Default.Register<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>(this, SucursalEdit, "SucursalEdit");
        }

        //public override void SucursalAdd(presentation.Model.Sucursal entity)
        //{
        //    var view = new Views.Sucursal();

        //    view.ShowDialog();

        //    base.SucursalAdd((presentation.Model.Sucursal)view.ViewModel);
        //}
        //public override void SucursalEdit((presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue) message)
        //{
        //    var view = new Views.Sucursal();
        //    view.ViewModel.Id = message.oldvalue.Id;

        //    var load = view.ViewModel.Load();
        //    if (load.result.Success)
        //    {
        //        view.ShowDialog();

        //        if (view.ViewModel.Deleted)
        //            Remove(message.oldvalue);
        //        else
        //            base.SucursalEdit((message.oldvalue, view.ViewModel));
        //    }
        //}
    }
}
