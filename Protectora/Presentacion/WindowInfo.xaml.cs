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

namespace Protectora.Presentacion
{
    /// <summary>
    /// Lógica de interacción para WindowInfo.xaml
    /// </summary>
    public partial class WindowInfo : Window
    {

        ////////////////////////////////////////        Arranque y cierre        ////////////////////////////////////////

        public WindowInfo()
        {
            InitializeComponent();
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
    }
}
