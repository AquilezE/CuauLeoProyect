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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Lógica de interacción para MonstersViewer.xaml
    /// </summary>
    public partial class MonstersViewer : UserControl
    {
        public EventHandler<MonstersViewer> closePanel;
        public MonstersViewer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closePanel?.Invoke(sender, this);
        }
    }
}
