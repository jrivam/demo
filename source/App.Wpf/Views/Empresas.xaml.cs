using Library.Impl;
using Library.Impl.Presentation;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class Empresas : Window
    {
        public EmpresasViewModel ViewModel
        {
            get
            {
                return (EmpresasViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Empresas()
        {
            InitializeComponent();
            ViewModel.EmpresasQuery.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaLoad, "EmpresaLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaSave, "EmpresaSave");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");

            Messenger.Default.Register<int>(this, EmpresasRefresh, "EmpresasRefresh");

            Messenger.Default.Register<Presentation.Table.Empresa>(this, EmpresasAdd, "EmpresasAdd");
            Messenger.Default.Register<Presentation.Table.Empresa>(this, EmpresaEdit, "EmpresaEdit");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "EmpresaLoad");
            Messenger.Default.Unregister(this, "EmpresaSave");
            Messenger.Default.Unregister(this, "EmpresaErase");

            Messenger.Default.Unregister(this, "EmpresasRefresh");

            Messenger.Default.Unregister(this, "EmpresasAdd");
            Messenger.Default.Unregister(this, "EmpresaEdit");
        }

        public virtual void EmpresaLoad((CommandAction action, (Result result, Presentation.Table.Empresa entity) operation) message)
        {
            ViewModel.EmpresasQuery.CommandLoad(message);
        }
        public virtual void EmpresaSave((CommandAction action, (Result result, Presentation.Table.Empresa entity) operation) message)
        {
            ViewModel.EmpresasQuery.CommandSave(message);
        }
        public virtual void EmpresaErase((CommandAction action, (Result result, Presentation.Table.Empresa entity) operation) message)
        {
            ViewModel.EmpresasQuery.CommandErase(message);
        }

        public virtual void EmpresasRefresh(int top = 0)
        {
            ViewModel.EmpresasQuery.CommandRefresh(ViewModel.EmpresasQuery.Refresh(top:top));
        }

        public virtual void EmpresasAdd(Presentation.Table.Empresa entity)
        {
            var view = new Views.Empresa();
            view.ViewModel.Empresa.Activo = true;
            view.ShowDialog();

            ViewModel.EmpresasQuery.ItemAdd(view.ViewModel.Empresa);
        }
        public virtual void EmpresaEdit(Presentation.Table.Empresa entity)
        {
            var view = new Views.Empresa(entity);
            view.ShowDialog();

            ViewModel.EmpresasQuery.ItemEdit(entity, view.ViewModel.Empresa);
        }
    }
}
