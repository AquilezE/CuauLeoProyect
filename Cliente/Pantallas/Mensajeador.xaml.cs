using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

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
            //MainWindow main = (MainWindow)Application.Current.MainWindow;
            //main.NavigateToView(new MenuPrincipal(),600,600);
        }
    }
}
