using System.Windows;

namespace Cliente.GameUserControllers
{

    public partial class ProvokeAnimationWindow : Window
    {
        public ProvokeAnimationWindow()
        {
            InitializeComponent();
        }

        private void ProvokeAnimationImage_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}