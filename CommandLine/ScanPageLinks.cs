using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("scan", HelpText = "Scan links selected page")]
    internal class ScanPageLinks
    {

        public static int Run(ScanPageLinks options) 
        {
            return 0;
        }
    }
}
