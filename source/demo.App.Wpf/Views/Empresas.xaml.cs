using demo.App.Wpf.ViewModels;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Presentation;
using System.Windows;

namespace demo.App.Wpf.Views
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
            DataContext = new EmpresasViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaLoad, nameof(EmpresaLoad));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaSave, nameof(EmpresaSave));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaErase, nameof(EmpresaErase));
            Messenger.Default.Register<Presentation.Table.Empresa>(this, EmpresaEdit, nameof(EmpresaEdit));

            Messenger.Default.Register<(Result result, Presentation.Table.Empresas list)>(this, EmpresasQueryRefresh, nameof(EmpresasQueryRefresh));
            Messenger.Default.Register<Presentation.Table.Empresa>(this, EmpresasQueryAdd, nameof(EmpresasQueryAdd));
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, nameof(EmpresaLoad));
            Messenger.Default.Unregister(this, nameof(EmpresaSave));
            Messenger.Default.Unregister(this, nameof(EmpresaErase));
            Messenger.Default.Unregister(this, nameof(EmpresaEdit));

            Messenger.Default.Unregister(this, nameof(EmpresasQueryRefresh));
            Messenger.Default.Unregister(this, nameof(EmpresasQueryAdd));
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

        public virtual void EmpresasQueryRefresh((Result result, Presentation.Table.Empresas list) message)
        {
            ViewModel.EmpresasQuery.CommandRefresh(message);
        }

        public virtual void EmpresasQueryAdd(Presentation.Table.Empresa entity)
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
