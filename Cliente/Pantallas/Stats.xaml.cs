using Cliente.ServiceReference;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{

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
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            statsManagerClient = new StatsManagerClient(new System.ServiceModel.InstanceContext(this));
            FillUserStats();
        }
    }
}