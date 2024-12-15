using System;
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
                    Application.Current.MainWindow,
                    Corner.TopRight,
                    10,
                    10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    TimeSpan.FromSeconds(3),
                    MaximumNotificationCount.FromCount(5));

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