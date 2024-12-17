using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class MonstersViewerPlayer1 : UserControl
    {
        public EventHandler<MonstersViewerPlayer1> closePanel;

        public MonstersViewerPlayer1()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player1Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closePanel?.Invoke(sender, this);
        }
    }
}