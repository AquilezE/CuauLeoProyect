using Cliente.Pantallas;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.GameUserControllers
{

    public partial class EndGame : UserControl
    {
        public EndGame()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance;
        }

        private void btContinue_Click(object sender, RoutedEventArgs e)
        {
            if (User.Instance.ID > 0)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new MainMenu());
            }
            else if (User.Instance.ID < 0)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new JoinLobby(), 650, 800);
            }
        }
    }
}