using System.Windows.Controls;
using System.Windows;
using Haley.Utils;

namespace Cliente.UserControllers
{
    public class KickPlayerDialog : Window
    {
        public string KickReason { get; private set; }

        public KickPlayerDialog()
        {
            Title = LangUtils.Translate("lblKickPlayer");
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var grid = new Grid();
            var textBox = new TextBox { Margin = new Thickness(10) };
            var stackPanel = new StackPanel
                { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            var confirmButton = new Button { Content = LangUtils.Translate("btnKick"), Margin = new Thickness(5) };
            var cancelButton = new Button { Content = LangUtils.Translate("btnCancel"), Margin = new Thickness(5) };

            confirmButton.Click += (s, e) =>
            {
                KickReason = textBox.Text;
                DialogResult = true;
            };
            cancelButton.Click += (s, e) => DialogResult = false;

            stackPanel.Children.Add(confirmButton);
            stackPanel.Children.Add(cancelButton);
            grid.Children.Add(textBox);
            grid.Children.Add(stackPanel);
            Content = grid;
        }
    }
}