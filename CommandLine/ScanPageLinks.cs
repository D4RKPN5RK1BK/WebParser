using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("scan-links", HelpText = "Scan links in selected page")]
    internal class ScanPageLinks
    {
        [Option('n', "name", Required = true, Default = null, HelpText = "Name of the page")]
        public string Name { get; set; }


        [Option('i', "initial", HelpText = "Starts initial page scan and defins page-groups")]
        public bool InitialScan { get; set; }

        [Option('a', "all", HelpText = "Starts all inner page scanning")]
        public bool ScanAllPages { get; set; }

        public static int Run(ScanPageLinks options) 
        {
            return 0;
        }
    }
}
