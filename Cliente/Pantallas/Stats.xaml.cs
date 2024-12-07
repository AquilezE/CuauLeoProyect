using Cliente.LogInService;
using Cliente.ServiceReference;
using Haley.Utils;
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
using ToastNotifications.Lifetime;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Lógica de interacción para Stats.xaml
    /// </summary>
    public partial class Stats : UserControl, IStatsManagerCallback
    {
        public StatsManagerClient statsManagerClient;
        public Stats()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        public void FillUserStats()
        {
            statsManagerClient.GetCurrentUserStats(User.instance.ID);

        }

        public void OnStatsReceived(int wins, int monsters, int babies)
        {
            lbWinsCounter.Content = wins;
            lbMonstersCounter.Content = monsters;
            lbBabiesCounter.Content = babies;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            statsManagerClient = new StatsManagerClient(new System.ServiceModel.InstanceContext(this));
            FillUserStats();
        }
    }
}
