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

            
            
            try
            {
                _logger.LogInformation("Запуск копирования файлов...");
                _logger.LogInformation("Пересоздание папок с файлами...");

                var folder = Environment.SpecialFolder.MyDocuments;
                var folderPath = Environment.GetFolderPath(folder);

                if (Directory.Exists($"{folderPath}/GoreftinskyData/Files"))
                    Directory.Delete($"{folderPath}/GoreftinskyData/Files", true);

                Directory.CreateDirectory($"{folderPath}/GoreftinskyData/Files");
                Directory.CreateDirectory($"{folderPath}/GoreftinskyData/Files/Pages");

                _logger.LogInformation("Пересоздание папок с файлами успешно завершено.");

            }
            catch (IOException ex)
            {
                _logger.LogCritical("Не удалось пересоздать папки для хранения файлов");
            }

            Page[] pages = _context.Pages.ToArray();

            try
            {
                _logger.LogInformation("Создание папок страниц...");

                foreach (var p in pages)
                {
                    var pageFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/GoreftinskyData/Files/Pages/{p.Id}";
                    Directory.CreateDirectory(pageFolderPath);
                }

                _logger.LogInformation("Создание папок страниц успешно завершено.");
            }
            catch(IOException ex)
            {
                _logger.LogCritical($"Не удалось cоздать папку для файлов страницы. Подробности:\n{ex.Message}");
            }

           

            if (!options.Files)
                for (int i = 0; i < pages.Count(); i++)
                    pages[i] = _scanner.ScanPageContent(pages[i]);

            if (options.Files)
                for (int i = 0; i < pages.Count(); i++)
                    _scanner.ScanPageFiles(pages[i]);

            _context.UpdateRange(pages);
            _context.SaveChanges();

            return 0;
        }
    }
}
