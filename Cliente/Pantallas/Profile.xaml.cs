﻿using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
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

namespace Cliente.Pantallas
{
    /// <summary>
    /// Lógica de interacción para Profile.xaml
    /// </summary>
    public partial class Profile : UserControl, IProfileManagerCallback
    {
        private ProfileManagerClient _servicio;
        private static string newUsername;
        private static int newProfilePictureId;

        public Profile()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            _servicio = new ProfileManagerClient(instanceContext);
            InitializeComponent();
            LoadUserInfo();
        }

        private void imgPfp1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 2;
            ResetAllBorders();
            Console.WriteLine(imgPfp1.Source);
            brdKanye.BorderBrush = Brushes.Red;
        }

        private void imgPfp2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 3;
            ResetAllBorders();
            Console.WriteLine(imgPfp2.Source);
            brdTravis.BorderBrush = Brushes.Red;
        }

        private void imgPfp3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 4;
            ResetAllBorders();
            Console.WriteLine(imgPfp3.Source);
            brdCarti.BorderBrush = Brushes.Red;
        }

        private void imgPfp4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 5;
            ResetAllBorders();
            Console.WriteLine(imgPfp4.Source);
            brdKendrick.BorderBrush = Brushes.Red;
        }

        private void imgPfp5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 6;
            ResetAllBorders();
            Console.WriteLine(imgPfp5.Source);
            brdKitty.BorderBrush = Brushes.Red;
        }

        private void imgPfp6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 7;
            ResetAllBorders();
            Console.WriteLine(imgPfp6.Source);
            brdMelody.BorderBrush = Brushes.Red;
        }

        private void imgPfp7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 8;
            ResetAllBorders();
            Console.WriteLine(imgPfp7.Source);
            brdKuromi.BorderBrush = Brushes.Red;
        }

        private void imgPfp8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newProfilePictureId = 9;
            ResetAllBorders();
            Console.WriteLine(imgPfp8.Source);
            brdCinamon.BorderBrush = Brushes.Red;
        }

        private void ResetAllBorders()
        {
            brdKanye.BorderBrush = Brushes.Black;
            brdTravis.BorderBrush = Brushes.Black;
            brdCarti.BorderBrush = Brushes.Black;
            brdKendrick.BorderBrush = Brushes.Black;
            brdKitty.BorderBrush = Brushes.Black;
            brdMelody.BorderBrush = Brushes.Black;
            brdKuromi.BorderBrush = Brushes.Black;
            brdCinamon.BorderBrush = Brushes.Black;
        }

        private void LoadUserInfo()
        {
            lbUsername.Content = User.Instance.Username;
            lbEmail.Content = User.Instance.Email;  
            lbUserId.Content = User.Instance.ID;

            if (User.Instance.ProfilePictureId != 1)
            {
                imgProfilePicture.Source = new BitmapImage(new Uri("pack://application:,,,/Images/pfp" + User.Instance.ProfilePictureId + ".jpg"));
            }
        }

        private void SetNewUserName()
        {
            newUsername = tbNewUsername.Text;
        }


        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SetNewUserName();
            _servicio.UpdateProfile(User.Instance.ID, newUsername, newProfilePictureId);
        }

        public void OnProfileUpdate(string username, int profilePictureId, string error)
        {
            if (error != null)
            {
                User.instance.Username = username;
                User.instance.ProfilePictureId = profilePictureId;
                LoadUserInfo();
                ResetAllBorders();
            }
            else
            {
                lbErrInvalidUsername.Content = error;
            }

        }
    }
}
