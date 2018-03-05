using library.Impl.Presentation;
using presentation.Model;

namespace WpfApp.ViewModels
{
    public class EmpresasViewModel : presentation.Model.Empresas
    {
        public EmpresasViewModel()
            : base()
        {
            Load(new presentation.Query.Empresa());
            //Messenger.Default.Register<presentation.Model.Empresa>(this, EmpresaAdd, "EmpresaAdd");
            //Messenger.Default.Register<(presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue)>(this, EmpresaEdit, "EmpresaEdit");
        }

        //public override void EmpresaAdd(presentation.Model.Empresa entity)
        //{
        //    var view = new Views.Empresa();

        //    view.ShowDialog();

        //    base.EmpresaAdd((presentation.Model.Empresa)view.ViewModel);
        //}
        //public override void EmpresaEdit((presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue) message)
        //{
        //    var view = new Views.Empresa();
        //    view.ViewModel.Id = message.oldvalue.Id;

        //    var load = view.ViewModel.Load();
        //    if (load.result.Success)
        //    {
        //        view.ShowDialog();

        //        if (view.ViewModel.Deleted)
        //            Remove(message.oldvalue);
        //        else
        //            base.EmpresaEdit((message.oldvalue, view.ViewModel));
        //    }
        //}
    }
}
