using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para MonstersViewerPlayer1.xaml
    /// </summary>
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
