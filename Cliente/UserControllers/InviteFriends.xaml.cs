using System.ComponentModel;
using System.Windows;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Interaction logic for InviteFriends.xaml
    /// </summary>
    public partial class InviteFriends : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int currentLobbyId;
        public InviteFriends(int currentLobbyId)
        {
            InitializeComponent();
            DataContext = Social.Instance;
            this.currentLobbyId = currentLobbyId;
        }

        private void FriendLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FriendToInvite friendToInviteUserControl)
            {
                friendToInviteUserControl.inviteFriend += OnFriendInvite;
            }
        }

        private void OnFriendInvite(object sender, Cliente.Friend friend)
        {
               Social.Instance.socialManagerClient.InviteFriendToLobby(User.Instance.Username, friend.FriendId, currentLobbyId);
        }
    }
}
