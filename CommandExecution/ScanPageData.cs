using Microsoft.Extensions.Logging;
using WebPareser.Data;
using WebParser.CommandVerbs;
using WebParser.Models;
using WebParser.Scanner;

namespace WebParser.CommandExecution
{
    internal static class ScanPageData
    {
        private static DatabaseContext _context;
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;
        private static ScannerApi _scanner;

        public static int Run(ScanPageDataVerb options, DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _scanner = new ScannerApi(_logger);

            Page[] pages = _context.Pages.ToArray();

            for (int i = 0; i < pages.Count(); i++)
                pages[i] = _scanner.ScanPageContent(pages[i]);

            if (options.Files)
                for (int i = 0; i < pages.Count(); i++)
                    _scanner.ScanPageFiles(pages[i]);

            _context.UpdateRange(pages);

            return 0;
        }
    }
}
