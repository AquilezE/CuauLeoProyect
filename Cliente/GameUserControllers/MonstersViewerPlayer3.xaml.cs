using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class MonstersViewerPlayer3 : UserControl
    {

        public EventHandler<MonstersViewerPlayer3> ClosePanel;

        public MonstersViewerPlayer3()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player3Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ClosePanel?.Invoke(sender, this);
        }

    }

}