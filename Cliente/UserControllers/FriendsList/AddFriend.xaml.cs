﻿using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

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
                        MessageBox.Show(LangUtils.Translate("lblErrBlockingException"));
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(LangUtils.Translate("lblErrNoConection"));
                }
            }
        }

        private void OnSendFriendRequest(object sender, UserFound e)
        {
            //MISSING EXCEPTIONS WITH INTERNAZIONALIZATION
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
                UserDTO[] usersFound = Social.Instance.socialManagerClient.GetUsersFoundByName(User.Instance.ID,search);
                foreach (UserDTO userFound in usersFound)
                {
                    Console.WriteLine(userFound.Username);
                    UsersFound.Add(new UserFound(userFound));
                }
            }
            tbSearchUser.Text = "";
        }
    }
}
