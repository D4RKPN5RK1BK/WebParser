using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("display-all", HelpText = "Display pages list")]
    internal class DisplayAllPages
    {

        public static int Run(DisplayAllPages options) 
        {
            return 0;
        }
    }
}
