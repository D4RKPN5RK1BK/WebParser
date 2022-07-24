using AngleSharp;
using WebPareser.Data;
using Microsoft.Extensions.Logging;
using CommandLine;
using Microsoft.Extensions.Configuration;
using WebParser.CommandVerbs;
using WebParser.CommandExecution;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebPareser;
class Program
{
    static async Task Main(string[] args)
    {
        var configBuilder = new ConfigurationBuilder();
        var config = configBuilder.AddJsonFile("../appsetings.json");

        var loggerFactory = LoggerFactory.Create(lc =>
        {
            lc.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.IncludeScopes = false;
            });
        });

        var logger = loggerFactory.CreateLogger<Program>();

        var context = new DatabaseContext();


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

