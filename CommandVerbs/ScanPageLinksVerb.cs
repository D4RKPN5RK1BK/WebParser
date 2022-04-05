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
      
    }
}
