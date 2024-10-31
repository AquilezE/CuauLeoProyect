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

namespace Cliente.UserControllers
{
    /// <summary>
    /// Lógica de interacción para BlockedUser.xaml
    /// </summary>
    public partial class BlockedUser : UserControl
    {
        public event EventHandler<Cliente.Blocked> unblockUser;
        public BlockedUser()
        {
            InitializeComponent();
        }

        private void btUnblock_Click(object sender, RoutedEventArgs e)
        {
            unblockUser?.Invoke(this, DataContext as Cliente.Blocked);
        }
    }
}
