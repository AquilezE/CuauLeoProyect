using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.UserControllers.ChangePassword;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cliente.Pantallas
{

    public partial class Profile : UserControl
    {

        private ProfileManagerClient _service;
        private static string _newUsername = User.Instance.Username;
        private static int _newProfilePictureId = User.Instance.ProfilePictureId;
        private readonly Validator _validator = new Validator();


        public Profile()
        {
            _service = new ProfileManagerClient();
            InitializeComponent();
            LoadUserInfo();
        }

        private void imgPfp1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 2;
            ResetAllBorders();
            Console.WriteLine(imgPfp1.Source);
            brdKanye.BorderBrush = Brushes.Red;
        }

        private void imgPfp2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 3;
            ResetAllBorders();
            Console.WriteLine(imgPfp2.Source);
            brdTravis.BorderBrush = Brushes.Red;
        }

        private void imgPfp3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 4;
            ResetAllBorders();
            Console.WriteLine(imgPfp3.Source);
            brdCarti.BorderBrush = Brushes.Red;
        }

        private void imgPfp4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 5;
            ResetAllBorders();
            Console.WriteLine(imgPfp4.Source);
            brdKendrick.BorderBrush = Brushes.Red;
        }

        private void imgPfp5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 6;
            ResetAllBorders();
            Console.WriteLine(imgPfp5.Source);
            brdKitty.BorderBrush = Brushes.Red;
        }

        private void imgPfp6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 7;
            ResetAllBorders();
            Console.WriteLine(imgPfp6.Source);
            brdMelody.BorderBrush = Brushes.Red;
        }

        private void imgPfp7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 8;
            ResetAllBorders();
            Console.WriteLine(imgPfp7.Source);
            brdKuromi.BorderBrush = Brushes.Red;
        }

        private void imgPfp8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _newProfilePictureId = 9;
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

            if (User.Instance.ProfilePictureId != 1 && User.Instance.ProfilePictureId != 0)
            {
                imgProfilePicture.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/pfp" + User.Instance.ProfilePictureId +
                                            ".jpg"));
            }
        }

        private void SetNewUserName()
        {
             _newUsername = tbNewUsername.Text;
             if (string.IsNullOrWhiteSpace(_newUsername))
             {
                 _newUsername = User.Instance.Username;
             }
        }


        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SetNewUserName();
            lbErrInvalidUsername.Content = "";
            lbErrNothingChanged.Content = "";

            string usernameError = _validator.ValidateUsername(_newUsername);
            if(usernameError!= string.Empty)
            {
                lbErrInvalidUsername.Content = LangUtils.Translate(usernameError);
                tbNewUsername.Text = string.Empty;
                return;
            }

            if (_newUsername == User.Instance.Username && _newProfilePictureId == User.Instance.ProfilePictureId)
            {
                lbErrNothingChanged.Content = LangUtils.Translate("lblErrProfileNothingChanged");
            }
            else if (usernameError == string.Empty)
            {
                try
                {
                    int result = _service.UpdateProfile(User.Instance.ID, _newUsername, _newProfilePictureId);

                    if (result == 1)
                    {
                        lbErrInvalidUsername.Content = LangUtils.Translate("lblErrProfileUsernameTaken");
                        _newUsername = User.Instance.Username;
                        return;
                    }
                    if (result ==0)
                    {
                        User.instance.Username = _newUsername;
                        User.instance.ProfilePictureId = _newProfilePictureId;
                        LoadUserInfo();
                        ResetAllBorders();
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (FaultException<BevososServerExceptions> ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
                }
            }
            else
            {
                lbErrInvalidUsername.Content = LangUtils.Translate(usernameError);
            }
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

    }

}