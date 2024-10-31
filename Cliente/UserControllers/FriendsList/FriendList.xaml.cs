using Cliente.Pantallas;
using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class FriendList : UserControl, ISocialManagerCallback, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Cliente.Friend> _friends;
        private ICollectionView _friendsFiltered;
        private SocialManagerClient _socialManager;
        private string _filterSearchName;

        public FriendList()
        {
            InitializeComponent();
            DataContext = this;
            _socialManager = new SocialManagerClient(new System.ServiceModel.InstanceContext(this));
            _friends = Social.Instance.friendList;

            _friendsFiltered = CollectionViewSource.GetDefaultView(_friends);
            _friendsFiltered.Filter = FilterFriends;

            FriendsListBox.ItemsSource = _friendsFiltered;
        }

        public string filterSearchName{ get => _filterSearchName;
            set
            {
                _filterSearchName = value;
                OnPropertyChanged(nameof(filterSearchName));
                _friendsFiltered.Refresh();
            }
        }

        private bool FilterFriends(object filteredFriend)
        {
            if (filteredFriend is Cliente.Friend friend)
            {
                if (string.IsNullOrEmpty(filterSearchName))
                    return true;

                var filterCriteria = filterSearchName.ToLower();
                return friend.FriendName.ToLower().Contains(filterCriteria);
            }
            return false;
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

        private void FriendLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is Friend friendUserControl)
            {
                friendUserControl.deleteFriend += OnFriendDelete;
                friendUserControl.blockUser += OnBlockUser;
            }
        }

        private void OnBlockUser(object sender, Cliente.Friend e)
        {
            if (e != null)
            {
                try
                {
                    bool result = _socialManager.BlockFriend(User.Instance.ID, e.FriendId);
                    if (result)
                    {
                        _friends.Remove(e);
                        Social.Instance.GetBlockedUsers();
                    }
                    else
                    {
                        MessageBox.Show("An error ocurred while blocking user");
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("An error ocurred while blocking user");
                }
            }
        }

        private void OnFriendDelete(object sender, Cliente.Friend e)
        {
            if (e != null)
            {
                try
                {
                    bool result = _socialManager.DeleteFriend(User.Instance.ID, e.FriendId);
                    if (result)
                    {
                        _friends.Remove(e);
                    }
                    else
                    {
                        MessageBox.Show("An error ocurred while deleting friend");
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("An error ocurred while deleting friend");
                }
            }
        }
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
