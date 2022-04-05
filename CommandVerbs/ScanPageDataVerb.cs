using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandVerbs
{
    [Verb("scan-data", HelpText = "Display pages list")]
    internal class ScanPageDataVerb
    {
        [Option('a', "all", HelpText = "Scans all pages content")]
        public bool AllPages { get; set; }

        [Option('r', "raw", HelpText = "Scans page content as raw html")]
        public bool Raw { get; set; }

    }
}
