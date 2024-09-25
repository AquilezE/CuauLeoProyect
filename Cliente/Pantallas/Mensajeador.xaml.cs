using Cliente.LobbyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para Mensajeador.xaml
    /// </summary>
    public partial class Mensajeador : UserControl
    {
        public Mensajeador()
        {
            InitializeComponent();
            InitializeService();


        }
        private void InitializeService()
        {
            var instanceContext = new InstanceContext(this);
        }

        public void enviarMensaje(object sender, RoutedEventArgs e)
        {
        }

        public void GetAnswerMessage(string message)
        {
            lbMensaje.Content = message;
        }

        public void irMenuPrincipal(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToMensajeador(new MenuPrincipal(),600,600);
        }
    }
}
