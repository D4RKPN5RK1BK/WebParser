using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandVerbs
{
    [Verb("scan-links", HelpText = "Scan links in selected page")]
    internal class ScanPageLinksVerb
    {
        [Option('n', "name", Required = true, Default = null, HelpText = "Name of the page (applaing only to 'single' mode")]
        public string Name { get; set; }

        [Option('m', "mode", Default = "full", HelpText = "Defination of scanning mode")]
        public string Mode { get; set; }

        [Option('c', "clear", HelpText = "Clear database before scanning")]
        public bool ClearDatabase { get; set; }

        public static int Run(ScanPageLinksVerb options) 
        {

            if (options.ClearDatabase)
            {

            }

            switch(options.Mode) 
            { 
                case "full":
                    InitialModelScan();
                    AllModeScan();
                    break;
                case "single":
                    SingleModeScan();
                    break;
                case "initial":
                    InitialModelScan();
                    break;
                case "all":
                    AllModeScan();
                    break;
            }


            Console.WriteLine("Scan page data is running");
            return 0;
        }

        private static void SingleModeScan()
        {

        }

        private static void InitialModelScan()
        {

        }

        private static void AllModeScan()
        {

        }
    }
}
