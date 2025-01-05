using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Cliente.Utils;
using System.Threading.Tasks;

namespace Cliente.UserControllers.FriendsList
{

    public partial class AddFriend : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserFound> _usersFound;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public AddFriend()
        {
            InitializeComponent();
            DataContext = this;
            UsersFound = new ObservableCollection<UserFound>();
        }

        private void UserFoundLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FindUserItem findUserItem)
            {
                findUserItem.SendFriendRequest += OnSendFriendRequest;
                findUserItem.BlockUser += OnBlockUser;
            }
        }

        private async void OnBlockUser(object sender, UserFound e)
        {



            if (e != null)
            {

                var blockFriendDialog = new BlockFriendDialog(e.UserFoundUsername)
                {
                    Owner = Window.GetWindow(this)
                };

                bool? dialogResult = blockFriendDialog.ShowDialog();

                if (dialogResult != true)
                {
                    return;
                }

                string blockReason = blockFriendDialog.BlockReason;

                try
                {
                    bool blockResult = await Social.Instance.SocialManagerClient.BlockUserAsync(User.Instance.ID, e.ID, blockReason);
                    if (blockResult)
                    {
                        UsersFound.Remove(e);
                        Social.Instance.GetBlockedUsers();
                    }
                    else
                    {
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    UsersFound.Remove(e);

                }
                catch (FaultException<BevososServerExceptions> ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
                    (sender as FindUserItem).EnableButtons();
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSocialRequestTimeout"));
                    UsersFound.Remove(e);

                    Social.Instance.GetBlockedUsers();

                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    UsersFound.Remove(e);

                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
                    UsersFound.Remove(e);

                }
            }
        }

        private async void OnSendFriendRequest(object sender, UserFound e)
        {
            if (e != null)
            {
                try
                {
                    bool result =
                        await Social.Instance.SocialManagerClient.SendFriendRequestAsync(User.Instance.ID, e.ID);
                    if (result)
                    {
                        UsersFound.Remove(e);
                    }
                    else
                    {
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSendingFriendRequest"));
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    UsersFound.Remove(e);
                }

                catch (FaultException<BevososServerExceptions> ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
                    (sender as FindUserItem).EnableButtons();
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSocialRequestTimeout"));
                    UsersFound.Remove(e);
                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    UsersFound.Remove(e);
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSendingFriendRequest"));
                    UsersFound.Remove(e);
                }
            }

        }


        public ObservableCollection<UserFound> UsersFound
        {
            get => _usersFound;
            set
            {
                _usersFound = value;
                OnPropertyChanged(nameof(UsersFound));
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            UsersFound.Clear();
            string search = tbSearchUser.Text;
            if (!string.IsNullOrEmpty(search))
            {
                try
                {
                    UserDTO[] usersFound =
                        Social.Instance.SocialManagerClient.GetUsersFoundByName(User.Instance.ID, search);
                    foreach (UserDTO userFound in usersFound)
                    {
                        Console.WriteLine(userFound.Username);
                        UsersFound.Add(new UserFound(userFound));
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
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
            }

            tbSearchUser.Text = "";
        }

    }

}