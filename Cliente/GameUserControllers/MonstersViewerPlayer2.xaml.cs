using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class MonstersViewerPlayer2 : UserControl
    {

        public EventHandler<MonstersViewerPlayer2> ClosePanel;

        public MonstersViewerPlayer2()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player2Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ClosePanel?.Invoke(sender, this);
        }

    }

}