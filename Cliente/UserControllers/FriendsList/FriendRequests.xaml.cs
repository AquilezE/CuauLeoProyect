using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.FriendsList
{
    /// <summary>
    /// Lógica de interacción para FriendRequests.xaml
    /// </summary>
    public partial class FriendRequests : UserControl
    {
        private ObservableCollection<Cliente.FriendRequest> _friendRequests;
        public FriendRequests()
        {
            InitializeComponent();
            DataContext = Social.Instance;
            _friendRequests = Social.Instance.FriendRequests;
            FriendRequestsListBox.ItemsSource = _friendRequests;
        }

        public void OnFriendNew(FriendDTO[] friends)
        {
            //delete
            throw new NotImplementedException();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void FriendRequestLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FriendRequest friendRequestUserControl)
            {
                friendRequestUserControl.acceptFriend += OnFriendRequestAccept;
                friendRequestUserControl.declineFriend += OnFriendRequestDecline;
            }
        }

        private void OnFriendRequestAccept(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try
                {
                    Social.Instance.socialManagerClient.AcceptFriendRequestAsync(User.Instance.ID, e.SenderId, e.FriendRequestId);
                    //MISSING EXCEPTION LIKE AN ERROR NOT LIKE COMUNICATION, ALSO ADD LOCALIZATION TO THAT EXCEPTION
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(LangUtils.Translate("lblErrNoConection"));
                }
            }
        }

        private void OnFriendRequestDecline(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try{
                bool result = Social.Instance.socialManagerClient.DeclineFriendRequest(e.FriendRequestId);

                    if (result)
                    {
                        Social.Instance.FriendRequests.Remove(e);
                    }
                    else
                    {
                        MessageBox.Show(LangUtils.Translate("lblErrDecliningFRException"));
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(LangUtils.Translate("lblErrNoConection"));
                }
            }
        }
    }
}
