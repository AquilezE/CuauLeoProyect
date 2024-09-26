﻿using Cliente.UserControllers;
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

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for RegisterAccount.xaml
    /// </summary>
    /// 

    //this will implement the IRegisterAccountService interface
    public partial class RegisterAccount : UserControl
    {
        public RegisterAccount()
        {
            InitializeComponent();
            var registerAccountFields = new RegisterAccountFields();
            registerAccountFields.RegistrationFilled += OnRegistrationFilled;
            RegisterAccountContentControl.Content = registerAccountFields;
        }

        private void OnRegistrationFilled(object sender, EventArgs e)
        {
            var registerCodeVerification = new RegisterCodeVerification();
            registerCodeVerification.VerificationCompleted += OnVerificationCompleted;
            RegisterAccountContentControl.Content = registerCodeVerification;
        }

        private void OnVerificationCompleted(object sender, EventArgs e)
        {
            RegisterAccountContentControl.Content = new RegisterSuccess();
        }
    }
}
