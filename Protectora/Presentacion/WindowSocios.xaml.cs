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
    /// Lógica de interacción para WindowSocios.xaml
    /// </summary>
    public partial class WindowSocios : Window
    {
        ////////////////////////////////////////        Variables        ////////////////////////////////////////

        List<Socio> arraySocios;
        WindowMenuSeleccion windowMenuSelec;
        string textDni = "El dni tiene que tener 9 caracteres\n";
        string textTelefono = "El telefono tiene que tener 9 caracteres\n";
        string textAyudaImporte = "La ayuda tiene que ser superior a 0 €\n";
        string textAyudaLongitud = "La ayuda tiene que tener menos de 10 caracteres\n";

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowSocios(WindowMenuSeleccion windowMenuSeleccion)
        {
            InitializeComponent();
            windowMenuSelec = windowMenuSeleccion;
            ActualizarListBox();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        ////////////////////////////////////////        EVENTO DE LISTBOX        ////////////////////////////////////////
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxSocios.SelectedIndex != -1)
            {
                BtnModificar.IsEnabled = true;
                BtnEliminar.IsEnabled = true;
                Socio socio = (Socio)ListBoxSocios.Items[ListBoxSocios.SelectedIndex];
                txtNombre.Text = socio.NombreCompleto.ToString();
                txtCorreo.Text = socio.Correo.ToString();
                txtDni.Text = socio.Dni.ToString();
                txtTelefono.Text = socio.Telefono.ToString();
                txtFormaPago.Text = socio.FormaPago.ToString();
                txtAyuda.Text = socio.CuantiaAyuda.ToString();
                txtDatosBancarios.Text = socio.DatosBancarios.ToString();
            }
        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            windowMenuSelec.Show();
            this.Hide();
        }

        private void BtnAnadir_Click(object sender, RoutedEventArgs e)
        {
            Socio socio = new Socio();

            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtAyuda.Text, out ignoreMe) && int.TryParse(txtTelefono.Text, out ignoreMe))
            {
                if (txtTelefono.Text.Length == 9 && txtAyuda.Text.Length < 10 && txtDni.Text.Length == 9 && Int32.Parse(txtAyuda.Text) > 0)
                {
                    try
                    {
                        socio = RellenarSocio(socio);
                        socio.InsertarSocio();
                        ActualizarListBox();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al introducir información: " + ex.Message + "\nCompruebe que no existen" +
                            " datos vacios, y que en el telefono y la cuantia de ayuda solo se encuentren numeros enteros.");
                    }
                }
                else
                {
                    if (txtTelefono.Text.Length != 9)
                        textAyuda = textAyuda + textTelefono;
                    if (txtDni.Text.Length != 9)
                        textAyuda = textAyuda + textDni;
                    if (txtAyuda.Text.Length >= 10)
                        textAyuda = textAyuda + textAyudaLongitud;
                    else if (Int32.Parse(txtAyuda.Text) <= 0)
                        textAyuda = textAyuda + textAyudaImporte;

                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }

            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono\nAyuda");
            }

        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Socio socio = (Socio)ListBoxSocios.Items[ListBoxSocios.SelectedIndex];
            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtAyuda.Text, out ignoreMe) && int.TryParse(txtTelefono.Text, out ignoreMe))
            {
                if (txtTelefono.Text.Length == 9 && txtAyuda.Text.Length < 10 && txtDni.Text.Length == 9 && Int32.Parse(txtAyuda.Text) > 0)
                {
                    try
                    {
                        socio = RellenarSocio(socio);
                        socio.ModificarSocio();
                        ActualizarListBox();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al introducir información: " + ex.Message + "\nCompruebe que no existen" +
                            " datos vacios, y que en el telefono y la cuantia de ayuda solo se encuentren numeros enteros.");
                    }

                }
                else
                {
                    if (txtTelefono.Text.Length != 9)
                        textAyuda = textAyuda + textTelefono;
                    if (txtDni.Text.Length != 9)
                        textAyuda = textAyuda + textDni;
                    if (txtAyuda.Text.Length >= 10)
                        textAyuda = textAyuda + textAyudaLongitud;
                    else if (Int32.Parse(txtAyuda.Text) <= 0)
                        textAyuda = textAyuda + textAyudaImporte;

                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono\nAyuda");
            }

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Socio socio = (Socio)ListBoxSocios.Items[ListBoxSocios.SelectedIndex];
            socio.EliminarSocio();
            ActualizarListBox();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        ////////////////////////////////////////        METODOS AUXILIARES        ////////////////////////////////////////

        private Socio RellenarSocio(Socio socio)
        {

            socio.NombreCompleto = txtNombre.Text;
            socio.Correo = txtCorreo.Text;
            socio.Dni = txtDni.Text;
            socio.Telefono = Int32.Parse(txtTelefono.Text);
            socio.FormaPago = txtFormaPago.Text;
            socio.CuantiaAyuda = Int32.Parse(txtAyuda.Text);
            socio.DatosBancarios = txtDatosBancarios.Text;

            return socio;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = "";
            txtFormaPago.Text = "";
            txtAyuda.Text = "";
            txtDatosBancarios.Text = "";
            BtnModificar.IsEnabled = false;
            BtnEliminar.IsEnabled = false;
        }

        private void ActualizarListBox()
        {
            ListBoxSocios.SelectedIndex=-1;
            ListBoxSocios.Items.Clear();
            LimpiarCampos();
            Socio socAUX = new Socio();
            arraySocios = socAUX.LeerTodosSocios();
            foreach (Socio socio in arraySocios)
            {
                ListBoxSocios.Items.Add(socio);
            }
        }

    }
}
