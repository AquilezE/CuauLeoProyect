using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
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
    /// <summary>
    /// Lógica de interacción para AddFriend.xaml
    /// </summary>
    public partial class AddFriend : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserFound> _usersFound;
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
                findUserItem.sendFriendRequest += OnSendFriendRequest;
                findUserItem.blockUser += OnBlockUser;
            }
        }
        private void OnBlockUser(object sender, UserFound e)
        {
            if (e != null)
            {
                try
                {
                    bool result = Social.Instance.socialManagerClient.BlockUser(User.Instance.ID, e.ID);
                    if (result)
                    {
                        UsersFound.Remove(e);
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

        private void OnSendFriendRequest(object sender, UserFound e)
        {
            Social.Instance.socialManagerClient.SendFriendRequest(User.Instance.ID ,e.ID);
            UsersFound.Remove(e);
        }
        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            UsersFound.Clear();
            string search = tbSearchUser.Text;
            if (!string.IsNullOrEmpty(search))
            {
                UserDto[] usersFound = Social.Instance.socialManagerClient.GetUsersFoundByName(User.Instance.ID,search);
                foreach (UserDto userFound in usersFound)
                {
                    Console.WriteLine(userFound.Username);
                    UsersFound.Add(new UserFound(userFound));
                }
            }
            tbSearchUser.Text = "";
        }
    }
}
