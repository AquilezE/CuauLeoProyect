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
using Haley.Utils;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Interaction logic for BlockFriendDialog.xaml
    /// </summary>
    public partial class BlockFriendDialog : Window
    {
        public string BlockReason { get; private set; }

        public BlockFriendDialog(string username)
        {
            InitializeComponent();
            Title = LangUtils.Translate("lblBlocking") + $" {username}";
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btBlock_Click(object sender, RoutedEventArgs e)
        {
            BlockReason = tbReason.Text.Trim();
            if (string.IsNullOrEmpty(BlockReason))
            {
                MessageBox.Show(LangUtils.Translate("lblTypeBlockReason"), "???", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        public BlockFriendDialog()
        {
            InitializeComponent();
        }
    }
}
