using WebPareser.Data;
using Microsoft.Extensions.Logging;
using CommandLine;
using WebParser.CommandVerbs;
using WebParser.CommandExecution;

namespace WebPareser
{
    class Program
    {

        private static DatabaseContext context;
        private static ILoggerFactory loggerFactory;
        private static ILogger logger;

        static async Task Main(string[] args)
        {

            // Подключение к файлу конфигурации
            // using IHost host = Host.CreateDefaultBuilder(args).Build();
            // host.Run();

            // Добавление логгера
            loggerFactory = LoggerFactory.Create(config =>
            {
                config.AddSimpleConsole(options =>
                {
                    options.SingleLine = true;
                    options.IncludeScopes = false;
                });
            });

            logger = loggerFactory.CreateLogger<Program>();

            context = new DatabaseContext();


            // Контроллер входящих параметров
            Parser.Default.ParseArguments<ScanPageDataVerb, CreateMapVerb, ScanPageLinksVerb>(args)
                .MapResult(
                    (ScanPageDataVerb options) => ScanPageData.Run(options, context, logger),
                    (CreateMapVerb options) => CreateMap.Run(options, context, logger),
                    (ScanPageLinksVerb options) => ScanPageLinks.Run(options, context, logger),
                    error => 1
                );
        }
    }
}
