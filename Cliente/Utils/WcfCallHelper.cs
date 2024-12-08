using System;
using System.ServiceModel;
using System.Windows;
using Cliente.Pantallas;
using Cliente;

public static class WcfCallHelper
{
    //private static readonly ILogger _logger = LoggerManager.GetLogger();

    // Método genérico para métodos que retornan un valor
    public static T Execute<T>(Func<T> func, bool requireLogin = false)
    {
        try
        {
            return func();
        }
        catch (EndpointNotFoundException ex)
        {
            //EmergentWindows.CreateConnectionFailedMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (TimeoutException ex)
        {
            //EmergentWindows.CreateTimeOutMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (FaultException ex)
        {
            //EmergentWindows.CreateServerErrorMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (CommunicationException ex)
        {
            //EmergentWindows.CreateServerErrorMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (Exception ex)
        {
            //EmergentWindows.CreateUnexpectedErrorMessageWindow();
            HandleFatalException(ex, requireLogin);
        }
        return default(T);
    }

    // Método para métodos que no retornan un valor
    public static bool Execute(Action action, bool requireLogin = false)
    {
        try
        {
            action();
            return true;
        }
        catch (EndpointNotFoundException ex)
        {
            //EmergentWindows.CreateConnectionFailedMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (TimeoutException ex)
        {
            //EmergentWindows.CreateTimeOutMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (FaultException ex)
        {
            //EmergentWindows.CreateServerErrorMessageWindow();
            HandleException(ex, requireLogin);
            NavigateToLogin();
        }
        catch (CommunicationException ex)
        {
            //EmergentWindows.CreateServerErrorMessageWindow();
            HandleException(ex, requireLogin);
        }
        catch (Exception ex)
        {
            //EmergentWindows.CreateUnexpectedErrorMessageWindow();
            HandleFatalException(ex, requireLogin);
        }
        return false;
    }

    private static void HandleException(Exception ex, bool requireLogin)
    {


        Console.WriteLine("Pedos");
        if (requireLogin)
        {
            NavigateToLogin();
        }
        else
        {
            NavigateToMainMenu();
        }
    }

    private static void HandleFatalException(Exception ex, bool requireLogin)
    {
        Console.WriteLine("Pedotes");
        //_logger.Fatal($"{ex.Message}\n{ex.StackTrace}");
        if (requireLogin)
        {
            NavigateToLogin();
        }
        else
        {
            NavigateToMainMenu();
        }
    }

    private static void NavigateToLogin()
    {

        App.AppDispatcher.Invoke(() =>
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        });
    }

    private static void NavigateToMainMenu()
    {
        App.AppDispatcher.Invoke(() =>
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        });
    }

}
