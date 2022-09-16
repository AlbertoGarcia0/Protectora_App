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
    /// Lógica de interacción para WindowContrasenaOlvidada.xaml
    /// </summary>
    public partial class WindowContrasenaOlvidada : Window
    {

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowContrasenaOlvidada()
        {
            InitializeComponent();
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF000000");
            lblContrasena.Foreground = brush;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();

        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = txtUsuario.Text;
                string password = usuario.LeerContraseniaUsuario();
                var converter = new System.Windows.Media.BrushConverter();
                if (password != null && password != "")
                {
                    lblContrasena.Content = password;

                    var brush = (Brush)converter.ConvertFromString("#FF000000");
                    lblContrasena.Foreground = brush;
                }
                else
                {
                    lblContrasena.Content = "El usuario no existe";
                    var brush = (Brush)converter.ConvertFromString("#FFFF0000");
                    lblContrasena.Foreground = brush;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al introducir información: " + ex.Message + " .");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            Hide();
        }
    }
}
