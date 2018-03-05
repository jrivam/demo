using library.Impl.Presentation;
using presentation.Model;

namespace WpfApp.ViewModels
{
    public class EmpresaViewModel : presentation.Model.Empresa
    {
        public EmpresaViewModel()
        {
            //Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalAdd, "SucursalAdd");
            //Messenger.Default.Register<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>(this, SucursalEdit, "SucursalEdit");
        }
        //public void SucursalAdd(presentation.Model.Sucursal entity)
        //{
        //    var view = new Views.Sucursal();
        //    view.ViewModel.IdEmpresa = Id;

        //    view.ShowDialog();

        //    if (view.ViewModel.IdEmpresa == Id)
        //        Sucursales.SucursalAdd((presentation.Model.Sucursal)view.ViewModel);
        //}
        //public void SucursalEdit((presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue) message)
        //{
        //    var view = new Views.Sucursal();
        //    view.ViewModel.Id = message.oldvalue.Id;

        //    var load = view.ViewModel.Load();
        //    if (load.result.Success)
        //    {
        //        view.ShowDialog();

        //        if (view.ViewModel.Deleted || message.oldvalue.IdEmpresa != message.newvalue.IdEmpresa)
        //        {
        //            Sucursales.Remove(message.oldvalue);
        //        }
        //        else
        //        {
        //            Sucursales.SucursalEdit((message.oldvalue, view.ViewModel));
        //        }
        //    }
        //}
    }
}
