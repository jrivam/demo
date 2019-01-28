using library.Impl;
using library.Impl.Presentation;
using System.Windows;

namespace WpfApp.Views
{
    public partial class Empresas : Window
    {
        public presentation.Model.Empresas ViewModel
        {
            get
            {
                return (presentation.Model.Empresas)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Empresas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>(this, EmpresaLoad, "EmpresaLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>(this, EmpresaSave, "EmpresaSave");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");

            Messenger.Default.Register<(presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue)>(this, EmpresaEdit, "EmpresaEdit");

            Messenger.Default.Register<presentation.Model.Empresa>(this, EmpresasAdd, "EmpresasAdd");
            Messenger.Default.Register<int>(this, EmpresasRefresh, "EmpresasRefresh");

            EmpresasRefresh();
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "EmpresaLoad");
            Messenger.Default.Unregister(this, "EmpresaSave");
            Messenger.Default.Unregister(this, "EmpresaErase");

            Messenger.Default.Unregister(this, "EmpresaEdit");

            Messenger.Default.Unregister(this, "EmpresasAdd");
            Messenger.Default.Unregister(this, "EmpresasRefresh");
        }

        public virtual void EmpresaLoad((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.CommandLoad(message);
        }
        public virtual void EmpresaSave((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.CommandSave(message);
        }
        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.CommandErase(message);
        }

        public virtual void EmpresaEdit((presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue) message)
        {
            var view = new Views.Empresa();

            view.ViewModel = message.oldvalue;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted)
                ViewModel.Remove(message.oldvalue);
            else
                ViewModel.CommandEdit((message.oldvalue, view.ViewModel));
        }

        public virtual void EmpresasAdd(presentation.Model.Empresa entity)
        {
            var view = new Views.Empresa();

            view.ViewModel.Activo = true;

            view.ShowDialog();

            ViewModel.CommandAdd((presentation.Model.Empresa)view.ViewModel);
        }
        public virtual void EmpresasRefresh(int top = 0)
        {
            var refresh = ViewModel.Refresh(top);

            ViewModel.CommandRefresh(refresh);
        }
    }
}
