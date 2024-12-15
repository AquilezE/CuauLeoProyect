using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Lógica de interacción para MonstersViewerPlayer2.xaml
    /// </summary>
    public partial class MonstersViewerPlayer2 : UserControl
    {
        public EventHandler<MonstersViewerPlayer2> closePanel;

        public MonstersViewerPlayer2()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance.Player2Monsters;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closePanel?.Invoke(sender, this);
        }
    }
}