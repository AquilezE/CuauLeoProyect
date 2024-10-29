﻿using System;
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