using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandVerbs
{
    [Verb("map", HelpText = "Создает HTML разметку с содержанием карты сайта")]
    internal class CreateMapVerb
    {

        [Option('d', "display", HelpText = "Defines Type of result display (file|display)")]
        public string Display {get; set;}


        [Option('f', "folder", HelpText = "Defines Resulting file Destination Folder")]
        public string DestinationFolder { get; set; }

    }

    
}
