using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para CardsViewer.xaml
    /// </summary>
    public partial class CardsViewer : UserControl
    {
        public CardsViewer()
        {
            InitializeComponent();
            this.DataContext = GameLogic.Instance;
            //AddFirstCards();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                // Determine the amount to scroll. Adjust the multiplier as needed for scroll speed.
                double scrollAmount = e.Delta > 0 ? -50 : 50;
                double newOffset = scrollViewer.HorizontalOffset + scrollAmount;

                // Ensure the new offset is within valid bounds
                if (newOffset < 0)
                    newOffset = 0;
                else if (newOffset > scrollViewer.ScrollableWidth)
                    newOffset = scrollViewer.ScrollableWidth;

                scrollViewer.ScrollToHorizontalOffset(newOffset);
                e.Handled = true; // Prevent vertical scrolling
            }
        }


    }
}
