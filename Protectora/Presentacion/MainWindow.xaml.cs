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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Protectora.Dominio;
using Protectora.Presentacion;

namespace Protectora
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public MainWindow()
        {
            InitializeComponent();

        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            lblEstado.Content = "";
            Usuario usuario = new Usuario(null, txtUsuario.Text.ToString(), passContrasena.Password.ToString(), null);
            usuario = usuario.LeerUsuario();

            if (usuario.FechaUltimaConex != null)
            {
                WindowMenuSeleccion winMenSel = new WindowMenuSeleccion(usuario);
                winMenSel.Show();
                usuario.ModificarUsuarioFecha();

                Hide();
            }
            else
            {
                lblEstado.Content = "Usuario y/o contraseña incorrecto";
            }

            LimpiarCampos();
        }


        private void BtnRecordarContrasena_Click(object sender, RoutedEventArgs e)
        {
            WindowContrasenaOlvidada winCon = new WindowContrasenaOlvidada();
            winCon.Show();

            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowInfo winInfo = new WindowInfo();
            winInfo.Show();

            Hide();
        }

        ////////////////////////////////////////        METODOS AUXILIARES        ////////////////////////////////////////

        private void LimpiarCampos()
        {
            txtUsuario.Text = "";
            passContrasena.Password = "";
        }
    }
}
