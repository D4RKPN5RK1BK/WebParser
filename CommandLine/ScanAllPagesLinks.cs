using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("scan-all", HelpText = "Scan all pages")]
    internal class ScanAllPagesLinks
    {
        [Option('c', "content-scan", Default = false, HelpText = "Also scan links in page content")]
        bool ContentScan { get; set; }

        public static int Run(ScanAllPagesLinks options) 
        {
            return 0;
        }
    }
}
