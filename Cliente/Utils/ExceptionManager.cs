using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Cliente.Utils
{

    public static class ExceptionManager
    {

        private static ILogger _logger = LoggerService.GetLogger();

        public static void LogErrorException(Exception ex)
        {
            _logger.Error("Error encountered in method '{0}'.\nMessage: {1}\nStackTrace:\n{2}", ex.TargetSite,
                ex.Message, ex.StackTrace);
        }

        public static void LogFatalException(Exception ex)
        {
            _logger.Fatal("Fatal error in method '{0}'.\nMessage: {1}\nStackTrace:\n{2}", ex.TargetSite, ex.Message,
                ex.StackTrace);
        }

    }

}