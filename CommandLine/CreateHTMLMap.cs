using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
    [Verb("create-map", HelpText = "Create html site map from all pages in database")]
    internal class CreateHTMLMap
    {

        [Option('d', "display", HelpText = "Defines Type of result display (file|display)")]
        public string Display {get; set;}


        [Option('f', "folder", HelpText = "Defines Resulting file Destination Folder")]
        public string DestinationFolder { get; set; }


        public static int Run(CreateHTMLMap options) 
        {
            return 0;
        }
    }

    
}
