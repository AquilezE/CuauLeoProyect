using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Cliente.UserControllers
{    public class KickPlayerDialog : Window
    {
        public string KickReason { get; private set; }

        public KickPlayerDialog()
        {
            Title = "Kick Player";
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var grid = new Grid();
            var textBox = new TextBox { Margin = new Thickness(10) };
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            var confirmButton = new Button { Content = "Kick", Margin = new Thickness(5) };
            var cancelButton = new Button { Content = "Cancel", Margin = new Thickness(5) };

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
