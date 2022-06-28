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
        [Option('f', "files", HelpText = "Дополнительно собирает данные о файлах на всех страницах")]
        public bool Files { get; set; }

    }
}
