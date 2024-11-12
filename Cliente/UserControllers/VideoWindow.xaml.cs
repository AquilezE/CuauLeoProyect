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

namespace Cliente.UserControllers
{
    /// <summary>
    /// Interaction logic for VideoWindow.xaml
    /// </summary>
    public partial class VideoWindow : Window
    {
        public VideoWindow()
        {
            InitializeComponent();
            Loaded += VideoWindow_Loaded;
            Closing += VideoWindow_Closing;
        }

        private void VideoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play(); 
        }

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoPlayer.Position = TimeSpan.Zero; 
            videoPlayer.Play(); 
        }

        private void VideoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; 

            Dispatcher.BeginInvoke(new Action(() =>
            {
                VideoWindow newWindow = new VideoWindow();
                newWindow.ShowDialog();
            }));

            this.Hide(); 
        }
    }

}
