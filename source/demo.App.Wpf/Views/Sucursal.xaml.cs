using demo.App.Wpf.ViewModels;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Presentation;
using System.Windows;

namespace demo.App.Wpf.Views
{
    public partial class Sucursal : Window
    {
        public SucursalViewModel ViewModel
        {
            get
            {
                return (SucursalViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursal(Presentation.Table.Sucursal entity = null)
        {
            InitializeComponent();
            DataContext = new SucursalViewModel(entity ?? new Presentation.Table.Sucursal());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalErase");
        }

        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            this.Close();
        }
    }
}
