using Library.Impl;
using Library.Impl.Presentation;
using System.Windows;

namespace WpfApp.Views
{
    public partial class Sucursal : Window
    {
        public Presentation.Table.Sucursal ViewModel
        {
            get
            {
                return (Presentation.Table.Sucursal)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursal()
        {
            InitializeComponent();
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
