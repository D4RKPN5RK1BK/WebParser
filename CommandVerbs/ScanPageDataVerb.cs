using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandVerbs
{
    [Verb("scancontent", HelpText = "Сканирует информацию со страниц в базе данных")]
    internal class ScanPageDataVerb
    {
        [Option('l', "link", HelpText = "Ссылка на конкретную страницу для сканирования")]
        public bool AllPages { get; set; }

        [Option('r', "raw", HelpText = "Scans page content as raw html")]
        public bool Raw { get; set; }

    }
}
