using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Protectora.Presentacion
{
    /// <summary>
    /// Lógica de interacción para WindowAnimales.xaml
    /// </summary>
    public partial class WindowAnimales : Window
    {
        ////////////////////////////////////////        Variables        ////////////////////////////////////////

        List<Perro> arrayAnimales;
        WindowMenuSeleccion windowMenuSelec;
        string textPeso = "El peso tiene que estar entre 1 y 100\n";
        string textTamano = "El tamaño tiene que estar entre 10 y 150\n";
        string textEdad = "La edad tiene que estar entre 0 y 20\n";
        string textEdadLongitud = "La edad tiene que tener menos de 3 caracteres\n";
        string textTamanoLongitud = "La tamaño tiene que tener menos de 4 caracteres\n";
        string textPesoLongitud = "La peso tiene que tener menos de 4 caracteres\n";


        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowAnimales(WindowMenuSeleccion windowMenuSeleccion)
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
            if (ListBoxAnimales.SelectedIndex != -1)
            {
                BtnModificar.IsEnabled = true;
                BtnEliminar.IsEnabled = true;
                BtnPadrino.IsEnabled = true;
                BtnImagenPerro.IsEnabled = true;

                Perro perro = (Perro)ListBoxAnimales.Items[ListBoxAnimales.SelectedIndex];
                fotoAnimales.Visibility = System.Windows.Visibility.Visible;
                txtNombre.Text = perro.Nombre.ToString();
                txtPeso.Text = perro.Peso.ToString();
                txtSexo.Text = perro.Sexo.ToString();
                txtRaza.Text = perro.Raza.ToString();
                txtDescripion.Text = perro.Descripcion.ToString();
                txtEnlace.Text = perro.Enlace.ToString();
                txtEstado.Text = perro.Estado.ToString();
                txtTamanio.Text = perro.Tamanio.ToString();
                dateFecha.Text = perro.FechaEntrada.ToString("dd-MM-yyyy");
                txtEdad.Text = perro.Edad.ToString();
                if(perro.Apadrinado == 0)
                {
                    lblPadrino.Content = "Sin padrino";
                }
                else
                {
                    lblPadrino.Content = "Con padrino";
                }
                //Foto
                try
                {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    path = path.Replace("\\bin\\Debug\\", "");
                    path = path + "/Presentacion/" + perro.Foto;

                    fotoAnimales.Source = new ImageSourceConverter().ConvertFromString(path) as ImageSource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
            }

        }

        ////////////////////////////////////////        EVENTOS DE BOTON        ////////////////////////////////////////
        private void BtnAnadir_Click(object sender, RoutedEventArgs e)
        {
            Perro perro = new Perro();
            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtEdad.Text, out ignoreMe) && int.TryParse(txtPeso.Text, out ignoreMe) && int.TryParse(txtTamanio.Text, out ignoreMe))
            {

                if (Int32.Parse(txtTamanio.Text) >= 10 && Int32.Parse(txtTamanio.Text) <= 150 && txtTamanio.Text.Length <= 3
                    && Int32.Parse(txtPeso.Text) >= 1 && Int32.Parse(txtPeso.Text) <= 100 && txtPeso.Text.Length <= 3
                    && Int32.Parse(txtEdad.Text) >= 0 && Int32.Parse(txtEdad.Text) <= 20 && txtEdad.Text.Length <= 2)
                {
                    try
                    {
                        perro = RellenarPerro(perro);
                        perro.InsertarPerro();
                        perro.Apadrinado = 0;
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
                    if (txtTamanio.Text.Length > 3)
                        textAyuda = textAyuda + textTamanoLongitud;
                    else if (Int32.Parse(txtTamanio.Text) <= 10 || Int32.Parse(txtTamanio.Text) >= 150)
                        textAyuda = textAyuda + textTamano;
                    if (txtPeso.Text.Length > 3)
                        textAyuda = textAyuda + textPesoLongitud;
                    else if (Int32.Parse(txtPeso.Text) <= 1 || Int32.Parse(txtPeso.Text) >= 100)
                        textAyuda = textAyuda + textPeso;
                    if (txtEdad.Text.Length > 2)
                        textAyuda = textAyuda + textEdadLongitud;
                    else if (Int32.Parse(txtEdad.Text) <= 0 || Int32.Parse(txtEdad.Text) >= 20)
                        textAyuda = textAyuda + textEdad;



                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nPeso\nEdad\nTamaño");
            }
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Perro perro = (Perro)ListBoxAnimales.Items[ListBoxAnimales.SelectedIndex];
            try
            {

                if (perro.Apadrinado != 0)
                {
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    DialogResult result;
                    string infoMensaje = "¿Quiere eliminar el perro seleccionado?\nEsto implicara tambien borrar a su padrino.";
                    string titulo = "Eliminar perro";

                    result = (DialogResult)MessageBox.Show(infoMensaje, titulo, (MessageBoxButtons)buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        perro.EliminarPerro();
                        Padrino padrino = new Padrino();
                        padrino.Id = perro.Apadrinado;
                        padrino.LeerunPadrino();
                        padrino.EliminarPadrino();
                    }
                }
                else
                {
                    perro.EliminarPerro();
                }
                ActualizarListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message );
            }

        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Perro perro = (Perro)ListBoxAnimales.Items[ListBoxAnimales.SelectedIndex];
            string textAyuda = "";
            int ignoreMe;
            if (int.TryParse(txtEdad.Text, out ignoreMe) && int.TryParse(txtPeso.Text, out ignoreMe) && int.TryParse(txtTamanio.Text, out ignoreMe))
            {

                if (Int32.Parse(txtTamanio.Text) >= 10 && Int32.Parse(txtTamanio.Text) <= 150 && txtTamanio.Text.Length <= 3
                    && Int32.Parse(txtPeso.Text) >= 1 && Int32.Parse(txtPeso.Text) <= 100 && txtPeso.Text.Length <= 3
                    && Int32.Parse(txtEdad.Text) >= 0 && Int32.Parse(txtEdad.Text) <= 20 && txtEdad.Text.Length <= 2)
                {
                    try
                    {
                        perro = RellenarPerro(perro);
                        perro.ModificarPerro();
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
                    if (txtTamanio.Text.Length > 3)
                        textAyuda = textAyuda + textTamanoLongitud;
                    else if (Int32.Parse(txtTamanio.Text) <= 10 || Int32.Parse(txtTamanio.Text) >= 150)
                        textAyuda = textAyuda + textTamano;
                    if (txtPeso.Text.Length > 3)
                        textAyuda = textAyuda + textPesoLongitud;
                    else if (Int32.Parse(txtPeso.Text) <= 1 || Int32.Parse(txtPeso.Text) >= 100)
                        textAyuda = textAyuda + textPeso;
                    if (txtEdad.Text.Length > 2)
                        textAyuda = textAyuda + textEdadLongitud;
                    else if (Int32.Parse(txtEdad.Text) <= 0 || Int32.Parse(txtEdad.Text) >= 20)
                        textAyuda = textAyuda + textEdad;



                    MessageBox.Show("Error al introducir información:\n" + textAyuda);
                }
            }
            else
            {
                MessageBox.Show("Solo se admiten caracteres númericos los campos \nPeso\nEdad\nTamaño");
            }

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnPadrino_Click(object sender, RoutedEventArgs e)
        {
            Perro perro = (Perro)ListBoxAnimales.Items[ListBoxAnimales.SelectedIndex];
            WindowPadrino winPadrino = new WindowPadrino(this, perro);
            ActualizarListBox();
            winPadrino.Show();
            Hide();
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            windowMenuSelec.Show();
            this.Hide();
        }

        private void BtnCargarImagenPerro_Click(object sender, RoutedEventArgs e)
        {
            var abrirDialog = new Microsoft.Win32.OpenFileDialog();
            abrirDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            if (abrirDialog.ShowDialog() == true)
            {
                try
                {
                    string nombreImagen = CopiarImagen(abrirDialog.FileName);
 

                    string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    path = path.Replace("\\bin\\Debug\\", "");
                    path = path + "/Presentacion/RecursosPerros/" + nombreImagen;

                    CambiarFotoPerro(("RecursosPerros/" + nombreImagen));

                    fotoAnimales.Source = new ImageSourceConverter().ConvertFromString(path) as ImageSource;
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }

        ////////////////////////////////////////        METODOS AUXILIARES        ////////////////////////////////////////
        private Perro RellenarPerro(Perro perro)
        {

            perro.Nombre = txtNombre.Text;
            perro.Peso = Int32.Parse(txtPeso.Text);
            perro.Sexo = txtSexo.Text;
            perro.Raza = txtRaza.Text;
            perro.Descripcion = txtDescripion.Text;
            perro.Enlace = txtEnlace.Text;
            perro.Estado = txtEstado.Text;
            perro.Tamanio = Int32.Parse(txtTamanio.Text);
            perro.FechaEntrada = DateTime.Parse(dateFecha.Text);
            perro.Edad = Int32.Parse(txtEdad.Text);
            if (perro.Foto == null)
            {
                perro.Foto = "";
            }

            return perro;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtPeso.Text = "";
            txtSexo.Text = "";
            txtRaza.Text = "";
            txtDescripion.Text = "";
            txtEnlace.Text = "";
            txtEstado.Text = "";
            txtTamanio.Text = "";
            dateFecha.Text = "";
            txtEdad.Text = "";
            fotoAnimales.Visibility = System.Windows.Visibility.Hidden;
            BtnModificar.IsEnabled = false;
            BtnEliminar.IsEnabled = false;
            BtnPadrino.IsEnabled = false;
            BtnImagenPerro.IsEnabled = false;
            lblPadrino.Content = "   Padrino";
        }

        private void ActualizarListBox()
        {
            ListBoxAnimales.SelectedIndex = -1;
            ListBoxAnimales.Items.Clear();
            LimpiarCampos();
            Perro aniAUX = new Perro();
            arrayAnimales = aniAUX.LeerTodosAnimales();
            foreach (Perro perro in arrayAnimales)
            {
                if (perro.Foto == "")
                {
                    perro.Foto = "RecursosPerros/default.jpg";
                }

                ListBoxAnimales.Items.Add(perro);
            }
        }

        private string CopiarImagen(string sourcePath)
        {
            string[] subs = sourcePath.Split('\\');
            string fName = subs[subs.Length - 1];

            string sourceDir = sourcePath.Replace("\\"+ fName, "");

            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            path = path.Replace("\\bin\\Debug\\", "");

            string destinoDir = path + "/Presentacion/RecursosPerros";


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
            Perro perro = (Perro)ListBoxAnimales.Items[ListBoxAnimales.SelectedIndex];
            try
            {
                perro.Foto = nuevaFoto;
                perro.ModificarPerro();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message + " .");
            }
        }

    }
}
