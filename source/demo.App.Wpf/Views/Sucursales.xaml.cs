using demo.App.Wpf.ViewModels;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Presentation;
using System;
using System.Windows;

namespace demo.App.Wpf.Views
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
            DataContext = new SucursalesViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalLoad, nameof(SucursalLoad));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, nameof(SucursalSave));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, nameof(SucursalErase));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, nameof(SucursalEdit));

            Messenger.Default.Register<(Result result, Presentation.Table.SucursalesReload list)>(this, SucursalesReloadRefresh, nameof(SucursalesReloadRefresh));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesReloadAdd, nameof(SucursalesReloadAdd));
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, nameof(SucursalLoad));
            Messenger.Default.Unregister(this, nameof(SucursalSave));
            Messenger.Default.Unregister(this, nameof(SucursalErase));
            Messenger.Default.Unregister(this, nameof(SucursalEdit));

            Messenger.Default.Unregister(this, nameof(SucursalesReloadRefresh));
            Messenger.Default.Unregister(this, nameof(SucursalesReloadAdd));
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Sucursales.CommandLoad(message);
        }
        public virtual void SucursalSave((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Sucursales.CommandSave(message);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Sucursales.CommandErase(message);
        }

        public virtual void SucursalesReloadRefresh((Result result, Presentation.Table.SucursalesReload list) message)
        {
            ViewModel.Sucursales.CommandRefresh(message);
        }

        public virtual void SucursalesReloadAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();
            view.ViewModel.Sucursal.Fecha = DateTime.Now.Date;
            view.ViewModel.Sucursal.Activo = true;
            view.ShowDialog();

            ViewModel.Sucursales.ItemAdd(view.ViewModel.Sucursal);
        }
        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal(entity);
            view.ShowDialog();

            ViewModel.Sucursales.ItemModify(entity, view.ViewModel.Sucursal);
        }
    }
}
