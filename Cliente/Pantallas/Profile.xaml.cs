using Cliente.ServiceReference;
using Cliente.UserControllers.ChangePassword;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cliente.Pantallas
{

    public partial class Profile : UserControl, IProfileManagerCallback
    {
        private ProfileManagerClient _service;
        private static string newUsername;
        private static int newProfilePictureId = 0;

        public Profile()
        {
            var instanceContext = new InstanceContext(this);
            _service = new ProfileManagerClient(instanceContext);
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
            tbNewUsername.Text = "";

            if (User.Instance.ProfilePictureId != 1 || User.Instance.ProfilePictureId != 0)
                imgProfilePicture.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/pfp" + User.Instance.ProfilePictureId + ".jpg"));
        }

        private void SetNewUserName()
        {
            newUsername = tbNewUsername.Text;
        }


        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SetNewUserName();
            lbErrInvalidUsername.Content = "";
            lbErrNothingChanged.Content = "";

            if (newUsername == "" && newProfilePictureId == 0)
                lbErrNothingChanged.Content = LangUtils.Translate("lblErrProfileNothingChanged");
            else if (newUsername != "" && newProfilePictureId != 0)
                _service.UpdateProfile(User.Instance.ID, newUsername, newProfilePictureId);
            else if (newUsername == "" && newProfilePictureId != 0)
                _service.UpdateProfile(User.instance.ID, "Not changed", newProfilePictureId);
            else if (newUsername != "" && newProfilePictureId == 0)
                _service.UpdateProfile(User.instance.ID, newUsername, User.Instance.ProfilePictureId);
        }

        public void OnProfileUpdate(string username, int profilePictureId, string error)
        {
            if (username != "Not changed") User.instance.Username = username;
            User.instance.ProfilePictureId = profilePictureId;
            LoadUserInfo();
            ResetAllBorders();

            newUsername = "";
            newProfilePictureId = 0;
            lbErrInvalidUsername.Content = error;
        }

        private void btChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new ChangePassword(), 750, 800);
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

        public void OnPasswordChange(string error)
        {
            throw new NotImplementedException();
        }
    }
}