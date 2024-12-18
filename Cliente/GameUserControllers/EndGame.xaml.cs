using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class EndGame : UserControl
    {
        private GameManagerClient gameManagerClient;
        private StatsDTO[] _matchStats;

        public EndGame(StatsDTO[] matchStats)
        {
            InitializeComponent();
            DataContext = GameLogic.Instance;
            gameManagerClient = new GameManagerClient(new InstanceContext(GameLogic.Instance));
            _matchStats = matchStats;

        }

        private void btContinue_Click(object sender, RoutedEventArgs e)
        {
            StatsDTO userStats = _matchStats.FirstOrDefault(stat => stat.Username == User.Instance.Username);

            try
            {
                if (User.Instance.ID > 0)
                {
                    gameManagerClient.SaveStatsForPLayer(userStats, User.Instance.ID);
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new MainMenu());
                }
                else if (User.Instance.ID < 0)
                {
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new JoinLobby(), 650, 800);
                }
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
    }
}