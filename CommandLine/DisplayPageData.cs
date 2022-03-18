using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("display", HelpText = "Display selected page data")]
    internal class DisplayPageData
    {
        [Option('l', "links", Default = false, HelpText = "Add links to display data")]
        public bool WithLinks { get; set; }
        public static int Run(DisplayPageData options) 
        {
            return 0;
        }
    }
}
