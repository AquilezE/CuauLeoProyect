using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Cliente.UserControllers
{

    public partial class UserLobby : UserControl
    {
        public event EventHandler<User> KickRequested;


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
            KickRequested?.Invoke(this, DataContext as User);
        }
    }
}
