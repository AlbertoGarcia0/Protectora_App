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
using Microsoft.Win32;
using System.IO;

namespace Protectora.Presentacion
{
    /// <summary>
    /// Lógica de interacción para WindowVoluntarios.xaml
    /// </summary>
    public partial class WindowVoluntarios : Window
    {

        ////////////////////////////////////////        Variables        ////////////////////////////////////////

        List<Voluntario> arrayVoluntarios;
        WindowMenuSeleccion windowMenuSelec;
        string textDni = "El dni tiene que tener 9 caracteres\n";
        string textTelefono = "El telefono tiene que tener 9 caracteres\n";

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowVoluntarios(WindowMenuSeleccion windowMenuSeleccion)
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
            if (ListBoxVoluntarios.SelectedIndex != -1)
            {
                BtnModificar.IsEnabled = true;
                BtnEliminar.IsEnabled = true;
                BtnImagenVoluntarios.IsEnabled = true;
                Voluntario voluntario = (Voluntario)ListBoxVoluntarios.Items[ListBoxVoluntarios.SelectedIndex];
                fotoVoluntario.Visibility = System.Windows.Visibility.Visible;
                txtNombre.Text = voluntario.NombreCompleto.ToString();
                txtCorreo.Text = voluntario.Correo.ToString();
                txtDni.Text = voluntario.Dni.ToString();
                txtTelefono.Text = voluntario.Telefono.ToString();
                txtHorario.Text = voluntario.Horario.ToString();
                txtZonaDisponibilidad.Text = voluntario.ZonaDisponibilidad.ToString();
                //Foto
                try
                {
                    String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    path = path.Replace("\\bin\\Debug\\", "");
                    path = path + "/Presentacion/" + voluntario.Foto;

                    fotoVoluntario.Source = new ImageSourceConverter().ConvertFromString(path) as ImageSource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
            }

        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////
        
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            windowMenuSelec.Show();
            this.Hide();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Voluntario voluntario = (Voluntario)ListBoxVoluntarios.Items[ListBoxVoluntarios.SelectedIndex];
            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtTelefono.Text, out ignoreMe))
            {
                if (txtTelefono.Text.Length == 9 && txtDni.Text.Length == 9)
                {
                    try
                    {
                        voluntario = RellenarVoluntario(voluntario);
                        voluntario.ModificarVoluntario();
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


                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono");
            }

        }

        private void BtnAnadir_Click(object sender, RoutedEventArgs e)
        {

            Voluntario voluntario = new Voluntario();
            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtTelefono.Text, out ignoreMe))
            {
                if (txtTelefono.Text.Length == 9 && txtDni.Text.Length == 9)
                {
                    try
                    {
                        voluntario = RellenarVoluntario(voluntario);
                        voluntario.InsertarVoluntario();
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


                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nTelefono");
            }
           
           
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Voluntario voluntario = (Voluntario)ListBoxVoluntarios.Items[ListBoxVoluntarios.SelectedIndex];
            voluntario.EliminarVoluntario();
            ActualizarListBox();
        }

        private void BtnCargarImagenVoluntario_Click(object sender, RoutedEventArgs e)
        {
            var abrirDialog = new OpenFileDialog();
            abrirDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            if (abrirDialog.ShowDialog() == true)
            {
                try
                {
                    string nombreImagen = CopiarImagen(abrirDialog.FileName);


                    string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    path = path.Replace("\\bin\\Debug\\", "");
                    path = path + "/Presentacion/RecursosPersonas/" + nombreImagen;

                    CambiarFotoPerro(("RecursosPersonas/" + nombreImagen));

                    fotoVoluntario.Source = new ImageSourceConverter().ConvertFromString(path) as ImageSource;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }


        ////////////////////////////////////////        METODOS AUXILIARES        ////////////////////////////////////////
        private Voluntario RellenarVoluntario(Voluntario voluntario)
        {

            voluntario.NombreCompleto = txtNombre.Text;
            voluntario.Correo = txtCorreo.Text;
            voluntario.Dni = txtDni.Text;
            voluntario.Telefono = Int32.Parse(txtTelefono.Text);
            voluntario.Horario = txtHorario.Text;
            voluntario.ZonaDisponibilidad = txtZonaDisponibilidad.Text;
            if (voluntario.Foto == null)
            {
                voluntario.Foto = "";
            }
            



            return voluntario;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = "";
            txtHorario.Text = "";
            txtZonaDisponibilidad.Text = "";
            BtnModificar.IsEnabled = false;
            BtnEliminar.IsEnabled = false;
            BtnImagenVoluntarios.IsEnabled = false;
        }

        private void ActualizarListBox()
        {
            ListBoxVoluntarios.SelectedIndex = -1;
            ListBoxVoluntarios.Items.Clear();
            LimpiarCampos();
            Voluntario volAUX = new Voluntario();
            arrayVoluntarios = volAUX.LeerTodosVoluntarios();
            foreach (Voluntario voluntario in arrayVoluntarios)
            {
                if (voluntario.Foto == "")
                {
                    voluntario.Foto = "RecursosPersonas/default.jpg";
                }

                ListBoxVoluntarios.Items.Add(voluntario);
            }
        }

        private string CopiarImagen(string sourcePath)
        {
            string[] subs = sourcePath.Split('\\');
            string fName = subs[subs.Length - 1];

            string sourceDir = sourcePath.Replace("\\" + fName, "");

            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            path = path.Replace("\\bin\\Debug\\", "");

            string destinoDir = path + "/Presentacion/RecursosPersonas";


            string[] picList = Directory.GetFiles(destinoDir, "*.jpg");

            if (!(picList.Contains(destinoDir + "\\" + fName)))
            {
                try
                {
                    File.Copy(System.IO.Path.Combine(sourceDir, fName), System.IO.Path.Combine(destinoDir, fName), true);
                }
                catch (DirectoryNotFoundException dirNotFound)
                {
                    MessageBox.Show("Error al cargar la imagen " + dirNotFound.Message);
                }
            }
            return fName;
        }

        private void CambiarFotoPerro(string nuevaFoto)
        {
            Voluntario voluntario = (Voluntario)ListBoxVoluntarios.Items[ListBoxVoluntarios.SelectedIndex];
            try
            {
                voluntario.Foto = nuevaFoto;
                voluntario.ModificarVoluntario();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message + " .");
            }
        }

    }
}
