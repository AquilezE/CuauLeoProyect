using Cliente.Pantallas;
using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Cliente.UserControllers.FriendsList
{
    /// <summary>
    /// Lógica de interacción para FriendRequests.xaml
    /// </summary>
    public partial class FriendRequests : UserControl, ISocialManagerCallback
    {
        private ObservableCollection<Cliente.FriendRequest> _friendRequests;
        private SocialManagerClient _socialManager;
        public FriendRequests()
        {
            InitializeComponent();
            _socialManager = new SocialManagerClient(new System.ServiceModel.InstanceContext(this));
            _friendRequests = Social.Instance.friendRequests;
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
                try{
                bool result = _socialManager.AcceptFriendRequest(User.Instance.ID,e.SenderId,e.FriendRequestId);

                    if (result)
                    {
                        Social.Instance.GetFriends();
                        _friendRequests.Remove(e);
                    }
                    else
                    {
                        MessageBox.Show("An error ocurred while Accepting Friend Request");
                    }
                }
                catch(CommunicationException ex)
                {
                    MessageBox.Show("An error ocurred while Accepting Friend Request");
                }
            }
        }

        private void OnFriendRequestDecline(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try{
                bool result =  _socialManager.DeclineFriendRequest(e.FriendRequestId);

                    if (result)
                    {
                        _friendRequests.Remove(e);
                    }
                    else
                    {
                        MessageBox.Show("An error ocurred while Declining Friend Request");
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("An error ocurred while Declining Friend Request");
                }
            }
        }
    }
}
