using System.Windows.Controls;
using System.Windows.Input;

namespace Cliente.GameUserControllers
{

    public partial class CardsViewer : UserControl
    {
        public CardsViewer()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                double scrollAmount = e.Delta > 0 ? -50 : 50;
                double newOffset = scrollViewer.HorizontalOffset + scrollAmount;

                if (newOffset < 0)
                    newOffset = 0;
                else if (newOffset > scrollViewer.ScrollableWidth)
                    newOffset = scrollViewer.ScrollableWidth;

                scrollViewer.ScrollToHorizontalOffset(newOffset);
                e.Handled = true; 
            }
        }
    }
}