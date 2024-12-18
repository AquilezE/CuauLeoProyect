using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Cliente.Utils
{

    public sealed class LoggerService
    {

        private const string DATE_FORMAT = "dd-MM-yyyy";
        private const string ID_FILE_NAME = "Log";
        private const string CHARACTER_SEPARATOR = "_";
        private const string FILE_EXTENSION = ".txt";
        private const string RELATIVE_LOG_FILE_PATH = @"Logs";

        private static ILogger _logger;

        private static readonly object _lock = new object();

        private LoggerService()
        {
            ConfigureLogger(BuildLogFilePath());
        }

        private static void ConfigureLogger(string logFilePath)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    logFilePath,
                    retainedFileCountLimit: 7,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        private static string BuildLogFilePath()
        {
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString(DATE_FORMAT);

            string logFileName = $"{ID_FILE_NAME}{CHARACTER_SEPARATOR}{date}{FILE_EXTENSION}";
            string absoluteLogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RELATIVE_LOG_FILE_PATH);

            if (!Directory.Exists(absoluteLogDirectory))
            {
                Directory.CreateDirectory(absoluteLogDirectory);
            }

            string logFilePath = Path.Combine(absoluteLogDirectory, logFileName);

            return logFilePath;
        }

        public static ILogger GetLogger()
        {
            if (_logger == null)
            {
                string logPath = BuildLogFilePath();
                ConfigureLogger(logPath);
            }

            _logger = Log.Logger;
            return _logger;
        }

        public static void CloseAndFlush()
        {
            lock (_lock)
            {
                (_logger as IDisposable)?.Dispose();
                Log.CloseAndFlush();
                _logger = null;
            }
        }

        public static void ResetLogger()
        {
            CloseAndFlush();
            ConfigureLogger(BuildLogFilePath());
        }

    }

}