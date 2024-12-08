using System;
using System.Windows;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Lógica de interacción para EndGameStartedAnimationWindow.xaml
    /// </summary>
    public partial class EndGameStartedAnimationWindow : Window
    {
        public EndGameStartedAnimationWindow()
        {
            InitializeComponent();
        }
        private void EndGameStarted_AnimationCompleted(object sender, EventArgs e)
        {
            Close();
        }
    }
}
