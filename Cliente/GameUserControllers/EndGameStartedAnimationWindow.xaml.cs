using System;
using System.Windows;

namespace Cliente.GameUserControllers
{

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