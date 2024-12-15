using System.Windows;
using System.Windows.Threading;

namespace Cliente
{
    public partial class App : Application
    {
        public static Dispatcher AppDispatcher { get; private set; }

        private App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-ES");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDispatcher = Dispatcher.CurrentDispatcher;
        }
    }
}