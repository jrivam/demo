using Library.Impl;
using Library.Impl.Presentation;
using System;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class Sucursales : Window
    {
        public SucursalesViewModel ViewModel
        {
            get
            {
                return (SucursalesViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursales()
        {
            InitializeComponent();
            ViewModel.SucursalesQuery.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalLoad, "SucursalLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, "SucursalSave");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<int>(this, SucursalesRefresh, "SucursalesRefresh");

            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesAdd, "SucursalesAdd");
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, "SucursalEdit");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalLoad");
            Messenger.Default.Unregister(this, "SucursalSave");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "SucursalesRefresh");

            Messenger.Default.Unregister(this, "SucursalesAdd");
            Messenger.Default.Unregister(this, "SucursalEdit");
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandLoad(message);
        }
        public virtual void SucursalSave((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandSave(message);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandErase(message);
        }

        public virtual void SucursalesRefresh(int top = 0)
        {
            ViewModel.SucursalesQuery.CommandRefresh(ViewModel.SucursalesQuery.Refresh(top:top));
        }

        public virtual void SucursalesAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();
            view.ViewModel.Sucursal.Fecha = DateTime.Now.Date;
            view.ViewModel.Sucursal.Activo = true;
            view.ShowDialog();

            ViewModel.SucursalesQuery.ItemAdd(view.ViewModel.Sucursal);
        }
        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal(entity);
            view.ShowDialog();

            ViewModel.SucursalesQuery.ItemEdit((entity, view.ViewModel.Sucursal));
        }
    }
}
