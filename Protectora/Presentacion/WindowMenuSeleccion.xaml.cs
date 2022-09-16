using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Protectora.Dominio;

namespace Protectora.Presentacion
{
    /// <summary>
    /// Lógica de interacción para WindowMenuSeleccion.xaml
    /// </summary>
    public partial class WindowMenuSeleccion : Window
    {

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowMenuSeleccion(Usuario usuario)
        {
            InitializeComponent();
            lblFinalFechaSesion.Content = usuario.FechaUltimaConex.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            App.Current.Shutdown();
        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            Hide();
        }

        private void ButtonAnimales_Click(object sender, RoutedEventArgs e)
        {
            WindowAnimales winAnimales = new WindowAnimales(this);
            winAnimales.Show();
            Hide();
        }

        private void ButtonSocios_Click(object sender, RoutedEventArgs e)
        {
            WindowSocios winSocios = new WindowSocios(this);
            winSocios.Show();
            Hide();
        }

        private void ButtonVoluntarios_Click(object sender, RoutedEventArgs e)
        {
            WindowVoluntarios winVoluntarios = new WindowVoluntarios(this);
            winVoluntarios.Show();
            Hide();
        }
    }
}
