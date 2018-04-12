using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAPP.App_Start
{
    public interface ILogAdapter
    {
        void WriteMessage(string source, WebApiAPP.Logging.NLogAdapter.LogLevel level, Exception ex);
        void WriteMessage(string source, WebApiAPP.Logging.NLogAdapter.LogLevel level, string message);
    }
}
