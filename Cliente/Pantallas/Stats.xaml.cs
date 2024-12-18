using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.Utils;
using Haley.Utils;
using System.ServiceModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{

    public partial class Stats : UserControl
    {

        public StatsManagerClient StatsManagerClient;

        public Stats()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        private void FillUserStats()
        {
            try
            {
                StatsDTO userStats = StatsManagerClient.GetCurrentUserStats(User.instance.ID);
                lbWinsCounter.Content = userStats.Wins;
                lbMonstersCounter.Content = userStats.MonstersCreated;
                lbBabiesCounter.Content = userStats.AnihilatedBabies;
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StatsManagerClient = new StatsManagerClient();
            FillUserStats();
        }

    }

}