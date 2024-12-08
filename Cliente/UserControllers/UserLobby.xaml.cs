using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class UserLobby : UserControl
    {
        public event EventHandler<Cliente.UserLobby> KickRequested;


        public static readonly DependencyProperty IsLeaderProperty =
            DependencyProperty.Register("IsLeader", typeof(bool), typeof(UserLobby), new PropertyMetadata(false));

        public bool IsLeader
        {
            get => (bool)GetValue(IsLeaderProperty);
            set => SetValue(IsLeaderProperty, value);
        }

        public static readonly DependencyProperty CurrentUserIdProperty =
            DependencyProperty.Register("CurrentUserId", typeof(int), typeof(UserLobby), new PropertyMetadata(0));

        public int CurrentUserId
        {
            get => (int)GetValue(CurrentUserIdProperty);
            set => SetValue(CurrentUserIdProperty, value);
        }


        public UserLobby()
        {
            InitializeComponent();
        }

        private void KickButton_Click(object sender, RoutedEventArgs e)
        {
            KickRequested?.Invoke(this, DataContext as Cliente.UserLobby);
        }
    }
}
