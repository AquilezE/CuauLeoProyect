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

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Lógica de interacción para MonstersViewerPlayer3.xaml
    /// </summary>
    public partial class MonstersViewerPlayer3 : UserControl
    {
        public EventHandler<MonstersViewerPlayer3> closePanel;
        public MonstersViewerPlayer3()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player3Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closePanel?.Invoke(sender, this);
        }
    }
}