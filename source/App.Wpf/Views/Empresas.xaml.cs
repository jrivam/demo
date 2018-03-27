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

            Messenger.Default.Register<presentation.Model.Empresa>(this, EmpresaAdd, "EmpresaAdd");
            Messenger.Default.Register<(presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue)>(this, EmpresaEdit, "EmpresaEdit");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "EmpresaLoad");
            Messenger.Default.Unregister(this, "EmpresaSave");
            Messenger.Default.Unregister(this, "EmpresaErase");

            Messenger.Default.Unregister(this, "EmpresaAdd");
            Messenger.Default.Unregister(this, "EmpresaEdit");
        }

        public virtual void EmpresaLoad((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.EmpresaLoad(message);
        }
        public virtual void EmpresaSave((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.EmpresaSave(message);
        }
        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            ViewModel.EmpresaErase(message);
        }

        public virtual void EmpresaAdd(presentation.Model.Empresa entity)
        {
            var view = new Views.Empresa();

            view.ViewModel.Activo = true;

            view.ShowDialog();

            ViewModel.EmpresaAdd((presentation.Model.Empresa)view.ViewModel);
        }
        public virtual void EmpresaEdit((presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue) message)
        {
            var view = new Views.Empresa();

            view.ViewModel = message.oldvalue;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted)
                ViewModel.Remove(message.oldvalue);
            else
                ViewModel.EmpresaEdit((message.oldvalue, view.ViewModel));
        }
    }
}
