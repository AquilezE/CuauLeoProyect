using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;

namespace Cliente.Utils
{

    internal class ThemeManagerService
    {

        public static void ChangePrimaryColor(MaterialDesignColor newPrimaryColor)
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();

            theme.SetPrimaryColor(SwatchHelper.Lookup[newPrimaryColor]);
            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(MaterialDesignColor newSecondaryColor)
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();

            theme.SetSecondaryColor(SwatchHelper.Lookup[newSecondaryColor]);
            paletteHelper.SetTheme(theme);
        }

        public static void ChangeDefaultForeground(Color foregroundColor)
        {
            var brush = new SolidColorBrush(foregroundColor);

            Application.Current.Resources["MaterialDesignBody"] = brush;
            Application.Current.Resources["MaterialDesignBodyLight"] = brush;
            Application.Current.Resources["MaterialDesignHeadline"] = brush;
            Application.Current.Resources["MaterialDesignSubheading"] = brush;
            Application.Current.Resources["MaterialDesignButtonForeground"] = brush;
            Application.Current.Resources["MaterialDesignCaptionForeground"] = brush;
            Application.Current.Resources["MaterialDesignSnackbarMessageForeground"] = brush;
            Application.Current.Resources["MaterialDesignSnackbarActionButtonForeground"] = brush;

            Application.Current.Resources["TextForeground"] = brush;
            Application.Current.Resources["MaterialDesignTextAreaForeground"] = brush;
            Application.Current.Resources["MaterialDesignTextBoxForeground"] = brush;
            Application.Current.Resources["MaterialDesignLabelForeground"] = brush;
        }

    }

}