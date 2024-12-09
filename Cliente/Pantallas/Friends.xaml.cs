﻿using Cliente.UserControllers.FriendsList;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Lógica de interacción para Friends.xaml
    /// </summary>
    public partial class Friends : UserControl
    {
        public Friends()
        {
            InitializeComponent();
        }

        private void btFriendsList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new FriendList());
        }

        private void btFriendRequests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new FriendRequests());
        }

        private void btAddFriend_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new AddFriend());
        }

        private void btBlockList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new BlockedUsersList());
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }
    }
}
