using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAPP.App_Start;

namespace WebApiAPP.Logging
{
    public class NLogAdapter : ILogAdapter
    {
        private Logger _logger;

        public NLogAdapter()
        {
            InitilizeLogAdapter();
        }

        private void InitilizeLogAdapter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public enum LogLevel
        {
            FATAL = 0,
            ERROR = 1,
            WARN = 2,
            INFO = 3,
            VERBOSE = 4,
            SUCCESS = 5,
            FAILURE = 6,
            DEBUG = 7
        }

        public enum LoggerSource
        {
            SeriLog = 1,
            Log4Net = 2,
            EnterpriseLib = 3
        }
        public void WriteMessage(string source, LogLevel level, Exception ex)
        {
            switch (level)
            {
                case LogLevel.ERROR:
                    _logger.Error(ex, source);
                    break;

                case LogLevel.DEBUG:
                    _logger.Debug(ex, source);
                    break;

                case LogLevel.INFO:
                    _logger.Info(ex, source);
                    break;

                case LogLevel.WARN:
                    _logger.Warn(ex, source);
                    break;

                case LogLevel.FATAL:
                    _logger.Fatal(ex, source);
                    break;

                default:
                    throw new ArgumentNullException("Invalid log level.");
            }
        }

        public void WriteMessage(string source, LogLevel level, string message)
        {
            message = string.Format("{0}:{1}", source, message);

            switch (level)
            {
                case LogLevel.ERROR:
                    _logger.Error(message);
                    break;

                case LogLevel.DEBUG:
                    _logger.Debug(message);
                    break;

                case LogLevel.INFO:
                    _logger.Info(message);
                    break;

                case LogLevel.WARN:
                    _logger.Info(message);
                    break;

                case LogLevel.FATAL:
                    _logger.Fatal(message);
                    break;

                default:
                    throw new ArgumentNullException("Invalid log level.");
            }
        }

    }
}