﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cliente.Pantallas;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToMensajeador(new LogIn());
        }
        public void NavigateToMensajeador(UserControl control, double ? newWidth = null, double ? newHeight = null)
        {
            MainContentControl.Content = control;
            if (newWidth != null && newHeight != null)
            {
                this.Height = newHeight.Value;
                this.Width = newWidth.Value;
                this.SizeToContent = SizeToContent.Manual;
            }
            else
            {
                this.SizeToContent = SizeToContent.WidthAndHeight;
            }

        }

    }
}