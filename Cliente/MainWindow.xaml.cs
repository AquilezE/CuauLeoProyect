using System.Windows;
using System.Windows.Controls;
using Cliente.Pantallas;


namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToView(new LogIn());
        }

        public void NavigateToView(UserControl control, bool isFullscreen)
        {
            if (isFullscreen)
            {
                MainContentControl.Content = control;
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                Width = SystemParameters.PrimaryScreenWidth;
                Height = SystemParameters.PrimaryScreenHeight;

                ResizeMode = ResizeMode.NoResize;
            }
        }

        public void NavigateToView(UserControl control, double? newWidth = null, double? newHeight = null)
        {
            MainContentControl.Content = control;
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;

            if (newWidth != null && newHeight != null)
            {
                Height = newHeight.Value;
                Width = newWidth.Value;
                SizeToContent = SizeToContent.Manual;
            }
            else
                SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}