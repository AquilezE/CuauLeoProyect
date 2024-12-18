using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class MonstersViewerPlayer4 : UserControl
    {

        public EventHandler<MonstersViewerPlayer4> closePanel;

        public MonstersViewerPlayer4()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player4Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closePanel?.Invoke(sender, this);
        }

    }

}