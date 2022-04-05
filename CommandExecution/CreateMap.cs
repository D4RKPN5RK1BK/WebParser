using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPareser.Data;
using WebParser.CommandVerbs;

namespace WebParser.CommandExecution
{
    internal static class CreateMap
    {
        private static DatabaseContext _context;
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;

        public static int Run(CreateMapVerb options, DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;



            return OptionsResponce();
        }

        private static int OptionsResponce()
        {
            return 0;
        }
    }
}
