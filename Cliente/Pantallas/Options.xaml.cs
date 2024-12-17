using Cliente.Utils;
using Haley.Utils;
using MaterialDesignColors;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Cliente.Pantallas
{

    public partial class Options : UserControl
    {
        private bool _isLoaded;
        public Options()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
        }
        private void cbLanguageOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoaded) return;

            var selectedItem = cbLanguageOptions.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string culture = selectedItem.Content as string;
                ChangeCulture(culture);
            }
        }

        private void ChangeCulture(string culture)
        {
            try
            {
                switch (culture)
                {
                    case "Spanish":
                        LangUtils.ChangeCulture("es");
                        break;
                    case "Inglés":
                        LangUtils.ChangeCulture("en");
                        break;
                    default:
                        LangUtils.ChangeCulture("en");
                        break;
                }
            }
            catch (CultureNotFoundException ex)
            {
                Console.WriteLine("Error changing culture: " + ex.Message);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                var selectedColor = "Green";
                var materialColor = (MaterialDesignColor)Enum.Parse(typeof(MaterialDesignColor), selectedColor);
                ThemeManagerService.ChangePrimaryColor(materialColor);
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new MainMenu());
        }

        private void rbPurpleTheme_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                var selectedColor = "Purple";
                var materialColor = (MaterialDesignColor)Enum.Parse(typeof(MaterialDesignColor), selectedColor);
                ThemeManagerService.ChangePrimaryColor(materialColor);
                ThemeManagerService.ChangeDefaultForeground(Colors.Indigo);
            }
        }

        private void rbBrownTheme_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                var selectedColor = "Brown";
                var materialColor = (MaterialDesignColor)Enum.Parse(typeof(MaterialDesignColor), selectedColor);
                ThemeManagerService.ChangePrimaryColor(materialColor);
                ThemeManagerService.ChangeDefaultForeground(Colors.SaddleBrown);
            }
        }

        private void rbGreenTheme_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                var selectedColor = "Green";
                var materialColor = (MaterialDesignColor)Enum.Parse(typeof(MaterialDesignColor), selectedColor);
                ThemeManagerService.ChangePrimaryColor(materialColor);
                ThemeManagerService.ChangeDefaultForeground(Colors.ForestGreen);
            }
        }
    }
}
