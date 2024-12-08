using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cliente.Converters
{
    public class KickButtonVisibilityConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is bool isLeader &&
                values[1] is int currentUserId &&
                values[2] is int userId)
            {
                // Show the Kick button only if the current user is the leader and not the same as the user in the control
                if (isLeader && currentUserId != userId)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
