using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAPP.App_Start;

namespace WebApiAPP.Logging
{
    public class LoggerFactory
    {
        private static ILogAdapter _logger;

        public static void InitializeLogFactory(ILogAdapter logger)
        {
            _logger = logger;
        }

        public static ILogAdapter GetLogger()
        {
            return _logger;
        }
    }
}