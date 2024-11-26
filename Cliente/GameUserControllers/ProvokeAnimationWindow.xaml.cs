using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfAnimatedGif;
using WpfAnimatedGif;

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
            this.Close();
        }
    }
}
