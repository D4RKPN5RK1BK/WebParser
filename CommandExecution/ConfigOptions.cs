using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPareser.CommandVerbs;
using WebPareser.Data;

namespace WebParser.CommandExecution
{
    internal static class ConfigOptions
    {
        private static DatabaseContext _context;
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;

        public static int Run(ConfigOptionsVerb options, ILogger logger)
        {
            _logger = logger;



            return OptionsResponce();
        }

        private static int OptionsResponce()
        {
            return 0;
        }
    }
}
