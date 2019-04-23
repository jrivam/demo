using Library.Impl;
using Library.Impl.Presentation;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Empresas_Click(object sender, RoutedEventArgs e)
        {
            var view = new Views.Empresas();
            view.Show();
        }

        private void Sucursales_Click(object sender, RoutedEventArgs e)
        {
            var view = new Views.Sucursales();
            view.Show();
        }
    }
}
