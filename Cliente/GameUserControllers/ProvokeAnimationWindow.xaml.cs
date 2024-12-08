using System.Windows;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Interaction logic for ProvokeAnimationWindow.xaml
    /// </summary>
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
