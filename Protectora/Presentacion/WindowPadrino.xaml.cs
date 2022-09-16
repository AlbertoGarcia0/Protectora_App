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
    /// Lógica de interacción para WindowPadrino.xaml
    /// </summary>
    public partial class WindowPadrino : Window
    {

        ////////////////////////////////////////        Variables        ////////////////////////////////////////

        WindowAnimales windowAnimales;
        Perro perro;
        string textDni = "El dni tiene que tener 9 caracteres\n";
        string textTelefono = "El telefono tiene que tener 9 caracteres\n";
        string textAyudaImporte = "La ayuda tiene que ser superior a 0 €\n";
        string textAyudaLongitud = "La ayuda tiene que tener menos de 10 caracteres\n";

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowPadrino(WindowAnimales windowAnimalesEn, Perro perroEn)
        {
            InitializeComponent();
            windowAnimales = windowAnimalesEn;
            perro = perroEn;
            if (perro.Apadrinado == 0)
            {
                NoHayPadrino();
            }
            else
            {
                HayPadrino();
                Padrino padrino = new Padrino();
                padrino.Id = perro.Apadrinado;
                padrino = padrino.LeerunPadrino();

                txtNombre.Text = padrino.NombreCompleto.ToString();
                txtCorreo.Text = padrino.Correo.ToString();
                txtDni.Text = padrino.Dni.ToString();
                txtTelefono.Text = padrino.Telefono.ToString();
                txtFormaPago.Text = padrino.FormaPago.ToString();
                txtImporteMensual.Text = padrino.ImporteMensual.ToString();
                txtDatosBancarios.Text = padrino.DatosBancarios.ToString();
                txtFechaEntrada.Text = padrino.FechaEntrada.ToString("dd-MM-yyyy");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();

        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////

        private void BtnAnadir_Click(object sender, RoutedEventArgs e)
        {
            Padrino padrino = new Padrino();
            string textAyuda = "";
            int ignoreMe;

            if (int.TryParse(txtImporteMensual.Text, out ignoreMe) && int.TryParse(txtTelefono.Text, out ignoreMe))
            {

                if (txtTelefono.Text.Length == 9 && txtImporteMensual.Text.Length < 10 && txtDni.Text.Length == 9 && Int32.Parse(txtImporteMensual.Text) > 0)
                {
                    try
                    {

                        padrino = RellenarPadrino(padrino);
                        padrino.InsertarPadrino();
                        perro.Apadrinado = padrino.ObtenerId();
                        perro.ModificarPerro();
                        HayPadrino();
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
                    if (txtImporteMensual.Text.Length >= 10)
                        textAyuda = textAyuda + textAyudaLongitud;
                    else if (Int32.Parse(txtImporteMensual.Text) <= 0)
                        textAyuda = textAyuda + textAyudaImporte;

                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }

            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono\nImporte mensual");
            }

        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Padrino padrino = new Padrino();
            string textAyuda = "";
            int ignoreMe;

            if (int.TryParse(txtImporteMensual.Text, out ignoreMe) && int.TryParse(txtTelefono.Text, out ignoreMe))
            {
                if (txtTelefono.Text.Length == 9 && txtImporteMensual.Text.Length < 10 && txtDni.Text.Length == 9 && Int32.Parse(txtImporteMensual.Text) > 0)
                {
                    try
                    {

                        padrino.Id = perro.Apadrinado;
                        padrino = padrino.LeerunPadrino();
                        padrino = RellenarPadrino(padrino);
                        padrino.ModificarPadrino();
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
                    if (txtImporteMensual.Text.Length >= 10)
                        textAyuda = textAyuda + textAyudaLongitud;
                    else if (Int32.Parse(txtImporteMensual.Text) <= 0)
                        textAyuda = textAyuda + textAyudaImporte;

                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono\nImporte mensual");
            }

           

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Padrino padrino = new Padrino();
            padrino.Id = perro.Apadrinado;
            padrino = padrino.LeerunPadrino();
            padrino.EliminarPadrino();
            perro.Apadrinado = 0;
            perro.ModificarPerro();
            NoHayPadrino();
            LimpiarCampos();
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            windowAnimales.Show();
            this.Hide();
        }
        ////////////////////////////////////////        METODOS AUXILIARES        ////////////////////////////////////////

        private Padrino RellenarPadrino(Padrino Socio)
        {

            Socio.NombreCompleto = txtNombre.Text;
            Socio.Correo = txtCorreo.Text;
            Socio.Dni = txtDni.Text;
            Socio.Telefono = Int32.Parse(txtTelefono.Text);
            Socio.FormaPago = txtFormaPago.Text;
            Socio.ImporteMensual = Int32.Parse(txtImporteMensual.Text);
            Socio.FechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
            Socio.DatosBancarios = txtDatosBancarios.Text;

            return Socio;
        }
        private void NoHayPadrino()
        {
            BtnModificar.IsEnabled = false;
            BtnEliminar.IsEnabled = false;
            BtnAnadir.IsEnabled = true;
        }
        private void HayPadrino()
        {
            BtnModificar.IsEnabled = true;
            BtnEliminar.IsEnabled = true;
            BtnAnadir.IsEnabled = false;
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = "";
            txtFormaPago.Text = "";
            txtImporteMensual.Text = "";
            txtFechaEntrada.Text = "";
            txtDatosBancarios.Text = "";
        }

    }
}
