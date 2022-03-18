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

        public static int Run(DisplayPageData options) 
        {
            return 0;
        }
    }
}
