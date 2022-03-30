using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPareser.Data;

namespace WebParser.CommandExecution
{
    internal static class Execution
    {
        private static DatabaseContext context;
        private static ILoggerFactory loggerFactory;
        private static ILogger logger;
        
        public static int Run()
        {
            //Some code here
            return OptionsResponce();
        }

        private static int OptionsResponce()
        {
            return 0;
        }
    }
}
