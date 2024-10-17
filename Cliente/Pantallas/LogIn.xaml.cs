﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Haley.Utils;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Cliente.ServiceReference;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {

        private UsersManagerClient _servicio;

        public LogIn()
        {
            LangUtils.Register();
            LangUtils.ChangeCulture("es-ES");
            InitializeComponent();
            _servicio = new UsersManagerClient();
        }


        private void btPlayAsGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = pbPassword.Password;


            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                
                try
                {

                    if (SetSessionUser(username, password))
                    {
                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.NavigateToMensajeador(new MainMenu());

                    }
                    else
                    {
                        lbErrLabel.Content = "Invalid username or password";
                    }
                }
                catch (Exception ex)
                {
                    lbErrLabel.Content = "Error: " + ex.Message;
                }


            }
            else
            {
                lbErrLabel.Content = "Please enter a username and password";
            }
        }


        private bool SetSessionUser(string email, string password)
        {
            UserDto userDto = _servicio.LogIn(email,password);
            User currentUser = User.Instance;
            
            if (userDto != null)
            {
                currentUser.ID = userDto.UserId;
                currentUser.Username = userDto.Username;
                currentUser.Email = userDto.Email;
                currentUser.ProfilePictureId = userDto.ProfilePictureId;
                return true;
            }

            return false;

        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new RegisterAccount());
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToMensajeador(new RecoverPassword());
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                 throw new Exception("Error");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static void ChangeCulture(string culture)
        {
            if (culture == "Spanish")
            {
                LangUtils.ChangeCulture("es-ES");

            }
            else if (culture == "Inglés")
            {
                LangUtils.ChangeCulture("en-EN");
            }
        }

    }
}
