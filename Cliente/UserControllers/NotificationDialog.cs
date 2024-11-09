using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Cliente.UserControllers
{
    public class NotificationDialog
    {
        private Notifier _notifier;

        public NotificationDialog()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        public void ShowInfoNotification(string message)
        {
            _notifier.ShowInformation(message);
        }

        public void ShowSuccessNotification(string message)
        {
            _notifier.ShowSuccess(message);
        }

        public void ShowWarningNotification(string message)
        {
            _notifier.ShowWarning(message);
        }

        public void ShowErrorNotification(string message)
        {
            _notifier.ShowError(message);
        }

        public void Dispose()
        {
            _notifier.Dispose();
        }
    }
}
