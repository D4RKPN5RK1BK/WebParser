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

        public static int Run(ScanAllPagesLinks options) 
        {
            return 0;
        }
    }
}
